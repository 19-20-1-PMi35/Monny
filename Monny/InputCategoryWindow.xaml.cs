using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccess.Entities;
using DataAccess;
using DataAccess.Repositories;
using System.Linq;

namespace Monny
{
	/// <summary>
	/// Interaction logic for InputCategoryWindow.xaml
	/// </summary>
	public partial class InputCategoryWindow : Window
	{
		private readonly MonnyDbContext dbContext;
		private readonly MainWindow controller;
		private readonly DateTime date;
		private readonly ExpensePage expensePage;
		public InputCategoryWindow(MainWindow _mainWindow, ExpensePage _expensePage, DateTime _date)
		{
			InitializeComponent();
			dbContext = new MonnyDbContext();
			controller = _mainWindow;
			expensePage = _expensePage;
			date = _date;

			ExpenseRepository repository = new ExpenseRepository();
			List<int> usedId = new List<int>();
			foreach (Expense e in repository.GetItems())
			{
				if (e.UserId == controller.user.Id && e.CategoryId > 7)
				{
					if (!usedId.Exists(i => i == e.CategoryId))
					{
						CategoryRepository categories = new CategoryRepository();
						customCategories.Items.Add(categories.GetItem(e.CategoryId).Name);

						usedId.Add(e.CategoryId);
					}
				}
			}
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			if (customCategories.SelectedIndex >= 0)
			{
				if (Double.TryParse(mainText.Text, out double amount))
				{
					Category cat = dbContext.Set<Category>().ToList().Find(c => c.Name == customCategories.Items[customCategories.SelectedIndex].ToString());

					Expense expense = new Expense
					{
						CategoryId = cat.Id,
						AmountOfMoney = amount,
						Date = date,
						UserId = controller.user.Id
					};

					ExpenseRepository repository = new ExpenseRepository();
					repository.Create(expense);

					expensePage.UpdateProgressBar(amount, date);
					this.Close();
				}
				else
				{
					MessageBox.Show("Wrong price entered");
				}
			}
			else
			{
				MessageBox.Show("Choose category from menu first.");
			}
		}

		private void AddCategory_Click(object sender, RoutedEventArgs e)
		{
			add.Visibility = Visibility.Collapsed;
			addCategory.Visibility = Visibility.Collapsed;
			create.Visibility = Visibility.Visible;
			cancel.Visibility = Visibility.Visible;
			text.Content = "Enter name of new category:";
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			add.Visibility = Visibility.Visible;
			addCategory.Visibility = Visibility.Visible;
			create.Visibility = Visibility.Collapsed;
			cancel.Visibility = Visibility.Collapsed;
			text.Content = "Price:";
		}
		private void Create_Click(object sender, RoutedEventArgs e)
		{
			if (!App.ContainNumbers(mainText.Text) && mainText.Text.Length != 0)
			{
				Category customCategory = new Category
				{
					Name = mainText.Text
				};

				CategoryRepository repository = new CategoryRepository();
				repository.Create(customCategory);

				mainText.Text = "";
				UpdateComboBox(customCategory.Name);

				Cancel_Click(sender, e);
			}
			else
			{
				MessageBox.Show("Incorrect name of category.");
			}
		}
		private void UpdateComboBox(string categoryName)
		{
			customCategories.Items.Add(categoryName);
		}
	}
}
