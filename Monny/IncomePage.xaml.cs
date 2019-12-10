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
            asum = database_variable.Set<Income>().ToList().Where(e => (e.UserId == controller.user.Id && e.CategoryCheck == 1)).Sum(e => e.MoneyCount);
            psum = database_variable.Set<Income>().ToList().Where(e => (e.UserId == controller.user.Id && e.CategoryCheck == 2)).Sum(e => e.MoneyCount);
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

            AddIncomeWindow new_form = new AddIncomeWindow(controller, this, category);//, datePicker.SelectedDate.Month);
            new_form.ShowDialog();
            
            //  }
            //  else
            // {
            //     MessageBox.Show($"Choose correct month! Don't look at future now!.\r\n{datePicker.SelectedDate.Value.ToShortDateString()} was selected.\r\n{DateTime.Now.ToShortDateString()} - today.");
            // }
            // }
            //  else
            // {
            //    MessageBox.Show("Choose month, please.");
            //}
        }
        public void ShowResult(double money, int? catcheck)
        {
            double user_income_written = database_variable.Set<Income>().ToList().Where(e => (e.UserId == controller.user.Id && e.Date.Month == now.Month)).Sum(e => e.MoneyCount);

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
           // MessageBox.Show(active_sum.ToString()+passive_sum.ToString()+total_sum.ToString());
        }
       

    }
}