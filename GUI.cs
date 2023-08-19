using Gdk;
using GLib;

#pragma warning disable CS0612 // Type or member is obsolete
namespace Angelwave;

using Gtk;
using System;


public class GUI
{ 
    static string filePath;
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

        VButtonBox options = new VButtonBox();
        //options.Add(find);
        //options.Add(newPlaylist);
        
        options.PackStart(find, false, false, 0);
        options.PackStart(newPlaylist, false, false, 0);
        options.PackEnd(new Box(Orientation.Vertical, 0), true, true, 0);
        
        
        optionsMenu.BorderWidth = 0;
        optionsMenu.Margin = 0;
        options.Margin = 0;


        optionsMenu.Add(options);

        //Attaching functions to the buttons
        
        find.ButtonPressEvent += FindHandler;
        
        //Adding the buttons to the layout
        layout.Attach(optionsMenu, 0, 1, 0, 2);
        layout.Attach(playLists, 1, 2, 0, 2);
        layout.Attach(foundSongs,2, 3, 0, 2);
        

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

    static Entry entry2 = new Entry();

    static void SongFinder() { //TODO: Handle exceptions 
        Window finder = new Window("Finder");
        finder.Resize(200, 200);
        
        VBox cont = new VBox();
        cont.Margin = 20;
        Label label = new Label("Insert music file path:");
        entry2.MarginTop = 20;
        entry2.MarginBottom = 20;
        Button confirm = new Button("Confirm");
        
        confirm.ButtonPressEvent += FinderHandler;

        cont.Add(label);
        cont.Add(entry2);
        cont.Add(confirm);
        
        finder.Add(cont);
        
        finder.ShowAll();
    }
    
    [ConnectBefore]
    static void FinderHandler(object obj, ButtonPressEventArgs args) {
        if (args.Event.Button == 1) {
            filePath = entry2.Text;
            if (filePath == "") {
                Console.WriteLine("No filepath given");
            }
            else {
                Finder.FindAllSongs(filePath);
            }
        }
    }
}


