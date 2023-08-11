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
        window.Resize(200, 200);

        VBox cont2 = new VBox(false, 2);

        HBox cont = new HBox(false, 4);
      
        AddButton(cont, "Button1");
        AddButton(cont, "Button2");
        AddButton(cont, "Button3");
        
        //Create a label with some text in it
        Label label = new Label();
        label.Text = "Hello World!";
        
        cont2.Add(label);
        cont2.Add(cont);
        
        //Add label to the window
        window.Add(cont2);
        
        //Show everything
        window.ShowAll();
        
        Application.Run();
    }
    
    static void AddButton(HBox box, string givenName){
        box.PackStart(new Button (givenName), false, true, 2);
    }
}
