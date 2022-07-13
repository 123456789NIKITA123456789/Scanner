using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApp1
{
    public interface Worker
    {
        void Attach(Window window);
        void Attach(Authentication authentication);
        void Detach(Window window);
        void Detach(Authentication authentication);
        void NotifyWin();
        void NotifyAuth_Cities();
        void NotifyAuth_Addresses();
    }

    public interface Window
    {
        void Update(List<Division> data);
    }

    public interface Authentication
    {
        void UpdateCities(List<City> cities);
        void UpdateAddresses(List<Address> addresses);
    }


    internal static class Program
    {
        public static DBworker dBworker = new DBworker();

        public static void ToMain(Control widget, Func<Control, Boolean> action)
        {
            if(widget.InvokeRequired)
            {
                Action safeChange = delegate
                {
                    action(widget);
                };
                widget.Invoke(safeChange);
            }
            else
            {
                action(widget);
            }
        }
            
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
