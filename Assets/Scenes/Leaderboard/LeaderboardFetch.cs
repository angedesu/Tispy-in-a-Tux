using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Leaderboard
{
    public struct LeaderboardEntry
    {
        public string user;
        public int score;
        //I'm looking up if I need to do operator overload or something else to get these sorted correctly
    }
    public class Leaderboard
    {
        List<LeaderboardEntry> ranking;
        private Leaderboard()
        {
            ranking = new List<LeaderboardEntry>();
        }
        static async Task<Leaderboard> GetLeaderboard()
        {
            //Create a new leaderboard
            Leaderboard newBoard = new Leaderboard();
            //Setup the leaderboard
            await newBoard.Fetch();
            newBoard.SortLeaderboard();
            /*This is my best attempt at a async constructor
            *Hopefully it doesn't have a memory leak or anything
            *I don't like garbage collecting languages
            *I don't know when stuff does get collected, or if it does
            */
            return newBoard;
        }
        //Fetch for Leaderboard
        private async Task Fetch()
        {
            //Dummy debug code
            //*
            string[] players = { "Alice", "Bob", "Charlie", "Dan", "Evan" };
            int[] wins = { 50, 40, 30, 10, 5 };
            for (int i = 0; i < 5; i++)
            {
                LeaderboardEntry profile = new LeaderboardEntry
                {
                    score = wins[i],
                    user = players[i]
                };
            }
            //*/
            //Connect to the database
            //Make a call to users & wins
            //Insert that data into the Leaderboard
        }
        private void SortLeaderboard()
        {
            //Sort the entire leaderboard
            ranking.Sort(delegate (LeaderboardEntry player1, LeaderboardEntry player2)
            {
                return player1.score.CompareTo(player2.score);
            });
        }
    }
}
    
