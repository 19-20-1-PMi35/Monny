using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DataAccess;
using DataAccess.Entities;

namespace Monny
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// Checks if string is numbers only
		/// </summary>
		/// <param name="bio"></param>
		/// <returns>
		/// True - if has no other symbols and False othrwise
		/// </returns>
		public static bool ContainNumbers(string _string)
		{
			return _string.Any(c => char.IsDigit(c));
		}
		/// <summary>
		/// Checks if string has @ sigh
		/// </summary>
		/// <param name="_string"></param>
		/// <returns>
		/// True - if has, False othrwise
		/// </returns>
		public static bool ContainAtSign(string _string)
		{
			return _string.Contains('@');
		}

		/// <summary>
		/// Replace textBox text with stars to provide secure password entering
		/// </summary>
		/// <param name="savedPassword">
		/// Variable where the real inputed text will be saved
		/// </param>
		/// <param name="password">
		/// textBox itself
		/// </param>
		/// <param name="doWork">
		/// Varibale to check if method should be executed
		/// </param>
		public static void HideTextBoxContentBehindStarts(ref string savedPassword, ref TextBox password, ref bool doWork)
		{
			if (doWork)
			{
				doWork = false;
				// If textBox text is empty and no data was entered
				if (savedPassword.Length != 0)
				{
					// If user add one char of password
					if (savedPassword.Length < password.Text.Length)
					{
						// Save that char
						savedPassword = String.Concat(savedPassword, password.Text[password.Text.Length - 1]);
						// Change textBox text by same amount of stars as password length
						// Trigers TextChanged event and that's the next call of it should be skipped
						password.Text = String.Concat(Enumerable.Repeat("*", password.Text.Length));
					}
					//  If user delete n char(s) of password
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
