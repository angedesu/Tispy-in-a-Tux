using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Leaderboard
{
    public class Leaderboard : MonoBehaviour
    {
        public struct LeaderboardEntry
        {
            public LeaderboardEntry(string u = "", int s = 0)
            {
                user = u;
                score = s;
            }
            //WHY DO STRUCTS HAVE CONSTRUCTORS. THEY AREN'T OBJECTS. WHY IS C# MAKING THEM CLASSES
            //WHY EVEN HAVE STRUCTS
            //C# is evidence God is dead. Microsoft is evil. Who makes a struct a class?!
            public string user;
            public int score;
            //I'm looking up if I need to do operator overload or something else to get these sorted correctly
        }
        List<LeaderboardEntry> ranking;
        public GameObject playerRow;
        public Text rowText;
        public Transform leaderboardRow;
        public void Start()
        {
            Debug.Log("Leaderboard Script loaded");
            //Run code on scene loading
            //Set up the leaderboard
            this.Fetch();
            this.SortLeaderboard();
            //Create objects for each item in the list
            //Populate each objects text with the getLeaderboardEntry function
            Vector3 position = leaderboardRow.position;
            foreach (LeaderboardEntry player in ranking)
            {
                Debug.Log("In Loop. Player is " + player.user);
                //Instatiate object
                rowText.text = player.user+ " " + player.score + " wins";
                GameObject tmp = Instantiate(playerRow, leaderboardRow);
                tmp.transform.position = position;
                //I have no clue why unity is offsetting these so much
                //It should be
                //position.y -= 90;
                position.y -= 2.5f;
            }
        }
        public string getLeaderboardEntry(int index)
        {
            string EntryString;
            EntryString = ranking[index].user + " " + ranking[index].score + " wins";
            return EntryString;
        }
        private Leaderboard()
        {
            ranking = new List<LeaderboardEntry>();
        }
        static async Task<Leaderboard> GetLeaderboard()
        {
            //Create a new leaderboard
            Leaderboard newBoard = new Leaderboard();
            //Setup the leaderboard
            newBoard.Fetch();
            newBoard.SortLeaderboard();
            /*This is my best attempt at a async constructor
            *Hopefully it doesn't have a memory leak or anything
            *I don't like garbage collecting languages
            *I don't know when stuff does get collected, or if it does
            */
            return newBoard;
        }
        //Fetch for Leaderboard
        private void Fetch()
        {
            Debug.Log("Leaderboard Fetch called");
            //Dummy debug code
            //*
            string[] players = { "Alice", "Bob", "Charlie", "Dan", "Evan" };
            int[] wins = { 50, 40, 30, 10, 5 };
            for (int i = 0; i < 5; i++)
            {
                LeaderboardEntry profile = new LeaderboardEntry(players[i], wins[i]);

                ranking.Add(profile);
                Debug.Log("Creating Profile: " + profile.user + " " + profile.score);
            }
            //*/
            //Connect to the database
            //Make a call to users & wins
            //Insert that data into the Leaderboard
        }
        private void SortLeaderboard()
        {
            Debug.Log("Leaderboard Sort Called");
            //Sort the entire leaderboard
            ranking.Sort(delegate (LeaderboardEntry player1, LeaderboardEntry player2)
            {
                return player2.score.CompareTo(player1.score);
            });
        }
    }
}
    
