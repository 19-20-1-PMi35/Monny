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
		private string savedPassword = "";
		private bool doWorkPassword = true;

		private string savedConfirmPassword = "";
		private bool doWorkConfirmPassword = true;
		public SignUpPage(MainWindow _mainWindow)
		{
			InitializeComponent();
			controller = _mainWindow;
		}

		private void SignUpButton_Click(object sender, RoutedEventArgs e)
		{
			bool checkPassed = true;
			// Check data and write to db:
			checkPassed &= !App.ContainNumbers(name.Text);
			checkPassed &= !App.ContainNumbers(surname.Text);
			checkPassed &= App.ContainAtSign(mail.Text);
			checkPassed &= (String.Equals(savedPassword, savedConfirmPassword) && savedPassword.Length != 0 && savedConfirmPassword.Length != 0);
			
			if (checkPassed)
			{
				MessageBox.Show("Check data succeed");
				MessageBox.Show(savedPassword + " --- " + savedConfirmPassword);
			}
			else
			{
				MessageBox.Show("Check data failed");
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

		private void confirmPassword_TextChanged(object sender, TextChangedEventArgs e)
		{
			App.HideTextBoxContentBehindStarts(ref savedConfirmPassword, ref confirmPassword, ref doWorkConfirmPassword);
			confirmPassword.Focus();
			confirmPassword.SelectionStart = confirmPassword.Text.Length;
		}

		private void name_TextChanged(object sender, TextChangedEventArgs e)
		{
		}
	}
}
