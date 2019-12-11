using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using System.Windows.Media;

namespace Monny
{
	/// <summary>
	/// Interaction logic for ExpensePage.xaml
	/// </summary>
	public partial class ExpensePage : Page
	{
		public MainWindow controller;

		/// <summary>
		/// Save main windows instance and set progress bar width
		/// </summary>
		/// <param name="_mainWindow">
		/// Save main window instance to display other pages
		/// </param>
		public ExpensePage(MainWindow _mainWindow)
		{
			InitializeComponent();
			controller = _mainWindow;
			datePicker.SelectedDateChanged += DatePicker_SelectedDateChanged;
			datePicker.SelectedDate = DateTime.Now;
		}
		/// <summary>
		/// Buttons click events
		/// </summary>
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
			// Checks if date was selected
			if (datePicker.SelectedDate != null)
			{
				// Checks if date is current or past
				if (datePicker.SelectedDate.Value < DateTime.Now)
				{
					// Open help input window with add category button
					InputCategoryWindow inputCategoryWindow = new InputCategoryWindow(controller, this, datePicker.SelectedDate.Value);
					inputCategoryWindow.ShowDialog();
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
		/// <summary>
		/// Open's help window
		/// </summary>
		/// <param name="category">
		/// Define in which category expense will be added
		/// </param>
		private void OpenInputWindow(string category)
		{
			// Checks if date was selected
			if (datePicker.SelectedDate != null)
			{
				// Checks if date is current or past
				if (datePicker.SelectedDate.Value < DateTime.Now)
				{
					// Open help input window
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
		public void SetProgressBar(DateTime date)
		{
			if (date <= DateTime.Now)
			{
				// Set progress bar width
				ExpenseRepository expenses = new ExpenseRepository();
				double sumExpenses = expenses.GetItems().Where(e => (e.UserId == controller.user.Id && e.Date.Day == date.Day && e.Date.Month == date.Month && e.Date.Year == date.Year)).Sum(e => e.AmountOfMoney);
				progressBar.Value = sumExpenses;

				// need to be done: setting max value of progress bar
				IncomeRepository incomes = new IncomeRepository();
				double sumIncomes = incomes.GetItems().Where(i => (i.UserId == controller.user.Id && i.Date.Month == date.Month)).Sum(i => i.MoneyCount);
				progressBar.Maximum = Math.Round(sumIncomes / 30.0);
				CheckProgressBarStatus(sumExpenses, progressBar.Maximum);
			}
			else
			{
				MessageBox.Show("Wrong date, can't set progress bar");
			}
		}
		/// <summary>
		/// Updates progress bar width when new expense was added
		/// </summary>
		/// <param name="lastAddedPrice"></param>
		/// <param name="selectedDate"></param>
		public void UpdateProgressBar(double lastAddedPrice, DateTime selectedDate)
		{
			if (selectedDate < DateTime.Now)
			{
				progressBar.Value += lastAddedPrice;
			}
			else
			{
				MessageBox.Show("Wrong date, can't update progress bar");
			}
		}
		public void CheckProgressBarStatus(double dailyExpenses, double dailyMaximum)
		{
			if (dailyExpenses > dailyMaximum)
			{
				progressBar.Background = Brushes.Red;
				progressBar.BorderBrush = Brushes.DarkRed;
				progressBar.Foreground = Brushes.Transparent;
				status.Content = $"You have spent to much today. ({dailyExpenses}/{dailyMaximum})$";
			}
			else
			{
				status.Content = $"You are going greate today! ({dailyExpenses}/{dailyMaximum})$";
			}
		}
		private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			SetProgressBar((DateTime)datePicker.SelectedDate);
		}
	}
}
