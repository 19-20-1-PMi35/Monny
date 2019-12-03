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
        public ExpensePage()
        {
            InitializeComponent();
        }

        private void food_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("food");
        }
        private void clothes_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("clothes");
        }

        private void transport_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("transport");
        }

        private void caffe_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("caffe");
        }

        private void traveling_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("traveling");
        }

        private void health_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("health");
        }

        private void entertainments_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("entertainments");
        }

        private void other_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("other");
        }
        private static void OpenInputWindow(string category)
        {
            InputWindow inputWindow = new InputWindow();
            inputWindow.title.Content += category;
            inputWindow.Show();
        }
    }
}
