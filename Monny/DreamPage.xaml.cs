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
			phrases[0] = "Гроші — це свято, яке зажди з тобою.";
			phrases[1]= "Є речі важливіші від грошей, але без грошей їх не купиш.";
			phrases[2] = "Не в грошах щастя, а в їхній надійності.";
			phrases[3] = "Не в грошах щастя, а в їхній кількості.";
			phrases[4] = "Не в грошах щастя. А в покупках.";
			phrases[5] = "Багатство – це вміння витрачати менше, ніж заробляєш.";
			phrases[6] = "Багатство - це вміння відкладати і вкладати.";
			phrases[7] = "Багатий не той, хто багато заробляє, а той, хто мало витрачає.";
			phrases[8] = "Бідний не той, у кого немає грошей, а той, у кого немає мрії.";
			phrases[9] = " Уміння правильно розпоряджатися грошима – одна з \n головних якостей багатих людей.";
			phrases[10] = " З бажань багатство не виходить. \n Воно виходять з продуманого плану дій та з наполегливої праці.";
			phrases[11] = " Важливо не те, скільки грошей ви заробляєте, а те, \n скільки грошей у вас залишається, як вони працюють на вас, \n і скільки поколінь ви зможете ними забезпечити.";
			phrases[12] = "Не варто говорити про гроші з людьми, які їх мають набагато більше, або набагато менше.";
			generatePhrase();

			Dream dreamCheck=dbContext.Set<Dream>().ToList().Find(d => d.UserId == controller.user.Id);
			if(dreamCheck!=null)
			{
				dreamNamePage.Content = dreamCheck.Name;
				dreamNamePage.Content = dreamNamePage.Content + " " + dreamCheck.Price.ToString();
				UpdateProgressBar();
			}
		}

		private void generatePhrase()
		{
			Random rnd = new Random();
			int randomIndex = rnd.Next(0, 12);
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
			}
			else 
			{
				ProgressBar.Value = 0;
			}

		}

		/*
		 метод
		 {
		 поточна сума=(дохід-витрати)
		 (сума на мрію=100% поточна сума=?%)
		 поточні відсотки=(поточна сума*100)/сума на мрію
		 
		 }
		 */


		/*
		рандомні цитатки вставляє 
		*/




	}
}


