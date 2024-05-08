using System;
using Gtk;
using test5;

namespace test5.Widgets
{
    public partial class WindowDevice : Gtk.Window
    {
        public MyAppManager myAppManager = null;

        private int id = -1;
        private string devicename = "";


        public WindowDevice(int id, string devicename, ref MyAppManager appmng) :
                base(Gtk.WindowType.Toplevel)
        {
            myAppManager = appmng;

            AccelGroup agrp = new AccelGroup();
            AddAccelGroup(agrp);
            MenuBar menubar = onCreateMenuBar(agrp);
            VBox v = new VBox();
            v.PackStart(menubar, false, false, 0);
            Add(v);

            this.Build();
            this.id = id;
            this.devicename = devicename;
            this.Title = devicename;
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
            item.Activated += Open_Activated;
            file_menu.Append(item);
            // file > save as
            item = new ImageMenuItem(Stock.SaveAs, agrp);
            item.Activated += Open_Activated;
            file_menu.Append(item);
            // file > properties
            item = new ImageMenuItem(Stock.Properties, agrp);
            file_menu.Append(item);
            // file > ----------------
            file_menu.Append(new SeparatorMenuItem());
            // file > Quit
            item = new ImageMenuItem(Stock.Close, agrp);
            item.Activated += FileClose_Activated;
            file_menu.Append(item);
            // edit menu
            Menu edit_menu = new Menu();
            item = new MenuItem("_Edit");
            item.Submenu = edit_menu;
            menubar.Append(item);
            item = new MenuItem("_Transform");
            Menu transform_menu = new Menu();
            item.Submenu = transform_menu;
            edit_menu.Append(item);
            item = new MenuItem("_Rotate");
            // custom accelerator
            item.AddAccelerator("activate", agrp,
              new AccelKey(Gdk.Key.R, Gdk.ModifierType.ControlMask,
                AccelFlags.Visible));
            item.Activated += Rotate_Activated;
            transform_menu.Append(item);
            item = new MenuItem("_Flip");
            transform_menu.Append(item);

            return menubar;
        }

        public static void Open_Activated(object o, EventArgs e)
        {
            System.Console.WriteLine("Open");
        }

        public static void Rotate_Activated(object o, EventArgs e)
        {
            System.Console.WriteLine("Rotate");
        }

        public void FileClose_Activated(object o, EventArgs e)
        {
            this.Destroy();
        }
    }
}
