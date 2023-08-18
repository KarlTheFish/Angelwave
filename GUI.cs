using Gdk;
using GLib;

#pragma warning disable CS0612 // Type or member is obsolete
namespace Angelwave;

using Gtk;
using System;


public class GUI
{
    public static void CreateGUI(){
        Application.Init();
        
        //Create a new window
        Window window = new Window("AngelWave");
        window.Resize(400, 400);

        window.DeleteEvent += delegate(object o, DeleteEventArgs args) { //Event called when window is closed
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

        //Attaching functions to the buttons
        
        find.ButtonPressEvent += FindHandler;
        
        //Adding the buttons to the layout
        layout.Attach(find, 0, 1, 0, 1);
        
        
        window.Add(layout);
        window.ShowAll();
        
        Application.Run();
    }
    
    [ConnectBefore]
    static void FindHandler(object obj, ButtonPressEventArgs args) {
        if (args.Event.Button == 1) {
            Console.WriteLine("Find button pressed");
        }
    }
}


