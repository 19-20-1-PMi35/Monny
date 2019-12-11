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
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Entities;

namespace Monny
{
    /// <summary>
    /// Логика взаимодействия для DreamPage.xaml
    /// </summary>
    public partial class DreamPage : Page
    {
		private MainWindow controller;
		public string dreamName;
		public double dreamPrice;
		private string[] phrases;
		private MonnyDbContext dbContext;

		public DreamPage(MainWindow _mainWindow)
		{

			InitializeComponent();
			controller = _mainWindow;
			dbContext = new MonnyDbContext();

			phrases = new string[13];
			phrases[0] = "The best things in life are free.";
			phrases[1] = "There are things more important than money, but you don't buy them without money.";
			phrases[2] = "Money is a holiday that is always with you.";
			phrases[3] = "Money doesn't grow on trees.";
			phrases[4] = "The happiness is in the purchases rather than the actual money";
			phrases[5] = "Wealth is the ability to spend less than you earn.";
			phrases[6] = "Wealth is the ability to invest.";
			phrases[7] = "A rich one is not the one who earns a lot, rather the one who spends a little";
			phrases[8] = "The poor is not the one who has no money but the one who has no dream.";
			phrases[9] = " The ability to properly manage money is one of \n the main qualities of rich people.";
			phrases[10] = " Wealth does not come from desires. \n It comes from a thoughtful plan of action and from hard work.";
			phrases[11] = "You shouldnt bring the topic of money, \n with people who earn either much more or much less";
			phrases[12] = "It's not about the money, but rather the amount";
			generatePhrase();

		

							Dream dreamCheck = dbContext.Set<Dream>().ToList().Find(d => d.UserId == controller.user.Id);
							if(dreamCheck!=null)
						{ 
								dreamNameLabel.Content = dreamCheck.Name;
								dreamNameLabel.Content = dreamNameLabel.Content + " " + dreamCheck.Price.ToString();

								UpdateProgressBar();

						}
		}

		private void generatePhrase()
		{
			Random rnd = new Random();
			int randomIndex = rnd.Next(0, 13);
			Phrase.Content = phrases[randomIndex];
		}


		private void add_Dream_Click(object sender, RoutedEventArgs e)
		{
			DreamInputWindow inputWindow = new DreamInputWindow(controller, this);
			inputWindow.ShowDialog();
		}


		public void UpdateProgressBar()
		{

			double totalExpance = dbContext.Set<Expense>().ToList().Where(e => (e.UserId == controller.user.Id)).Sum(e => e.AmountOfMoney);
			double totalIncome = dbContext.Set<Income>().ToList().Where(e => (e.UserId == controller.user.Id)).Sum(e => e.MoneyCount);
			double difference = (totalIncome - totalExpance);
			if (difference >= 0)
			{
				ProgressBar.Value = difference / 100;
				money.Text =  "("+difference+" UAH)";
			}
			else 
			{
				ProgressBar.Value = 0;
			}

		}





	}
}


