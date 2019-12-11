using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Monny
{
    
    /// <summary>
    /// Логика взаимодействия для StatisticPage.xaml
    /// </summary>
    

public partial class StatisticPage : Page
    {
        public MainWindow mainWindow;
        private MonnyDbContext dbContext = new MonnyDbContext();
        public List<int> mounth = new List<int>();
        public string name_of_month;
        public Dictionary<int, string> monthes = new Dictionary<int, string>();
        public Dictionary<int, string> categ = new Dictionary<int, string>();
        public Dictionary<int, string> categ2 = new Dictionary<int, string>();
        private List<Expense> expenses;
        private List<Expense> expenses2;
        private List<Income> incomes1;
        private List<Income> incomes2;
        /// <summary>
        /// Initialize page, draw axis and label for them
        /// </summary>
        /// <param name="_mainWindow"></param>
        public StatisticPage(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            Axis();
            DrawText(canvas1, "40", new Point(-7, 50),270, 10);
            DrawText(canvas1, "80", new Point(-7, 90),270, 10);
            DrawText(canvas1, "120", new Point(-7, 130),270, 10);
            DrawText(canvas1, "160", new Point(-7, 170),270, 10);
            DrawText(canvas1, "200", new Point(-7, 210), 270, 10);

        }
        /// <summary>
        /// Function, which  chooses tmo-month's expenses and incomes, calls the function for drawing grafics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Click(object sender, RoutedEventArgs e)
        {
            expenses = dbContext.Set<Expense>().ToList().FindAll(u => (u.Date.Month==mounth[0]));
            expenses2 = dbContext.Set<Expense>().ToList().FindAll(u => (u.Date.Month == mounth[1]));
            incomes1 = dbContext.Set<Income>().ToList().FindAll(u => (u.Date.Month == mounth[0]));
            incomes2 = dbContext.Set<Income>().ToList().FindAll(u => (u.Date.Month == mounth[1]));
            CreatePoints();
            col1.Visibility = Visibility.Visible;
            graf_1.Content = monthes[mounth[0]];
            col2.Visibility = Visibility.Visible;
            graf_2.Content = monthes[mounth[1]];
        }
        /// <summary>
        /// Function for drawing grafics
        /// </summary>
        private void CreatePoints()
        {
            polyLine.Points.Clear();
            polyLine2.Points.Clear();
            for (int i = 1; i <= 31; i += 1)
            {
                var money = (from a in expenses
                             where a.Date.Day == i
                             select a.AmountOfMoney).ToList<double>().Sum();
                var money2 = (from a in expenses2
                             where a.Date.Day == i
                             select a.AmountOfMoney).ToList<double>().Sum();
                var income1 = (from a in incomes1
                              where a.Date.Day == i
                              select a.MoneyCount).ToList<double>().Sum();
                var income2 = (from a in incomes2
                               where a.Date.Day == i
                               select a.MoneyCount).ToList<double>().Sum();
                polyLine.Points.Add(new Point(i*10,10+(money*0.5) ));
                polyLine2.Points.Add(new Point(i*10,10+ money2*0.5));
            }
        }
        /// <summary>
        /// Method for analize grafics, show month, where you spend less, catagories, 
        /// where you spend money the most and how much you save in these monthes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void analize_Click(object sender, RoutedEventArgs e)
        {
            double sum = 0, sum1 = 0;
            for (int i = 0; i < expenses.Count; i++)
            {
                sum += expenses[i].AmountOfMoney;
            }
            for (int i = 0; i < expenses2.Count; i++)
            {
                sum1 += expenses2[i].AmountOfMoney;
            }
            if (sum < sum1)
            {
                name_of_month = monthes[mounth[0]];
            }
            else
            {
                name_of_month = monthes[mounth[1]];
            }
            double sav_month1 = 0, sav_month2 = 0;
            var spend1 = (from a in expenses
                         select a.AmountOfMoney).ToList<double>().Sum();
            var spend2 = (from a in expenses2
                          select a.AmountOfMoney).ToList<double>().Sum();
            var income1 = (from a in incomes1
                           select a.MoneyCount).ToList<double>().Sum();
            var income2 = (from a in incomes2
                           select a.MoneyCount).ToList<double>().Sum();
            sav_month1 = (double)income1 - (double)spend1;
            sav_month2 = (double)income2 - (double)spend2;
            month.Content += name_of_month;
            saving.Content += $"{monthes[mounth[0]]}: {sav_month1} UAH\n{monthes[mounth[1]]}: {sav_month2}  UAH";
            List<int> temp = new List<int>();
            List<int> temp2 = new List<int>();
            for (int i = 1; i<=7; i++)
            {
                var cat = (from a in expenses
                           where a.CategoryId == i 
                           select a.AmountOfMoney).ToList<double>().Sum();
                var cat2 = (from a in expenses2
                           where a.CategoryId == i
                           select a.AmountOfMoney).ToList<double>().Sum();
                
                Category  q = dbContext.Set<Category>().ToList().Find(u => (u.Id == i));
                if (q!= null && (int) cat!=0 && (int)cat2 != 0)
                {
                    categ.Add((int)cat, q.Name);
                    categ2.Add((int)cat2, q.Name);
                }
                temp.Add((int)cat);
                temp2.Add((int)cat2);
            }
            category_name.Content += $"{monthes[mounth[0]]}: {categ[temp.Max()]} \n{monthes[mounth[1]]}: {categ2[temp2.Max()]}";




        }
        /// <summary>
        /// Method, whisch checks quantity of choosing monthes
        /// </summary>
        public void checking()
        {
            if (mounth.Capacity==3)
            {
                mounth.RemoveAt(0);
            }
        }
        /// <summary>
        /// Function, which draws the axises and show them
        /// </summary>
        private void Axis()
        {
            Line axies_X = new Line();
            axies_X.Stroke = System.Windows.Media.Brushes.Black;
            axies_X.X1 = 0;
            axies_X.X2 = 0;
            axies_X.Y1 = 0+10;
            axies_X.Y2 = canvas1.Height+20;

            Line axies_Y = new Line();
            axies_Y.Stroke = System.Windows.Media.Brushes.Black;
            axies_Y.X1 = 0;
            axies_Y.X2 = canvas1.Width+40;
            axies_Y.Y1 = 0+10;
            axies_Y.Y2 = 0+10;

            canvas1.Children.Add(axies_X);
            canvas1.Children.Add(axies_Y);
        }
        /// <summary>
        /// Method, which adds the labels near axises and rotates them
        /// </summary>
        /// <param name="can"></param>
        /// <param name="text"></param>
        /// <param name="location"></param>
        /// <param name="angle"></param>
        /// <param name="font_size"></param>
        private void DrawText(Canvas can, string text, Point location, double angle,
            double font_size)
        {
            // Make the label.
            Label label = new Label();
            label.Content = text;
            label.FontSize = font_size;
            can.Children.Add(label);
            if (angle != 0)
                label.LayoutTransform = new RotateTransform(angle,label.DesiredSize.Width / 2, label.DesiredSize.Height / 2);

            // Position the label.
            label.Measure(new Size(double.MaxValue, double.MaxValue));

            double x = location.X;
                x -= label.DesiredSize.Width / 2;
           
            Canvas.SetLeft(label, x);

            double y = location.Y;
                y -= label.DesiredSize.Height;
            Canvas.SetTop(label, y);
        }
        /// <summary>
        /// Events, which works when you check CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void febrary_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(2);
            monthes.Add(2, "February");
        }
        private void january_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(1);
            monthes.Add(1, "January");
        }
        private void march_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(3);
            monthes.Add(3, "March");
        }
        private void april_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(4);
            monthes.Add(4, "April");
        }
        private void may_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(5);
            monthes.Add(5, "May");
        }
        private void june_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(6);
            monthes.Add(6, "June");

        }
        private void july_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(7);
            monthes.Add(7, "July");
        }
        private void august_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(8);
            monthes.Add(8, "August");
        }
        private void september_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(9);
            monthes.Add(9, "September");
        }
        private void october_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(10);
            monthes.Add(10, "October");
        }
        private void november_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(11);
            monthes.Add(11, "November");
        }
        private void december_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(12);
            monthes.Add(12, "December");
        }
    }
}
