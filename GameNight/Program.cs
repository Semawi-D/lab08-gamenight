/*
* Name: Semawi Deneke
* Course: CSCI 1250, Section 001
* Assignment: Lab 08, Game Night
* Date: November 18, 2026
* Description: Keeps a game night leaderboard in three text files. Shows the
* season so far, records tonight's points, saves the new totals,
* and adds tonight's result to a history that grows every week.
*/



const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;
const int MIN_POINTS = 0;
// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(
Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);



//TITLE
Console.WriteLine("=== GAME NIGHT LEADERBOARD ===");

//PART-3: THE HISTORY (SECTION A)
string[] history = File.ReadAllLines(HISTORY_FILE);
int nightNumber;
if (history.Length == 0)
{
    Console.WriteLine("No nights played yet.");
    Console.WriteLine("Tonight is Night 1.");
    nightNumber = 1;
}
else
{
    nightNumber = history.Length + 1;
    Console.WriteLine($"Nights played: {history.Length}");
    //I chose this loop because it is best for iterating over an entire array, and I had to list every entry in "history"
    foreach (string night in history)
    {
        Console.WriteLine($"  {night}");
    }
    Console.WriteLine($"Tonight is Night {nightNumber}.");
}

//PART-1: THE STANDINGS
string[] points = File.ReadAllLines(POINTS_FILE);
string[] players = File.ReadAllLines(PLAYERS_FILE);
Console.WriteLine("");
Console.Write($"{"PLAYER".PadRight(11)}");
Console.WriteLine("POINTS");
//I had to iterate every element of two different arrays, and I was able to do that because I can use the control variable in this loop as an index for both of my arrays
for (int i = 0; i < players.Length; i++)
{
    Console.Write(players[i].PadRight(11));
    Console.WriteLine(points[i]);
}
Console.WriteLine($"Leader: {players[LeaderIndex(points)]} with {points[LeaderIndex(points)]}");
Console.WriteLine($"Season total: {TotalPoints(points)}");
Console.WriteLine(" ");

//PART-2: GAME NIGHT
string tonightsWinner = "";
int tonightsBestScore = 0;
int season;
Console.WriteLine("");
//I chose this loop because the control variable/index allowed me to create a new "points" array that housed the updated season points for each person
for(int i = 0; i < players.Length; i++)
{
    Console.Write($"Points tonight for {players[i]}: ");
    int tonightsPoint = Convert.ToInt32(Console.ReadLine());
    //I chose this loop because it will keep asking for the right input until the user does so
    while (tonightsPoint > MAX_POINTS || tonightsPoint < MIN_POINTS)
    {
        Console.WriteLine($"Points must be from {MIN_POINTS} to {MAX_POINTS}.");
        Console.Write($"Points tonight for {players[i]}: ");
        tonightsPoint = Convert.ToInt32(Console.ReadLine());
    }
    if (tonightsPoint > tonightsBestScore)
    {
        tonightsBestScore = tonightsPoint;
        tonightsWinner = players[i];
    }
    season = Convert.ToInt32(points[i]) + tonightsPoint;
    points[i] = season.ToString();
}
Console.WriteLine($"Tonight's winner: {tonightsWinner} with {tonightsBestScore}");
File.WriteAllLines(POINTS_FILE, points);

//PART-3: THE HISTORY (SECTION B)
string[] entry = { HistoryEntry(nightNumber, tonightsWinner, tonightsBestScore) };
File.AppendAllLines(HISTORY_FILE, entry);
Console.WriteLine("Saved points.txt.");
Console.WriteLine($"Added to history.txt: {entry[0]}");





//METHODS

//This method returns the index of the person with highest total number of points
static int LeaderIndex(string[] points)
{
    int leaderIndex = 0;
    int highestPoint = 0;
    //I chose this loop because I had to capture the index of the highest point in the array
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
//This method returns total number of points in the season
static int TotalPoints(string[] points)
{
    int totalPoints = 0;
    //I chose this array because I needed to iterate over the entire array without the need of a control variable
    foreach (string point in points)
    {
        totalPoints += Convert.ToInt32(point);
    }
    return totalPoints;
}
//This method returns an entry for a single night
static string HistoryEntry(int night, string winner, int points)
{
    string history = $"Night {night}: {winner} won with {points} points";
    return history;
}