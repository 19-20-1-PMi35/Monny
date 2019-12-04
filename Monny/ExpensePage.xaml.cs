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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monny
{
    /// <summary>
    /// Interaction logic for ExpensePage.xaml
    /// </summary>
    public partial class ExpensePage : Page
    {
        private MainWindow controller;
        public ExpensePage(MainWindow _mainWindow)
        {
            InitializeComponent();
            controller = _mainWindow;
            
        }

        private void food_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Food");
        }
        private void clothes_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Clothes");
        }

        private void transport_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Transport");
        }

        private void caffe_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Caffe");
        }

        private void traveling_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Traveling");
        }

        private void health_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Health");
        }

        private void entertainments_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Entertainments");
        }

        private void other_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Other");
        }
        private void OpenInputWindow(string category)
        {
            InputWindow inputWindow = new InputWindow(controller, category);
            inputWindow.Show();
        }
    }
}
