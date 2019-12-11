using System;
using System.Collections.Generic;
using System.Windows;
using DataAccess.Entities;
using DataAccess;
using DataAccess.Repositories;

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

			// Set ComboBox list Item with user's custom categories
			ExpenseRepository repository = new ExpenseRepository();
			// List where used custom categories ids are being saved
			List<int> usedId = new List<int>();
			foreach (Expense e in repository.GetItems())
			{
				// Checks if e is current user expense
				// e.CategoryId > 7 because custom categories have id greater than 7
				if (e.UserId == controller.user.Id && e.CategoryId > 7)
				{
					// Check if category hasn't been used already
					if (!usedId.Exists(i => i == e.CategoryId))
					{
						CategoryRepository categories = new CategoryRepository();
						// Add category to ComboBox Items
						customCategories.Items.Add(categories.GetItem(e.CategoryId).Name);

						usedId.Add(e.CategoryId);
					}
				}
			}
		}
		/// <summary>
		/// Add new expense in custom user's category to database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Add_Click(object sender, RoutedEventArgs e)
		{
			// Checks if custom category was selected
			if (customCategories.SelectedIndex >= 0)
			{
				// Validation for field mainText (it is where price was entered)
				if (Double.TryParse(mainText.Text, out double amount))
				{
					// Adding to database
					CategoryRepository categories = new CategoryRepository();
					Category cat = categories.GetItems().Find(c => c.Name == customCategories.Items[customCategories.SelectedIndex].ToString());

					Expense expense = new Expense
					{
						CategoryId = cat.Id,
						AmountOfMoney = amount,
						Date = date,
						UserId = controller.user.Id
					};

					ExpenseRepository repository = new ExpenseRepository();
					repository.Create(expense);

					// Updating progress bar
					expensePage.SetProgressBar(date);
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
		/// <summary>
		/// Change UI for creating new category
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddCategory_Click(object sender, RoutedEventArgs e)
		{
			add.Visibility = Visibility.Collapsed;
			addCategory.Visibility = Visibility.Collapsed;
			create.Visibility = Visibility.Visible;
			cancel.Visibility = Visibility.Visible;
			text.Content = "Enter name of new category:";
		}
		/// <summary>
		/// Change UI to default
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			add.Visibility = Visibility.Visible;
			addCategory.Visibility = Visibility.Visible;
			create.Visibility = Visibility.Collapsed;
			cancel.Visibility = Visibility.Collapsed;
			text.Content = "Price:";
		}
		/// <summary>
		/// Creating new category
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Create_Click(object sender, RoutedEventArgs e)
		{
			// Validation for new category name
			if (!App.ContainNumbers(mainText.Text) && mainText.Text.Length != 0)
			{
				Category customCategory = new Category
				{
					Name = mainText.Text
				};

				CategoryRepository repository = new CategoryRepository();
				repository.Create(customCategory);

				mainText.Text = "";
				// Updating ComboBox Items
				UpdateComboBox(customCategory.Name);
				
				// Change UI to default
				Cancel_Click(sender, e);
			}
			else
			{
				MessageBox.Show("Incorrect name of category.");
			}
		}
		// Update ComboBox Items
		private void UpdateComboBox(string categoryName)
		{
			customCategories.Items.Add(categoryName);
		}
	}
}
