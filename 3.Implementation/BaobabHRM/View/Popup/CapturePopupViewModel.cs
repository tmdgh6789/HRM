using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BaobabHRM
{
    public class CapturePopupViewModel : BindableBase
    {

        private BitmapImage m_Image;
        public BitmapImage Image
        {
            get
            {
                return m_Image;
            }
            set
            {
                m_Image = value;
                RaisePropertyChanged("Image");
            }
        }
        
        public DelegateCommand<UserControl> OkCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    Window.GetWindow(uc).DialogResult = true;
                });
            }
        }
    }
}
