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
            monny1.Source = controller.SetImageSource("banner3.png");
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

    private void ListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
       frame.Navigate(new IncomePage(controller));
    }

    private void ListView_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
    {
       frame.Navigate(new ExpensePage(controller));
    }
    
    private void ListView_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
		{
			frame.Navigate(new StatisticPage(controller));
		}
        
    private void monny1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
       MemeWindow meme = new MemeWindow(controller, this);
       meme.ShowDialog();
    }
	}
}
