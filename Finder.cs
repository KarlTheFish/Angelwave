namespace Angelwave;

public class Finder
{
    private static string filePath;
    private static string nextSong;

    public static List<string> FoundSongs = new List<string>();

    public static void FindAllSongs()
    {
        Console.WriteLine("Insert music file path:");
        filePath = Console.ReadLine();

        foreach (var song in Directory.GetFiles(filePath, "*.mp3", SearchOption.AllDirectories)) {
            FoundSongs.Add(song);
        }

        Console.WriteLine("Found songs:");
        foreach (var song in FoundSongs) {
            Console.WriteLine(song.Remove(0, filePath.Length).Remove(song.Length - filePath.Length - 4, 4));
        }
    }
}
