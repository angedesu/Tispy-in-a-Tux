using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine.SocialPlatforms.Impl;

namespace Leaderboard
{
    public class Top3Leaderboard : MonoBehaviour
    {
        public struct LeaderboardEntry
        {
            public LeaderboardEntry(string u = "", int s = 0, int p = 0)
            {
                user = u;
                score = s;
                profile = p;
            }
            //Reusing the other leaderboard script as a base
            //reworking what I can for the other leaderboard
            //I realized I can't use the same script for both, and need seperate scripts
            public string user;
            public int profile;
            public int score;
            //We need the user's profile for display, score for sorting...
            //Assuming I don't figure out how to pull from the database sorted
            //Which is pretty likely, I'm just tired
        }
        List<LeaderboardEntry> ranking;
        public GameObject playerRepresentation;
        public Text playerName;
        public Transform parentObject;
        public void Start()
        {
            Debug.Log("Leaderboard Script loaded");
            //Run code on scene loading
            //Set up the leaderboard
            this.Fetch();
            this.SortLeaderboard();
            //Create objects for each item in the list
            //Populate each objects text with the getLeaderboardEntry function
            Vector3 position = parentObject.position;
            for (int i = 0; i < 3; i++)
            {
                //I'm too tired to fix this at the moment
                /*
                Debug.Log("In Loop. Player is " + player.user);
                //Instatiate object
                rowText.text = player.user+ " " + player.score + " wins";
                GameObject tmp = Instantiate(playerRow, leaderboardRow);
                tmp.transform.position = position;
                //I have no clue why unity is offsetting these so much
                //It should be
                //position.y -= 90;
                position.y -= 2.5f;
                */ //There
            }
        }
        private Top3Leaderboard()
        {
            //I can trim the list after the sort. Or maybe just pull it sorted?
            ranking = new List<LeaderboardEntry>();
        }
        
        /*
        static async Task<Top3Leaderboard> GetLeaderboard()
        {
            //Create a new leaderboard
            Top3Leaderboard newBoard = new Top3Leaderboard();
            //Setup the leaderboard
            newBoard.Fetch();
            newBoard.SortLeaderboard();
            /*This is my best attempt at a async constructor
            *Hopefully it doesn't have a memory leak or anything
            *I don't like garbage collecting languages
            *I don't know when stuff does get collected, or if it does
        
            return newBoard;
        }*/
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
    
