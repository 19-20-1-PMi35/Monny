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
using System.Linq;

using DataAccess.Entities;
using DataAccess;
using DataAccess.Repositories;

namespace Monny
{
    /// <summary>
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
		private readonly CategoryRepository categoryRepository;
		private readonly ExpenseRepository expenseRepository;

        private string category;
		private MainWindow controller;
		private readonly ExpensePage expensePage;
        public InputWindow(MainWindow _mainWindow, ExpensePage _expensePage, string _category)
        {
            InitializeComponent();

			categoryRepository = new CategoryRepository();
			expenseRepository = new ExpenseRepository();

            title.Content += _category;
            category = _category;
			controller = _mainWindow;
			expensePage = _expensePage;
		}

        private void add_Click(object sender, RoutedEventArgs e)
        {
			if (Double.TryParse(price.Text, out double amount))
			{
				Category currentCategory = categoryRepository.GetItems().Find(c => c.Name == category);

				Expense expense = new Expense();
				expense.CategoryId = currentCategory.Id;
				expense.AmountOfMoney = Double.Parse(price.Text);
				expense.Date = DateTime.Now;
				expense.UserId = controller.user.Id;

				expenseRepository.Create(expense);

				expensePage.UpdateProgressBar();
				this.Close();
			}
			else
			{
				MessageBox.Show("Wrong price entered");
			}			
        }
    }
}
