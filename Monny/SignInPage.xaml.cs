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
	/// Interaction logic for SignIn.xaml
	/// </summary>
	public partial class SignInPage : Page
	{
		private MainWindow controller;
		private string savedPassword = "";
		private bool doWorkPassword = true;
		public SignInPage(MainWindow _mainWindow)
		{
			InitializeComponent();
			controller = _mainWindow;
		}
		private void SignInButton_Click(object sender, RoutedEventArgs e)
		{
			bool checkPassed = true;
			checkPassed &= App.ContainAtSign(mail.Text);
			if (checkPassed)
			{
				controller.OpenPage(MainWindow.pages.home);
			}
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			controller.OpenPage(MainWindow.pages.start);
		}
		private void password_TextChanged(object sender, TextChangedEventArgs e)
		{
			App.HideTextBoxContentBehindStarts(ref savedPassword, ref password, ref doWorkPassword);
			password.Focus();
			password.SelectionStart = password.Text.Length;
		}
	}
}
