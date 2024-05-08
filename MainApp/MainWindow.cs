using System;
using Gtk;
using test5.Widgets;
using test5;

public partial class MainWindow : Gtk.Window
{

    public MyAppManager myAppManager = null;


    public MainWindow(ref MyAppManager appmng) : base(Gtk.WindowType.Toplevel)
    {
        myAppManager = appmng;
        DeleteEvent += Window_Deleted;
        AccelGroup agrp = new AccelGroup();
        AddAccelGroup(agrp);
        MenuBar menubar = onCreateMenuBar(agrp);
        VBox v = new VBox();
        v.PackStart(menubar, false, false, 0);
        Add(v);
        Build();
    }

    private MenuBar onCreateMenuBar(AccelGroup agrp)
    {

        // menu bar
        MenuBar menubar = new MenuBar();
        // file menu
        Menu file_menu = new Menu();
        MenuItem item = new MenuItem("_File");
        item.Submenu = file_menu;
        menubar.Append(item);
        // file > new
        item = new ImageMenuItem(Stock.New, agrp);
        file_menu.Append(item);
        Menu subMenu_new = new Menu();
        item.Submenu = subMenu_new;
        item = new MenuItem("_Device");
        item.Activated += FileNewDevice_Activated;
        subMenu_new.Append(item);
        // file > save as
        item = new ImageMenuItem(Stock.SaveAs, agrp);
        item.AddAccelerator("activate", agrp, new AccelKey(Gdk.Key.S, Gdk.ModifierType.ControlMask | Gdk.ModifierType.ShiftMask, AccelFlags.Visible));

        item.Activated += FileSaveAs_Activated;
        file_menu.Append(item);
        // file > properties
        item = new ImageMenuItem(Stock.Properties, agrp);
        item.Activated += FileProperties_Activated;
        file_menu.Append(item);
        // file > ----------------
        file_menu.Append(new SeparatorMenuItem());
        // file > Quit
        item = new ImageMenuItem(Stock.Quit, agrp);
        item.Activated += Quit_Activated;
        file_menu.Append(item);

        // help menu
        Menu edit_menu = new Menu();
        item = new MenuItem("_Help");
        item.Submenu = edit_menu;
        menubar.Append(item);
        item = new MenuItem("_Version info");
        item.Activated += HelpVersionInfo_Activated;
        edit_menu.Append(item);

        return menubar;
    }


    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }


    public void FileNewDevice_Activated(object o, EventArgs e)
    {
        System.Console.WriteLine("MouseClick: File>New>Device");
        WindowDevice _dev = new WindowDevice(0, "Device 0", ref this.myAppManager);
        _dev.Show();

    }
    public static void FileSaveAs_Activated(object o, EventArgs e)
    {
        System.Console.WriteLine("MouseClick: File>SaveAs");
    }
    public static void FileProperties_Activated(object o, EventArgs e)
    {
        System.Console.WriteLine("MouseClick: File>Properties");
    }


    public static void HelpVersionInfo_Activated(object o, EventArgs e)
    {
        System.Console.WriteLine("MouseClick: Help>VersionInfo");
    }

    public static void Quit_Activated(object o, EventArgs e)
    {
        Application.Quit();
    }

    public static void Window_Deleted(object o, DeleteEventArgs e)
    {
        Application.Quit();
    }
}
