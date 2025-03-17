using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Leaderboard
{
    public class LeaderboardEntry
    {
        string user;
        int score;
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
            //Connect to the database
            //Make a call to users & wins
            //Insert that data into the Leaderboard
        }
        private void SortLeaderboard()
        {
            //Sort the entire leaderboard
        }
    }
}
    
