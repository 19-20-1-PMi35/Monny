using System;
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

namespace Monny
{
	/// <summary>
	/// Interaction logic for HomePage.xaml
	/// </summary>
	public partial class HomePage : Page
	{
		private MainWindow controller;
		public HomePage(MainWindow _mainWindow)
		{
			InitializeComponent();
			controller = _mainWindow;
		}
		private void OpenMenuButton_Click(object sender, RoutedEventArgs e)
		{
			OpenMenuButton.Visibility = Visibility.Collapsed;
			CloseMenuButton.Visibility = Visibility.Visible;
		}
		private void CloseMenuButton_Click(object sender, RoutedEventArgs e)
		{
			OpenMenuButton.Visibility = Visibility.Visible;
			CloseMenuButton.Visibility = Visibility.Collapsed;
		}

        private void ListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
			frame.Navigate(new ExpensePage(controller));
            //controller.OpenPage(MainWindow.pages.expense);
        }
    }
}
