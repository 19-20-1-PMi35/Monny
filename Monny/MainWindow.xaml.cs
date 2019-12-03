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
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			OpenPage(pages.start);
			//MonnyDbContext dbContext = new MonnyDbContext();
			//List<Category> categories = dbContext.Set<Category>().ToList();
			//string sfsf = "dgd";
		}
		public enum pages
		{
			start, signIn, signUp, home
		}

		public void OpenPage(pages pages)
		{
			if (pages == pages.signIn)
			{
				frame.Navigate(new SignInPage(this));
			}
			else if (pages == pages.signUp)
			{
				frame.Navigate(new SignUpPage(this));
			}
			else if (pages == pages.home)
			{
				frame.Navigate(new HomePage(this));
			}
			else
			{
				frame.Navigate(new StartPage(this));
			}
		}
	}
}
