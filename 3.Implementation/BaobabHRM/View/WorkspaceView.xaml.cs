﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BaobabHRM
{
    /// <summary>
    /// Interaction logic for WorkspaceView.xaml
    /// </summary>
    public partial class WorkspaceView : UserControl
    {
        public WorkspaceView()
        {
            InitializeComponent();
            Loaded += LoadedRoutedEventHandler;
        }

        public void LoadedRoutedEventHandler(Object sender, RoutedEventArgs e)
        {
            Loaded -= LoadedRoutedEventHandler;
            streamPlayerControl.StartPlay(new Uri("rtmp://61.72.187.6/oflaDemo/testStream"));
        }
    }
}
