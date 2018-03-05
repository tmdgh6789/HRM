using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BaobabHRM
{
    public class WindowHelper
    {
        /// <summary>
        /// create popup window
        /// </summary>
        /// <param name="view"></param>
        /// <param name="title"></param>
        /// <param name="isModal"></param>
        public static bool? CreatePopup(UserControl view, string title, bool isModal = false)
        {
            var window = new PopupWindow();
            window.Owner = System.Windows.Application.Current.MainWindow;
            window.BODY.Children.Add(view);
            window.TITLE.Text = title;

            if (isModal)
            {
                return window.ShowDialog();
            }
            else
            {
                window.Show();
                return false;
            }
        }
    }
}
