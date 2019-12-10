using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

//using DataAccess.Entities;
//using DataAccess;

namespace Monny
{
    /// <summary>
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class DreamInputWindow : Window
    {
        private MainWindow controller;
        private DreamPage dreamPage;

        public DreamInputWindow(MainWindow _mainWindow, DreamPage _dreamPage)
        {
            InitializeComponent();
            controller = _mainWindow;
            dreamPage = _dreamPage;

        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            dreamPage.dreamName = MyTextBox.Text;
            dreamPage.dreamPrice = Double.Parse(MyTextBox2.Text);
            dreamPage.dreamNamePage.Content = dreamPage.dreamName + " " + dreamPage.dreamPrice;
        }
    }
}
