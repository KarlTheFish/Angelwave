namespace Angelwave;

public class Finder
{
    private static string filePath;
    private static string nextSong;

    public static List<string> FoundSongs = new List<string>();

    public static void FindAllSongs(string filePath)
    {
        foreach (var song in Directory.GetFiles(filePath, "*.mp3", SearchOption.AllDirectories)) {
            FoundSongs.Add(song);
        }

        if (FoundSongs.Count == 0) {
            Console.WriteLine("No songs found.");
        }
        else {
            Console.WriteLine("Found songs:");
            foreach (var song in FoundSongs) {
                Console.WriteLine(song.Remove(0, filePath.Length).Remove(song.Length - filePath.Length - 4, 4));
            }
        }
    }
}
