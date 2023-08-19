namespace Angelwave;

public struct Song {
    public string Name;
    public string Artist;
    public string Path;

    public Song(string name, string artist, string path)
    {
        Name = name;
        Artist = artist;
        Path = path;
    }
}