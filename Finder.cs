using System.Xml.Linq;
using Gtk;

namespace Angelwave; //TODO: Make song data saved in a file

public class Finder
{
    static Song newSong;
    private static bool newSongsFound;
    public static List<int> NoPlaylists = new List<int>();
    static readonly List<Song> FoundSongs = new List<Song>();
    private static XDocument allSongs = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));

    public static void FindAllSongs(string filePath) {
        NoPlaylists.Add(-1);

        if (GUI.subDirSearchToggled == true) {
            FindSongsInDir(filePath);
            foreach (var dir in Directory.GetDirectories(filePath)) {
                FindSongsInDir(dir); //BUG: Only finds songs in first level subdirectory
                //TODO: Fix the bug - make the function recursive?
            }
        }
        else {
            FindSongsInDir(filePath);
        }

        if (FoundSongs.Count == 0) {
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

    static void FindSongsInDir(string path) {
        newSongsFound = false;
        foreach (var song in Directory.GetFiles(path, "*.mp3", SearchOption.TopDirectoryOnly)) {
            newSong = new Song(Path.GetFileNameWithoutExtension(song), "Unknown", song, NoPlaylists);
            if (FoundSongs.Contains(newSong)) {
                Console.WriteLine("This song is already found!");
                newSongsFound = false;
            }
            else {
                FoundSongs.Add(newSong);
                newSongsFound = true;
            }
        }
    }

    static void AddSongToXML(Song song){
        
    }
}
