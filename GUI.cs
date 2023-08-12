using Gdk;

#pragma warning disable CS0612 // Type or member is obsolete
namespace Angelwave;

using Gtk;
using System;


public class GUI
{
    public static void CreateGUI(){
        Application.Init();
        
        //Create a new window
        Window window = new Window("Baby's First GTK# Application!");
        window.Resize(400, 400);

        window.DeleteEvent += delegate(object o, DeleteEventArgs args) {
            args.RetVal = true;
            Window confirm = new Window("Confirmation");
            confirm.Resize(200, 100);
            VBox cont3 = new VBox();
            HBox buttons = new HBox();
            Button yes = new Button("Yes");
            Button no = new Button("No");
            Label sure = new Label("Are you sure?");
            buttons.Add(yes);
            buttons.Add(no);
            cont3.Add(sure);
            cont3.Add(buttons);
            yes.ButtonPressEvent += new ButtonPressEventHandler(ExitHandler);
            no.ButtonPressEvent += new ButtonPressEventHandler(NoHandler);
            confirm.Add(cont3);
            confirm.ShowAll();
            
            [GLib.ConnectBefore]
            void ExitHandler(object obj, ButtonPressEventArgs args2){
                if (args2.Event.Type == EventType.ButtonPress){
                    args.RetVal = false;
                    Application.Quit();
                }
            }

            [GLib.ConnectBefore]
            void NoHandler(object obj, ButtonPressEventArgs args2){
                if (args2.Event.Type == EventType.ButtonPress){
                    confirm.Destroy();
                }
            }
        };

        VBox cont2 = new VBox(false, 2);

        ButtonBox bBox = new ButtonBox(Orientation.Horizontal);

        Button finder2 = new Button("Find Songs");
        
        Button button3 = new Button("Random Button");
        bBox.Add(finder2); bBox.Add(button3);

        bBox.Margin = 10;

        button3.ButtonPressEvent += new ButtonPressEventHandler(ButtonPressHandler);
        
        //Create a label with some text in it
        Label label = new Label();
        label.Text = "Hello World!";
        
        cont2.Add(label);
        cont2.Add(bBox);
        
        //Add label to the window
        window.Add(cont2);
        
        window.ShowAll();
        
        Application.Run();
    }
    
    static void AddButton(HBox box, string givenName){
        box.PackStart(new Button (givenName), false, true, 2);
    }

    [GLib.ConnectBefore] //Don't know what it actually does, but event doesn't work properly without it
    static void ButtonPressHandler(object obj, ButtonPressEventArgs args){
        if (args.Event.Type == EventType.ButtonPress){
            Window pressWindow = new Window("Button pressed");
            pressWindow.Resize(200, 200);
            Label lbl = new Label("Hello World!");
            pressWindow.Add(lbl);
            pressWindow.ShowNow();
        }
        
        
    }
}
