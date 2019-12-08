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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace Monny
{
    /// <summary>
    /// Interaction logic for ExpensePage.xaml
    /// </summary>
    public partial class ExpensePage : Page
    {
        private readonly ExpenseRepository expenseRepository;
		public MainWindow controller;
		public DateTime now = DateTime.Now;
		public ExpensePage(MainWindow _mainWindow)
        {
            InitializeComponent();
			controller = _mainWindow;
            expenseRepository = new ExpenseRepository();

<<<<<<< Updated upstream
            // Set progress bar width
            double sum = expenseRepository.GetItems().Where(e => (e.UserId == controller.user.Id && e.Date.Month == now.Month)).Sum(e => e.AmountOfMoney);
            progressBar.Value = sum;
=======
			// Set progress bar width
			double sum = dbContext.Set<Expense>().ToList().Where(e => (e.UserId == controller.user.Id && e.Date.Month == now.Month)).Sum(e => e.AmountOfMoney);
			progressBar.Value = sum;
>>>>>>> Stashed changes
			// need to be done: setting max value of progress bar
			//progressBar.Maximum = ?
		}
        private void Food_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Food");
		}
        private void Clothes_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Clothes");
		}
        private void Transport_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Transport");
		}
        private void Caffe_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Caffe");
		}
        private void Traveling_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Traveling");
		}
        private void Health_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Health");
		}
        private void Entertainments_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Entertainments");
		}
        private void Other_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Other");
        }
        private void OpenInputWindow(string category)
        {
			if (datePicker.SelectedDate != null)
			{
				if (datePicker.SelectedDate.Value < DateTime.Now)
				{
					InputWindow inputWindow = new InputWindow(controller, this, category, datePicker.SelectedDate.Value);
					inputWindow.ShowDialog();
				}
				else
				{
					MessageBox.Show($"Choose correct date (past or current).\r\n{datePicker.SelectedDate.Value.ToShortDateString()} was selected.\r\n{DateTime.Now.ToShortDateString()} - today.");
				}
			}
			else
			{
				MessageBox.Show("Choose date, please.");
			}
        }
		public void UpdateProgressBar()
		{
			Expense added = expenseRepository.GetItems().Where(e => (e.UserId == controller.user.Id && e.Date.Month == now.Month)).Last();
			progressBar.Value += added.AmountOfMoney;
		}
    }
}
