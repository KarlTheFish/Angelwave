using System.Xml.Serialization;

namespace Angelwave;

[XmlRoot("AllSongs")]
public struct Song {
    [XmlElement(ElementName = "Title")]
    public string Title { get; set; }
    
    [XmlElement(ElementName = "Artist")]
    public string Artist { get; set; }
    
    [XmlElement(ElementName = "Path")]
    public string Path { get; set; }
    
    [XmlElement(ElementName = "Playlists")]
    public List<int> Playlists { get; set; }

    public Song(string title, string artist, string path, List<int> playlists)
    {
        Title = title;
        Artist = artist;
        Path = path;
        Playlists = playlists;
    }
}