/*
* Name: Semawi Deneke
* Course: CSCI 1250, Section 001
* Assignment: Lab 08, Game night
* Date: November 18, 2026
* Description: Keeps a game night leaderboard in three text files. Shows the
* season so far, records tonight's points, saves the new totals,
* and adds tonight's result to a history that grows every week.
*/


using System.Drawing;

const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;
// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(
Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);


//PART-1: THE STANDINGS
string[] points = File.ReadAllLines(POINTS_FILE);
string[] players = File.ReadAllLines(PLAYERS_FILE);
string[] history = File.ReadAllLines(HISTORY_FILE);

Console.Write($"{"PLAYER".PadRight(11)}");
Console.WriteLine("POINTS");
for (int i = 0; i < players.Length; i++)
{
    Console.Write(players[i].PadRight(11));
    Console.WriteLine(points[i]);
}
Console.WriteLine($"Leader: {players[LeaderIndex(points)]} with {points[LeaderIndex(points)]}");
Console.WriteLine($"Season total: {TotalPoints(points)}");







static int LeaderIndex(string[] points)
{
    int leaderIndex = 0;
    int highestPoint = 0;
    for (int i = 0; i < points.Length; i++)
    {
        if (Convert.ToInt32(points[i]) > highestPoint)
        {
            highestPoint = Convert.ToInt32(points[i]);
            leaderIndex = i;
        }
    }
    return leaderIndex;
}
static int TotalPoints(string[] points)
{
    int totalPoints = 0;
    foreach (string point in points)
    {
        totalPoints += Convert.ToInt32(point);
    }
    return totalPoints;
}
//static string HistoryEntry(int night, string winner, int points)