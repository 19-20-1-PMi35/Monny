using System;
using System.Windows;
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
        private readonly MonnyDbContext dbContext;
        private string category;
		private MainWindow controller;
		private readonly DateTime date;
		private readonly ExpensePage expensePage;
        public InputWindow(MainWindow _mainWindow, ExpensePage _expensePage, string _category, DateTime _date)
        {
            InitializeComponent();
            //controller = _mainWindow;
            dbContext = new MonnyDbContext();
            title.Content += _category;
            category = _category;
			controller = _mainWindow;
			expensePage = _expensePage;
			date = _date;
		}

        private void Add_Click(object sender, RoutedEventArgs e)
        {
			// Validation of field price
			if (Double.TryParse(price.Text, out double amount))
			{
				CategoryRepository categories = new CategoryRepository();
				Category cat = categories.GetItems().Find(c => c.Name == category);

				// Adding new expense to database
				Expense expense = new Expense();
				expense.CategoryId = cat.Id;
				expense.AmountOfMoney = amount;
				expense.Date = date;
				expense.UserId = controller.user.Id;

				dbContext.Set<Expense>().Add(expense);
				dbContext.SaveChanges();

				// Updating progress bar
				expensePage.UpdateProgressBar(amount, date);
				this.Close();
			}
			else
			{
				MessageBox.Show("Wrong price entered");
			}			
        }
    }
}
