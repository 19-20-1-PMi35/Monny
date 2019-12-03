using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Monny
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static bool ContainNumbers(string bio)
		{
			return bio.Any(c => char.IsDigit(c)) && bio.Length != 0;
		}
		public static bool ContainAtSign(string bio) // Check if mail has @ sigh
		{
			return bio.Contains('@') && bio.Length != 0;
		}
		public static void HideTextBoxContentBehindStarts(ref string savedPassword, ref TextBox password, ref bool doWork)
		{
			if (doWork)
			{
				doWork = false;
				if (savedPassword.Length != 0)
				{
					if (savedPassword.Length < password.Text.Length)
					{
						savedPassword = String.Concat(savedPassword, password.Text[password.Text.Length - 1]);
						password.Text = String.Concat(Enumerable.Repeat("*", password.Text.Length));
					}
					else
					{
						int repeat = savedPassword.Length - password.Text.Length;
						for (int i = 0; i < repeat; i++)
						{
							savedPassword = savedPassword.Remove(savedPassword.Length - 1);
						}
						doWork = true;
					}
				}
				else
				{
					savedPassword = String.Concat(savedPassword, password.Text[password.Text.Length - 1]);
					password.Text = String.Concat(Enumerable.Repeat("*", password.Text.Length));
				}
			}
			else
			{
				doWork = true;
			}
		}
	}
}
