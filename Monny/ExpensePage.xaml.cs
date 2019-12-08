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
			Expense last = dbContext.Set<Expense>().ToList().Last();
			dbContext.Set<Expense>().ToList().Remove(last);
			dbContext.SaveChanges();
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
            InputWindow inputWindow = new InputWindow(controller, this, category);
            inputWindow.ShowDialog();
        }
		public void UpdateProgressBar()
		{
			Expense added = dbContext.Set<Expense>().ToList().Where(e => (e.UserId == controller.user.Id && e.Date.Month == now.Month)).Last();
			progressBar.Value += added.AmountOfMoney;
		}
    }
}
