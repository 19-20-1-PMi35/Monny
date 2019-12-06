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

namespace Monny
{
    /// <summary>
    /// Interaction logic for ExpensePage.xaml
    /// </summary>
    public partial class ExpensePage : Page
    {
		private readonly MonnyDbContext dbContext = new MonnyDbContext();
		public MainWindow controller;
		public DateTime now = DateTime.Now;
		public ExpensePage(MainWindow _mainWindow)
        {
            InitializeComponent();
			controller = _mainWindow;

			// Set progress bar width
			double sum = dbContext.Set<Expense>().ToList().Where(e => (e.UserId == controller.user.Id && e.Date.Month == now.Month)).Sum(e => e.AmountOfMoney);
			progressBar.Value = sum;
			// need to be done: setting max value of progress bar
			//progressBar.Maximum = ?
		}

        private void food_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Food");
			UpdateProgressBar();

		}
        private void clothes_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Clothes");
			UpdateProgressBar();
		}

        private void transport_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Transport");
			UpdateProgressBar();
		}

        private void caffe_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Caffe");
			UpdateProgressBar();
		}

        private void traveling_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Traveling");
			UpdateProgressBar();
		}

        private void health_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Health");
			UpdateProgressBar();
		}

        private void entertainments_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Entertainments");
			UpdateProgressBar();
		}

        private void other_Click(object sender, RoutedEventArgs e)
        {
            OpenInputWindow("Other");
			UpdateProgressBar();
        }
        private void OpenInputWindow(string category)
        {
            InputWindow inputWindow = new InputWindow(controller, category);
            inputWindow.ShowDialog();
        }
		private void UpdateProgressBar()
		{
			Expense added = dbContext.Set<Expense>().ToList().Where(e => (e.UserId == controller.user.Id && e.Date.Month == now.Month)).Last();
			progressBar.Value += added.AmountOfMoney;
		}
    }
}
