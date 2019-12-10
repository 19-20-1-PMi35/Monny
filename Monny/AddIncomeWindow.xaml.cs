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
using System.Windows.Shapes;
using DataAccess;
using DataAccess.Entities;

namespace Monny
{
    /// <summary>
    /// Interaction logic for AddIncomeWindow.xaml
    /// </summary>
    public partial class AddIncomeWindow : Window
    {
        private readonly MonnyDbContext database_variable = new MonnyDbContext();
        private string category;
        private MainWindow controller;
        private readonly DateTime date;
        private readonly IncomePage incomePage;
        public AddIncomeWindow(MainWindow _mainWindow, IncomePage _incomePage, string _category, int month_id)
        {
            InitializeComponent();
            title.Content += _category + " ---- " + month_id.ToString();
            category = _category;
            controller = _mainWindow;
            incomePage = _incomePage;
            date = new DateTime(2019, month_id, 1);
        }
        private void Add_Income_Click(object sender, RoutedEventArgs e)
        {
            if (Double.TryParse(price.Text, out double amount))
            {
                Category categ = database_variable.Set<Category>().ToList().Find(a => a.Name == category);
                Income new_income = new Income();
      
                new_income.CategoryId = categ.Id;
                new_income.MoneyCount = Double.Parse(price.Text);
                new_income.Date = date;
                new_income.UserId = controller.user.Id;
                new_income.CategoryCheck = categ.CategoryType;


                database_variable.Set<Income>().Add(new_income);
                database_variable.SaveChanges();
                incomePage.ShowResult(new_income.MoneyCount, new_income.CategoryCheck);
                this.Close();
                
            }
            else
            {
                MessageBox.Show("Wrong price entered!");
            }

        }
      
    }
}
