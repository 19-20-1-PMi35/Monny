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

using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace Monny
{
	/// <summary>
	/// Interaction logic for SignIn.xaml
	/// </summary>
	public partial class SignInPage : Page
	{
		private readonly UserRepository userRepository;

		private readonly MainWindow controller;

		private string savedPassword = "";
		private bool doWorkPassword = true;

		public SignInPage(MainWindow _mainWindow)
		{
			InitializeComponent();
			controller = _mainWindow;

			userRepository = new UserRepository();
		}
		private void SignInButton_Click(object sender, RoutedEventArgs e)
		{
			bool checkPassed = true;
			checkPassed &= mail.Text.Length != 0 && App.ContainAtSign(mail.Text);
			checkPassed &= password.Text.Length != 0;
			if (checkPassed)
			{
                User user = userRepository.GetItems().Find(u => (u.Email == mail.Text && u.Password == savedPassword));

                if (user == null)
                {
                    MessageBox.Show("Incorrect data");
                }
                else
                {
					controller.user = user;
					controller.OpenPage(MainWindow.pages.home);
                }
            }
			else
			{
				MessageBox.Show("Incorrect data");
			}
        }

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			controller.OpenPage(MainWindow.pages.start);
		}
		private void Password_TextChanged(object sender, TextChangedEventArgs e)
		{
			App.HideTextBoxContentBehindStarts(ref savedPassword, ref password, ref doWorkPassword);
			password.Focus();
			password.SelectionStart = password.Text.Length;
		}
	}
}
