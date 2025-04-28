using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TMPro;
using UnityEngine.Networking;
using System.Linq;
using System.Collections;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;

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
            public Recipe(string n, bool a, List<string> ingredients, List<string> tools, int i)
            {
                name = n;
                Alcoholic = a;
                //These need a better method of propogating themselves
                //I need to connect to the database and find out how I assemble this information though
                ingredientList = new List<string>(ingredients);
                toolList = new List<string>(tools);
                id = i;
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
                StringComparison comp = StringComparison.OrdinalIgnoreCase;
                foreach (string item in filterList)
                {
                    bool flag;
                    filterString.Trim();
                    flag = item.Contains(filterString);
                    if (item.Contains(filterString, comp))
                    {
                        return true;
                    }
                    else
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
            //Surrogate ID from database
            public int id { get; }
        }
        //Web URL
        private string url = $"https://www.thecocktaildb.com/api/json/v1/1/";
        //Lists
        private SortedDictionary<String, Recipe> marshalRecipeList; //Contains all items retrieved, so only one fetch necessary to the database for each recipe
        private SortedDictionary<String, Recipe> sergentRecipeList; //Contains the sorted/displayed list
        private List<GameObject> recipeGameObjectList; //List of all objects for destruction
        //Components for assembling individual recipe entries
        public GameObject recipeTemplate;
        public Text recipeText;
        public Transform recipeRowStart;
        //Components for sorting items
        public GameObject advancedFilter;//Set this to the advanced filter canvas
        public TMP_InputField nameFilter;
        public bool nameWhitelist = true; //For possible future filtering, easier to code in now, than refactor later
        public bool virginFilter = false;
        public TMP_InputField ingredientFilter;
        public bool ingredientWhitelist = true;
        public TMP_InputField toolFilter;
        public bool toolWhitelist = true;
        public void Start()
        {
            Debug.Log("Recipe List Script loaded");
            //Run code on scene loading
            //Set up the leaderboard
            StartCoroutine(this.Fetch("search.php?f=a"));
            //this.SortMarshal();
            //Create objects for each item in the list
            recipeGameObjectList = new List<GameObject>();
            //Populate each objects text with the getLeaderboardEntry function
            Vector3 position = recipeRowStart.position;
            foreach (var DictEntry in marshalRecipeList)
            {
                Recipe recipeEntry = DictEntry.Value;
                Debug.Log("In Loop. Recipe is " + recipeEntry.name);
                //Instatiate object
                recipeText.text = recipeEntry.name;
                GameObject tmp = Instantiate(recipeTemplate, recipeRowStart);
                tmp.transform.position = position;
                //It was probably a scale issue on the leaderboard version... sigh...
                position.y -= 100f;
                recipeGameObjectList.Add(tmp);
            }
        }
        private RecipeBook()
        {
            marshalRecipeList = new SortedDictionary<string, Recipe>();
        }
        public int GetRecipeID(string name)
        {
            //Grab the ID off the list
            Recipe recipe = marshalRecipeList.GetValueOrDefault(name);
            return recipe.id;
        }
        private static async Task<RecipeBook> GetRecipes()
        {
            //Create a new leaderboard
            RecipeBook newBook = new RecipeBook();
            //Setup the leaderboard
            IEnumerator enumerator = newBook.Fetch("search.php?f=a");
            //newBook.SortMarshal();
            /*This is my best attempt at a async constructor
            *Hopefully it doesn't have a memory leak or anything
            *I don't like garbage collecting languages
            *I don't know when stuff does get collected, or if it does
            */
            return newBook;
        }
        //Fetch for Leaderboard
        private IEnumerator Fetch(string searchType)
        {
            Debug.Log("Recipe Book Fetch called");
            //Dummy debug code
            /*
            for (int i = 0; i < 5; i++)
            {
                Recipe entry = new Recipe("Recipe " + i, i%2 == 0);
                //This is going to need some type of scanning to construct each recipe...
                //I need more info on the API I'm connecting to
                marshalRecipeList.Add(entry.name, entry);
                Debug.Log("Creating Recipe: " + entry.name);
            }
            */
            //Connect to the database
            string target = url+searchType;
            UnityWebRequest request = UnityWebRequest.Get(target);
            yield return request.SendWebRequest();
            //Fetch the recipe names
            if (request.result != UnityWebRequest.Result.Success)
            {
                //Request failed
                Debug.LogError(request.error);
                yield break;
            }
            JObject drinks = JObject.Parse(request.downloadHandler.text);
            foreach(JToken drink in drinks["drinks"])
            {
                if (drink == null)
                {
                    break;
                }
                //Get necessary tags per recipe
                string name = drink["strDrink"]!.ToString();
                //Check to see if this is a duplicate
                if (marshalRecipeList.ContainsKey(name))
                {
                    continue;
                }
                //It's new, add it
                string alcoholic = drink["strAlcoholic"]!.ToString();
                bool alcohol = (alcoholic == "Alcoholic");
                List<string> ingredients = new List<string>();
                int i = 1;
                do
                {
                    string ingredient;
                    ingredient = drink["strIngredient"+i]!.ToString();
                    i++;
                    if (ingredient != "")
                    {
                        ingredients.Add(ingredient);
                    }
                    else
                    {
                        break;
                    }
                } while (i < 16);
                List<string> tools = new List<string>();
                //Get the glass type
                tools.Add(drink["strGlass"]!.ToString());
                //Query the instructions to find individual tools used
                string instructions = drink["strInstructions"]!.ToString();
                if (instructions.Contains("shake"))
                {
                    tools.Add("Cocktail Shaker");
                }
                int id = ((int)drink["idDrink"]!);
                Recipe entry = new Recipe(name, alcohol, ingredients, tools, id);
                marshalRecipeList.Add(name, entry);
                Debug.Log("Creating Recipe: " + entry.name);
            }
            //Call the filter function
            string temp = nameFilter.text;
            nameFilter.text = "";
            FilterSergent();
            nameFilter.text = temp;
        }
        //The Marshal should only ever need to be sorted when it's fetched
        /*
        * Changed from list to dictionary
        private void SortMarshal()
        {
            Debug.Log("Marshal Sort Called");
            //Sort the Marshal list
            marshalRecipeList.Sort(delegate (Recipe r1, Recipe r2)
            {
                return r1.name.CompareTo(r2.name);
            });
        }*/
        //The Sergent list is always made from a copy of the sorted marshal,
        //it should always be sorted, therefore this should be unnecessary
        /*
         * Changed from list to dictionary
        private void SortSergent()
        {
            Debug.Log("Sergent Sort Called");
            //Sort the Sergent list
            sergentRecipeList.Sort(delegate (Recipe r1, Recipe r2)
            {
                    return r1.name.CompareTo(r2.name);
            });
        }*/
        //This is the important list for sorting
        public void ToggleVirgin()
        {
            virginFilter = !virginFilter;
            /*string Alcoholic;
            if (virginFilter)
            {
                Alcoholic = "Non-Alcoholic";
            }
            else
            {
                Alcoholic = "Alcoholic";
            }
            StartCoroutine(Fetch("filter.php?a="+Alcoholic));
            */
            //Taken out as it returns a different format of responses, that lacks much of the information we use
            FilterSergent();
        }
        public void UpdateSearch()
        {
            //Update the search
            if (nameFilter.text != "")
            {
                //Use nameFilter for the search
                StartCoroutine(Fetch("search.php?s="+nameFilter.text));
            }
            //Use the ingredients or tools to search
            //NOTE: These are unimplemented ATM as the filtered search returns less information than a normal search
            if(ingredientFilter.text != "")
            {
                //Use IngredientFilter for the search
            }
            if (toolFilter.text != "")
            {
                //Use toolFilter for the search
            }
        }
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
            if (sergentRecipeList is not null)
            {
                sergentRecipeList.Clear();
            }
            sergentRecipeList = new SortedDictionary<string, Recipe>(marshalRecipeList);
            //Drop whitespace from filters if necessary. Requires testing if necessary
            //Iterate through the new sergent list and make sure the recipe matches our filters
            List<Recipe> DropList = new List<Recipe>();
            foreach (var kvPair in sergentRecipeList)
            {
                Recipe r = kvPair.Value;
                if (r.nameCheck(nameFilter.text))
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
                        DropList.Add(r);
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
                        DropList.Add(r);
                        continue;
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
                if (r.Alcoholic != virginFilter)
                {
                    //This recipe matches the virgin filter
                    //The virgin filter is inverted from the alcoholic setting
                    //Do nothing
                }
                else
                {
                    //This recipe does not match the virgin filter
                    //Drop it
                    DropList.Add(r);
                    continue;
                }
                if (r.ingredientCheck(ingredientFilter.text))
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
                        DropList.Add(r);
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
                        DropList.Add(r);
                        continue;
                    }
                    else
                    {
                        //Do nothing
                    }
                }
                if (r.toolCheck(toolFilter.text))
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
                        DropList.Add(r);
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
                        DropList.Add(r);
                        continue;
                    }
                    else
                    {
                        //Do nothing
                    }
                }
            }
            //Iterate through the droplist and drop those items
            //Yeah, it'd probably have been more efficient to only add items to the sergent list if they matched. I coded the first half, thinking I could drop as I scanned, I couldn't
            //Either way... more SLOP I mean SLOC!
            foreach (Recipe r in DropList)
            {
                sergentRecipeList.Remove(r.name);
            }
            //Create the new gameObjects for the filtered recipes
            Vector3 position = recipeRowStart.position;
            recipeGameObjectList = new List<GameObject>();
            foreach (var kvPair in sergentRecipeList)
            {
                Recipe r = kvPair.Value;
                Debug.Log("In Loop, Filtered Recipes. Recipe is " + r.name);
                //Instatiate object
                recipeText.text = r.name;
                GameObject tmp = Instantiate(recipeTemplate, recipeRowStart);
                tmp.transform.position = position;
                //It was probably a scale issue on the leaderboard version... sigh...
                position.y -= 100f;
                recipeGameObjectList.Add(tmp);
            }
        }
    }
}

