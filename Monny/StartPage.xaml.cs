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
using System.IO;

using DataAccess.Entities;
using DataAccess;

namespace Monny
{
	/// <summary>
	/// Interaction logic for StartPage.xaml
	/// </summary>
	public partial class StartPage : Page
	{
		private readonly MainWindow controller;
		public StartPage(MainWindow _mainWindow)
		{
			InitializeComponent();

			controller = _mainWindow;

			logo.Source = controller.SetImageSource("logo.png");
		}

		private void SignIn_Click(object sender, RoutedEventArgs e)
		{
			controller.OpenPage(MainWindow.pages.signIn);
		}

		private void SignUp_Click(object sender, RoutedEventArgs e)
		{
			controller.OpenPage(MainWindow.pages.signUp);
		}
	}
}
