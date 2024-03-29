﻿using System;
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
        public User user;
		public MainWindow()
		{
			InitializeComponent();
            //OpenPage(pages.expense);
            user = new User();
			OpenPage(pages.start);
   //         MonnyDbContext dbContext = new MonnyDbContext();
   //         List<Category> categories = dbContext.Set<Category>().ToList();
   //         string sfsf = "dgd";
        }
		public enum pages
		{
			start, signIn, signUp, home, expense, dream
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
            else if (pages == pages.expense)
            {
                //frame.Navigate(new ExpensePage(this));
            }
			else if (pages == pages.dream)
			{
				frame.Navigate(new DreamPage(this));
			}
			else
			{
				frame.Navigate(new StartPage(this));
			}
		}
		public BitmapImage SetImageSource(string imageName)
		{
			string baseProjectDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
			BitmapImage bitmap = new BitmapImage();
			bitmap.BeginInit();
			bitmap.UriSource = new Uri(System.IO.Path.Combine(baseProjectDirectory, "Images", imageName));
			bitmap.EndInit();
			return bitmap;
		}
	}
}
