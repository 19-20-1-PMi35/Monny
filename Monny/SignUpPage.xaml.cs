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

using DataAccess.Entities;
using DataAccess;

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

        private readonly MonnyDbContext dbContext = new MonnyDbContext();
		public SignUpPage(MainWindow _mainWindow)
		{
			InitializeComponent();
			controller = _mainWindow;
		}

		private void SignUpButton_Click(object sender, RoutedEventArgs e)
		{
			bool checkPassed = true;

			// Check data and write to db:
			checkPassed &= name.Text.Length != 0 && !App.ContainNumbers(name.Text);
			checkPassed &= surname.Text.Length != 0 && !App.ContainNumbers(surname.Text);
			checkPassed &= mail.Text.Length != 0 && App.ContainAtSign(mail.Text);
			checkPassed &= (String.Equals(savedPassword, savedConfirmPassword) && savedPassword.Length != 0 && savedConfirmPassword.Length != 0);

			if (checkPassed)
			{
				User u = dbContext.Set<User>().ToList().Find(u => (u.Email == mail.Text));
				if (u == null)
				{
					User user = new User
					{
						Name = name.Text,
						Surname = surname.Text,
						Email = mail.Text,
						Password = savedPassword
					};

					dbContext.Set<User>().Add(user);
					dbContext.SaveChanges();

					controller.OpenPage(MainWindow.pages.home);
				}
				else
				{
					MessageBox.Show("User with entered email is already exist");
				}
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
	}
}
