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
	/// Interaction logic for SignUp.xaml
	/// </summary>
	public partial class SignUpPage : Page
	{
		private MainWindow controller;
		public SignUpPage(MainWindow _mainWindow)
		{
			InitializeComponent();
			controller = _mainWindow;
		}

		private void SignUpButton_Click(object sender, RoutedEventArgs e)
		{
			// Check password and write to db
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			controller.OpenPage(MainWindow.pages.start);
		}
	}
}
