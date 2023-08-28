using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using Gtk;

namespace Angelwave; //TODO: Make song data saved in a file

public class Finder
{
    static Song newSong;
    private static bool newSongsFound = false;
    public static List<int> NoPlaylists = new List<int>();
    static readonly List<Song> FoundSongs = new List<Song>();
    private static XDocument allSongs = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), new XElement("allSongs"));

    public static void FindAllSongs(string filePath)
    {
        NoPlaylists.Add(-1);
        //This if-else block determines whether or not to search through all subdirectories
        if (GUI.subDirSearchToggled) {
            foreach (var song in Directory.GetFiles(filePath, "*.mp3", SearchOption.AllDirectories)) {
                newSong = new Song(Path.GetFileNameWithoutExtension(song), "Unknown", song, NoPlaylists);
                newSongFound(newSong);
            }
        }
        else {
            foreach (var song in Directory.GetFiles(filePath, "*.mp3", SearchOption.TopDirectoryOnly)) {
                newSong = new Song(Path.GetFileNameWithoutExtension(song), "Unknown", song, NoPlaylists);
                newSongFound(newSong);
            }
        }
        
        //Once the if-else block is done, all (new) songs are shown in GUI and saved to XML
        GUI.songsList.ShowAll();
        allSongs.Save("/home/karl-aleksander/RiderProjects/Angelwave/songsList.xml");
        GUI.finder.Destroy();
        GUI.finder = new Window("Finder");
    }

    public static void newSongFound(Song song) {
        GUI.songsList.Add(MakeSongLabel(song.Title));
        AddSongToXML(song);
    }

    public static Button MakeSongLabel(string Name) {
        Button songLabel = new Button(Name);
        songLabel.Relief = ReliefStyle.None;
        songLabel.Margin = 0;
        return songLabel;
    }

    static void AddSongToXML(Song song) {
        XElement songElement = new XElement("Song",
            new XElement("Title", song.Title),
            new XElement("Artist", song.Artist),
            new XElement("Path", song.Path),
            new XElement("Playlists", song.Playlists)
        );
        
        allSongs.Root.Add(songElement);
    }
}
