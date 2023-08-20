using System.Diagnostics.CodeAnalysis;
using Gdk;
using GLib;

#pragma warning disable CS0612 // Type or member is obsolete
namespace Angelwave;

using Gtk;
using System;


[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GUI
{ 
    static string filePath;
    public static VButtonBox songsList;
    public static void CreateGUI(){
        Application.Init();
        
        //Create a new window
        Window window = new Window("AngelWave");
        window.Resize(400, 400);

        window.DeleteEvent += delegate(object o, DeleteEventArgs args) { //Event called when window is closed
            //TODO: Redo this with dialog widget
            args.RetVal = true;
            var confirm = new Window("Confirmation");
            confirm.Resize(200, 100);
            var cont3 = new VBox();
            var buttons = new HBox();
            var yes = new Button("Yes");
            var no = new Button("No");
            var sure = new Label("Are you sure?");
            buttons.Add(yes);
            buttons.Add(no);
            cont3.Add(sure);
            cont3.Add(buttons);
            yes.ButtonPressEvent += ExitHandler;
            no.ButtonPressEvent += NoHandler;
            confirm.Add(cont3);
            confirm.ShowAll();

            [ConnectBefore]
            void ExitHandler(object obj, ButtonPressEventArgs args2)
            {
                if (args2.Event.Button == 1)
                {
                    args.RetVal = false;
                    Application.Quit();
                }
            }

            [ConnectBefore]
            void NoHandler(object obj, ButtonPressEventArgs args2)
            {
                if (args2.Event.Button == 1) confirm.Destroy();
            }
        };
        
        Table layout = new Table(3, 3, false);

        //Creating the buttons
        
        Button find = new Button("Find");
        find.Relief = ReliefStyle.None;
        find.Margin = 0;
        
        Button newPlaylist = new Button("New Playlist");
        newPlaylist.Relief = ReliefStyle.None;
        newPlaylist.Margin = 0;
        
        
        //Other layout things
        
        Frame optionsMenu = new Frame("Actions");
        Frame playLists = new Frame("Playlists");
        Frame foundSongs = new Frame("Songs");

        ActionBar actions = new ActionBar(); //TODO: Add music controls and progress bar to the action bar
        
        songsList = new VButtonBox();
        songsList.Homogeneous = true;
        
        VButtonBox options = new VButtonBox();

        options.PackStart(find, false, false, 0);
        options.PackStart(newPlaylist, false, false, 0);
        options.PackEnd(new Box(Orientation.Vertical, 0), true, true, 0);
        
        
        optionsMenu.BorderWidth = 0;
        optionsMenu.Margin = 0;
        options.Margin = 0;


        optionsMenu.Add(options);
        foundSongs.Add(songsList);

        //Attaching functions to the buttons
        
        find.ButtonPressEvent += FindHandler;
        
        //Adding the buttons to the layout
        layout.Attach(optionsMenu, 0, 1, 0, 2);
        layout.Attach(playLists, 1, 2, 0, 2);
        layout.Attach(foundSongs,2, 3, 0, 2);
        layout.Attach(actions, 0, 3, 2, 3);
        

        window.Add(layout);
        window.ShowAll();
        
        Application.Run();
    }
    
    [ConnectBefore]
    static void FindHandler(object obj, ButtonPressEventArgs args) {
        if (args.Event.Button == 1) {
            SongFinder();
        }
    }
    
    static Window finder;
    static Entry entry2;
    static Label InvalidFilepathLabel = new Label("Please insert a valid path!");
    public static CheckButton subDirSearch;
    public static bool subDirSearchToggled;

    static void SongFinder() {
        subDirSearchToggled = false;
        entry2 = new Entry();
        finder = new Window("Finder");
        finder.Resize(200, 200);
        finder.Resizable = false;
        VBox finderCont = new VBox();
        finderCont.Margin = 20;
        Label label = new Label("Insert music file path:");
        entry2.MarginTop = 20;
        entry2.MarginBottom = 20;
        Button confirm = new Button("Confirm");
        confirm.MarginTop = 20;
        confirm.MarginBottom = 20;
        subDirSearch = new CheckButton("Also search subdirectories");
        subDirSearch.Toggled += subTogHandler!;
        
        confirm.ButtonPressEvent += FinderHandler; //BUG: Sometimes console throws error if the button is pressed twice. Investigate.

        finderCont.Add(label);
        finderCont.Add(entry2);
        finderCont.Add(subDirSearch);
        finderCont.Add(confirm);
        
        finder.Add(finderCont);
        
        finder.ShowAll();
        
        finderCont.Add(InvalidFilepathLabel);
    }
    
    [ConnectBefore]
    static void FinderHandler(object obj, ButtonPressEventArgs args) {
        if (args.Event.Button == 1) {
            filePath = entry2.Text;
            if (filePath == "") {
                if (InvalidFilepathLabel.Visible == false) {
                    InvalidFilepathLabel.Show();
                }
            }
            else {
                try {
                    Finder.FindAllSongs(filePath);
                }
                catch (Exception e) {
                    Console.WriteLine("No songs were found!");
                }
                finally {
                    finder.Destroy();
                    finder = new Window("Finder");
                }
            }
        }
    }
    
    [ConnectBefore]
    static void subTogHandler(object obj, EventArgs args) {
        subDirSearchToggled = !subDirSearchToggled;
    }
}


