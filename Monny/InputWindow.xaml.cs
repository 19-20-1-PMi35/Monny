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
			if (Double.TryParse(price.Text, out double amount))
			{
				Category cat = dbContext.Set<Category>().ToList().Find(c => c.Name == category);

				Expense expense = new Expense();
				expense.CategoryId = cat.Id;
				expense.AmountOfMoney = Double.Parse(price.Text);
				expense.Date = date;
				expense.UserId = controller.user.Id;

				dbContext.Set<Expense>().Add(expense);
				dbContext.SaveChanges();

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
