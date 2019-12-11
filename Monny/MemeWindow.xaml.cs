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
using DataAccess;
using DataAccess.Entities;

namespace Monny
{
    /// <summary>
    /// Interaction logic for MemeWindow.xaml
    /// </summary>
    public partial class MemeWindow : Window
    {
        private readonly MonnyDbContext database_variable = new MonnyDbContext();
        
        private MainWindow controller;

        private readonly HomePage homePage;
        public MemeWindow(MainWindow _mainWindow, HomePage _homePage)
        {
            InitializeComponent();
            controller = _mainWindow;
        }


        private void MemeGenerator_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            int randomi = r.Next(1, 7);
            memes.Source = controller.SetImageSource($"{randomi.ToString()}.png");
        }
    }
}
