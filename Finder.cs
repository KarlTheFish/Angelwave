namespace Angelwave;

public class Finder
{
    static Song newSong;

    public static List<Song> FoundSongs = new List<Song>();

    public static void FindAllSongs(string filePath) {
        foreach (var song in Directory.GetFiles(filePath, "*.mp3", SearchOption.AllDirectories)) {
            newSong = new Song(Path.GetFileNameWithoutExtension(song), "Unknown", song);
            FoundSongs.Add(newSong);
        }

        if (FoundSongs.Count == 0) {
            Console.WriteLine("No songs found.");
        }
        else {
            Console.WriteLine("Found songs:");
            foreach (var song in FoundSongs) {
                Console.WriteLine(song.Name);
            }
        }
    }
}
