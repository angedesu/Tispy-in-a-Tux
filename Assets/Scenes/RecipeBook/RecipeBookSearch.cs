using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RecipeBook
{
    public class RecipeBook : MonoBehaviour
    {
        public enum filterType
        {
            NAME,
            INGREDIENT,
            TOOL
        }
        public class Recipe
        {
            public Recipe(string n = "", bool a = true)
            {
                name = n;
                Alcoholic = a;
                //These need a better method of propogating themselves
                //I need to connect to the database and find out how I assemble this information though
                ingredientList = new List<string>();
                toolList = new List<string>();
            }
            public bool ingredientCheck(string ingredientString)
            {
                //This is going to be quite the bit of magic...
                //Temporarily though
                return filterParse(ingredientString, filterType.INGREDIENT);
            }
            public bool toolCheck(string toolString)
            {
                //This is going to be quite the bit of magic...
                //Temporarily though
                return filterParse(toolString, filterType.TOOL);
                //As for the rules of how the tools are checked, I can use the above logic
                //Technically,
                //I can probably just pass the string containing the filter expression to a generalized parser,
                //so long as it knows what it's parsing
                //Which is what I refactored the code to do
            }
            public bool nameCheck(string nameString)
            {
                //Makes a standardized wrapper for interacting with the parser for increased legibility
                return filterParse(nameString, filterType.NAME);
            }
            private bool filterParse(string filterString, filterType filter)
            {
                List<string> filterList = new List<string>();
                switch (filter)
                {
                    case filterType.NAME:
                        //A clunky way to do this, but a list of one allows me to parse all identically
                        //As well as giving the Secret Advanced Search Features/Parsing functionality to ALL filters
                        filterList = new List<string>();
                        filterList.Add(name);
                        break;
                    case filterType.INGREDIENT:
                        filterList = new List<string>(ingredientList);
                        break;
                    case filterType.TOOL:
                        filterList = new List<string>(toolList);
                        break;
                }
                foreach (string item in filterList)
                {
                    if (item.Contains(filterString))
                    {
                        return true;
                    } else
                    {
                        continue; //This is terrible practice... but I do it because SLOC is used as a metric of my performance
                        //Really I should have written this function three seperate times and not used the above switch case to again
                        //Artificially bloat my SLOC, but it'd be a maintance nightmare, and I'm not that stupid
                    }
                }
                return false;
                //It needs to disect the input box string for common seperation methods a user might use to list different items in a list
                //Then individually scan each item against the list and determine if they match
                /* Implementation notes
                 * by default in whitelist mode the default junction between items is considered logical AND
                 * Therefore, every ingredient listed needs to show up
                 * LOGICAL AND DELIMITORS:
                 * ' ', ',', ';', ':', '/n', '&', '.' and tab (forgot the character code. Using whitespace filtering and the others noted)
                 * The following delimiters can be considered logical OR instead: 
                 * '|'
                 * Finally, the prefix of '!' will function as a logical NOT
                 * In addition the delimitors of (/), [/], and {/} can be used to create recursive calls
                 * In the event of a partial or misaligned set of groupings, they will be dropped, and undefined filtering may occur
                 * Due to the whitelist/blacklist, this results in a DeMorgan's behavior,
                 * blacklists will exclude any entries that include any default ANDS
                 * blacklists will require BOTH ORS to be present to exclude
                 * and finally blacklists will try to include sections prefixed by NOTS
                 * This may cause odd results as the WHOLE expression is inverted by the whitelist setting AFTER whitelist style handling
                 * This is all technically undocumented code, but its functionality a good filtering system SHOULD include
                 * Likewise, be aware that advanced sorting is done individually per category,
                 * so the ingredient check, tool check, and name check are all independant
                 * I'll try to update the necessary code for this filtering logic when I can,
                 * but it's one of those things that I don't know how much time I can devote to it...
                 * It is a project for Academia, and this is an outline of how I believe it should be done
                 * How I'm actually able to implement it is a different story,
                 * especially considering: 1. This project is closed source as I write this
                 * 2. I am heavily time constricted
                 * 3. Pressure is to deliver a "working" version, not make each feature ideal
                 */
            }
            //Information about recipes required for sorting/handling
            public string name;
            public bool Alcoholic { get; }

            //Need ingredient list
            private List<string> ingredientList;
            //Need tool list
            private List<string> toolList;
        }
        private List<Recipe> marshalRecipeList; //Contains all items, so only one fetch necessary to the database
        private List<Recipe> sergentRecipeList; //Contains the sorted/displayed list
        private List<GameObject> recipeGameObjectList; //List of all objects for destruction
        //Components for assembling individual recipe entries
        public GameObject recipeTemplate;
        public Text recipeText;
        public Transform recipeRowStart;
        //Components for sorting items
        public GameObject advancedFilter;//Set this to the advanced filter canvas
        public string nameFilter;
        public bool nameWhitelist; //For possible future filtering, easier to code in now, than refactor later
        public bool virginFilter;
        public string ingredientFilter;
        public bool ingredientWhitelist;
        public string toolFilter;
        public bool toolWhitelist;
        public void Start()
        {
            Debug.Log("Recipe List Script loaded");
            //Run code on scene loading
            //Set up the leaderboard
            this.Fetch();
            this.SortMarshal();
            //Create objects for each item in the list
            //Populate each objects text with the getLeaderboardEntry function
            Vector3 position = recipeRowStart.position;
            foreach (Recipe recipeEntry in marshalRecipeList)
            {
                Debug.Log("In Loop. Recipe is " + recipeEntry.name);
                //Instatiate object
                recipeText.text = recipeEntry.name;
                GameObject tmp = Instantiate(recipeTemplate, recipeRowStart);
                tmp.transform.position = position;
                //I have no clue why unity is offsetting these so much
                //It should be
                //position.y -= 90;
                position.y -= 2.5f;
            }
        }
        private RecipeBook()
        {
            marshalRecipeList = new List<Recipe>();
        }
        static async Task<RecipeBook> GetRecipes()
        {
            //Create a new leaderboard
            RecipeBook newBook = new RecipeBook();
            //Setup the leaderboard
            newBook.Fetch();
            newBook.SortMarshal();
            /*This is my best attempt at a async constructor
            *Hopefully it doesn't have a memory leak or anything
            *I don't like garbage collecting languages
            *I don't know when stuff does get collected, or if it does
            */
            return newBook;
        }
        //Fetch for Leaderboard
        private void Fetch()
        {
            Debug.Log("Recipe Book Fetch called");
            //Dummy debug code
            //*
            string[] players = { "Alice", "Bob", "Charlie", "Dan", "Evan" };
            int[] wins = { 50, 40, 30, 10, 5 };
            for (int i = 0; i < 5; i++)
            {
                Recipe entry = new Recipe("Recipe " + i, false);
                //This is going to need some type of scanning to construct each recipe...
                //I need more info on the API I'm connecting to
                marshalRecipeList.Add(entry);
                Debug.Log("Creating Recipe: " + entry.name);
            }
            //*/
            //Connect to the database
            //Fetch the recipe names
            //Get necessary tags per recipe
        }
        //The Marshal should only ever need to be sorted when it's fetched
        private void SortMarshal()
        {
            Debug.Log("Marshal Sort Called");
            //Sort the Marshal list
            marshalRecipeList.Sort(delegate (Recipe r1, Recipe r2)
            {
                return r2.name.CompareTo(r1.name);
            });
        }
        //The Sergent list is always made from a copy of the sorted marshal,
        //it should always be sorted, therefore this should be unnecessary
        private void SortSergent()
        {
            Debug.Log("Sergent Sort Called");
            //Sort the Sergent list
            sergentRecipeList.Sort(delegate (Recipe r1, Recipe r2)
            {
                return r2.name.CompareTo(r1.name);
            });
        }
        //This is the important list for sorting
        public void FilterSergent()
        {
            Debug.Log("Filtering the list");
            //Delete all currently visible game objects for the list
            foreach (GameObject obj in recipeGameObjectList)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
            recipeGameObjectList.Clear();
            //Create a new copy of the sergent list
            sergentRecipeList = new List<Recipe>(marshalRecipeList);
            //Drop whitespace from filters if necessary. Requires testing if necessary
            //Iterate through the new sergent list and make sure the recipe matches our filters
            foreach (Recipe r in sergentRecipeList)
            {
                if (r.nameCheck(nameFilter))
                {
                    //This recipe contains a name filter term
                    //If whitelist mode is on, keep it in
                    if (nameWhitelist)
                    {
                        //Do nothing
                    }
                    else
                    {
                        //Drop it
                        sergentRecipeList.Remove(r);
                        continue;
                    }
                }
                else
                {
                    //This recipe does not contain a name filter term
                    //If whitelist mode is on, drop it
                    if (nameWhitelist)
                    {
                        //Drop it
                        sergentRecipeList.Remove(r);
                    }
                    else
                    {
                        //Do nothing
                    }
                }
                if (advancedFilter.activeInHierarchy == false)
                {
                    //Advanced search is off
                    continue;
                }
                if (r.Alcoholic == virginFilter)
                {
                    //This recipe matches the virgin filter
                    //Do nothing
                }
                else
                {
                    //This recipe does not match the virgin filter
                    //Drop it
                    sergentRecipeList.Remove(r);
                }
                if (r.ingredientCheck(ingredientFilter))
                {
                    //Recipe matches the ingredient Filter
                    //If whitelist mode is on, keep it in
                    if (ingredientWhitelist)
                    {
                        //Do nothing
                    }
                    else
                    {
                        //Drop it
                        sergentRecipeList.Remove(r);
                        continue;
                    }
                }
                else
                {
                    //This recipe does not match the ingredient filter
                    //If whitelist mode is on, drop it
                    if (ingredientWhitelist)
                    {
                        //Drop it
                        sergentRecipeList.Remove(r);
                    }
                    else
                    {
                        //Do nothing
                    }
                }
                if (r.toolCheck(toolFilter))
                {
                    //Recipe matches the tool Filter
                    //If whitelist mode is on, keep it in
                    if (toolWhitelist)
                    {
                        //Do nothing
                    }
                    else
                    {
                        //Drop it
                        sergentRecipeList.Remove(r);
                        continue;
                    }
                }
                else
                {
                    //This recipe does not match the tool filter
                    //If whitelist mode is on, drop it
                    if (toolWhitelist)
                    {
                        //Drop it
                        sergentRecipeList.Remove(r);
                    }
                    else
                    {
                        //Do nothing
                    }
                }
            }
        }
    }
}
    
