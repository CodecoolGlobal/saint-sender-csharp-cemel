using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace SaintSender.Core.Services
{
    public static class ClosingWindow
    {
        public static void CloseWindow(object obj)
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == obj) item.Close();
            }
        }
    }
}
