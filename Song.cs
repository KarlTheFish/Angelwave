namespace Angelwave;

public struct Song {
    public string Name;
    public string Artist;
    public string Path;
    public List<int> Playlists;

    public Song(string name, string artist, string path, List<int> playlists)
    {
        Name = name;
        Artist = artist;
        Path = path;
        Playlists = playlists;
    }
}