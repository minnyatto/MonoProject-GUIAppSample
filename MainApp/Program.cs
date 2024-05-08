using System;
using Gtk;
using DBus;
using System.Threading;



namespace test5
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            MyAppManager myAppManager = new MyAppManager();
            myAppManager.start();

            Console.Write("test main");
            Application.Init();
            MainWindow win = new MainWindow(ref myAppManager);
            win.Title = "Device Viewer";
            win.Show();
            //var address = Environment.GetEnvironmentVariable("DBUS_SESSION_BUS_ADDRESS");
            //var bus = Bus.Open(address);
            Application.Run();
            myAppManager.stop();

        }



    }

    public class DataPool
    {
        public delegate void NotifyValueChanged();

        public event NotifyValueChanged timeCountChanged;

        private int timeCount = 0;

        public int TimeCount
        {
            get { return timeCount; }
            set {
                if (timeCount != value)
                {
                    timeCount = value;
                    timeCountChanged?.Invoke();
                }
            }
        }

        public DataPool()
        {

        }
    }

    public class MyAppManager
    {
        public Mutex mtx = new Mutex();
        public DataPool datapool = new DataPool();
        Thread threadhnd_databuffering = null;
        public MyAppManager()
        {

        }


        public void start()
        {
            threadhnd_databuffering = new Thread(new ThreadStart(this.threadmth_databuffering));
            threadhnd_databuffering.Start();
        }


        public void stop()
        {
            threadhnd_databuffering.Abort();
        }

        public void threadmth_databuffering()
        {
            while (true)
            {
                mtx.WaitOne();
                this.datapool.TimeCount += 1;
                mtx.ReleaseMutex();
                Console.Write("test thread");
                Thread.Sleep(1000);
            }
        }
    }
}
