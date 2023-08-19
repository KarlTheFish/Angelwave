using Gtk;

namespace Angelwave;

public class Finder
{
    static Song newSong;
    private static bool newSongsFound;
    public static List<int> NoPlaylists = new List<int>();
    static readonly List<Song> FoundSongs = new List<Song>();

    public static void FindAllSongs(string filePath) {
        newSongsFound = false;
        NoPlaylists.Add(-1);
        foreach (var song in Directory.GetFiles(filePath, "*.mp3", SearchOption.AllDirectories)) {
            newSong = new Song(Path.GetFileNameWithoutExtension(song), "Unknown", song, NoPlaylists);
            if (FoundSongs.Contains(newSong) == false) {
                Console.WriteLine("A new song was found!");
                FoundSongs.Add(newSong);
                newSongsFound = true;
            }
        }

        if (FoundSongs.Count == 0 || newSongsFound == false) {
            Console.WriteLine("No new songs found.");
        }
        else {
            Console.WriteLine("Found songs:");
            foreach (var song in FoundSongs) {
                Console.WriteLine(song.Name);
                GUI.songsList.Add(MakeSongLabel(song.Name));
            }
            GUI.songsList.ShowAll();
        }
    }

    public static Button MakeSongLabel(string Name) {
        Button songLabel = new Button(Name);
        songLabel.Relief = ReliefStyle.None;
        songLabel.Margin = 0;
        return songLabel;
    }
}
