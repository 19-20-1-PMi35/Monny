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
using DataAccess;
using DataAccess.Entities;

namespace Monny
{
    /// <summary>
    /// Interaction logic for IncomePage.xaml
    /// </summary>
    public partial class IncomePage : Page
    {
        private readonly MonnyDbContext database_variable = new MonnyDbContext();
        public MainWindow controller;
        public double active_sum = 0;
        public int? checker = 0;
        public double passive_sum = 0;
        public double total_sum = 0;
        public DateTime now = DateTime.Now;
        private double asum = 0;
        private double psum = 0;
        public int month_id = 0;
        public string passiv
        {
            get { return (string)GetValue(DebtProperty); }
            set { SetValue(DebtProperty, passive_sum); }
        }


        public static readonly DependencyProperty DebtProperty =
            DependencyProperty.Register("Passive", typeof(string), typeof(IncomePage), new PropertyMetadata(string.Empty));


        public IncomePage(MainWindow _mainWindow)
        {
            InitializeComponent();
            controller = _mainWindow;
            pig.Source = controller.SetImageSource("pig.png");
          
            // double sum = from t in temp
            //             where (t.UserId == controller.user.Id) && (t.Date.Month == now.Month).Sum(l => l.MoneyCount);
            //               temp.Sum(t => t.MoneyCount);
            asum = database_variable.Set<Income>().ToList().Where(e => (e.UserId == controller.user.Id && e.CategoryCheck == 1 && e.Date.Month == month_id)).Sum(e => e.MoneyCount);
            psum = database_variable.Set<Income>().ToList().Where(e => (e.UserId == controller.user.Id && e.CategoryCheck == 2 && e.Date.Month == month_id)).Sum(e => e.MoneyCount);
            progressBar.Value = asum;
            progressBar2.Value = psum;
           

            active.Content = asum;
            passive.Content = psum;
            total.Content = asum + psum;
            this.DataContext = this;
          
        }
        
        private void OnSalary(object sender, MouseButtonEventArgs e)
        {
            AddIncome("Salary");
        }
        private void OnScholarship(object sender, MouseButtonEventArgs e)
        {
            AddIncome("Scholarship");
        }
        private void OnRetirement(object sender, MouseButtonEventArgs e)
        {
            AddIncome("Retirement");
        }
        private void OnPartTimeJob(object sender, MouseButtonEventArgs e)
        {
            AddIncome("Part-time job");
        }
        private void OnRent(object sender, MouseButtonEventArgs e)
        {
            AddIncome("Rent");
        }
        private void OnShares(object sender, MouseButtonEventArgs e)
        {
            AddIncome("Shares");
        }

        private void AddIncome(string category)
        {
            //if (datePicker.SelectedDate != null)
            // {
            //   if (datePicker.SelectedDate.Month < DateTime.Now.Month)
            ///   {
            ///   

            AddIncomeWindow new_form = new AddIncomeWindow(controller, this, category);
            new_form.ShowDialog();
            
      
        }
        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
           // ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            

            string message = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
            if (message == "January")
            {
                month_id = 1;
            }
            if (message == "February")
            {
                month_id = 2;
            }
            if (message == "March")
            {
                month_id = 3;
            }
            if (message == "April")
            {
                month_id = 4;
            }
            if (message == "May")
            {
                month_id = 5;
            }
            if (message == "June")
            {
                month_id = 6;
            }
            if (message == "July")
            {
                month_id = 7;
            }
            if (message == "August")
            {
                month_id = 8;
            }
            if (message == "September")
            {
                month_id = 9;
            }
            if (message == "October")
            {
                month_id = 10;
            }
            if (message == "November")
            {
                month_id = 11;
            }
            if (message == "December")
            {
                month_id = 12;
            }
           
        }
        public void ShowResult(double money, int? catcheck)
        {
            double user_income_written = database_variable.Set<Income>().ToList().Where(e => (e.UserId == controller.user.Id && e.Date.Month == month_id)).Sum(e => e.MoneyCount);

            if (catcheck == 1)
            {
                active_sum = asum + money;
                progressBar.Value = asum + money;
            }
            if (catcheck == 2)
            {
                passive_sum = psum + money;
                progressBar2.Value = psum + money;
            }

            total_sum = active_sum + passive_sum;
            active.Content = active_sum.ToString();
            passive.Content = passive_sum.ToString();
            total.Content = total_sum.ToString();

          }

        private void pig_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

           double expenses_suma = database_variable.Set<Expense>().ToList().Where(e => (e.UserId == controller.user.Id && e.Date.Month == month_id)).Sum(e => e.AmountOfMoney);
           double incomes_suma = database_variable.Set<Income>().ToList().Where(e => (e.UserId == controller.user.Id && e.Date.Month == month_id)).Sum(e => e.MoneyCount);

         
            if (incomes_suma > expenses_suma)
            {

                MessageBox.Show($"You have {incomes_suma - expenses_suma} UAH left in your personal piggy bank! \nYou are doing pretty well, good job :)", "pig saver");

            }
            else if (incomes_suma < expenses_suma)
            {
                MessageBox.Show($"You have no cash in your personal piggy bank. \nIt means your expenses are higher than your incomes, try to learn about money managment!.", "pig saver");

            }
        }
    }
}