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
using System.Windows.Shapes;
using System.Linq;

using DataAccess.Entities;
using DataAccess;

namespace Monny
{
    /// <summary>
    /// Logic for InputWindow.xaml
    /// </summary>
    public partial class DreamInputWindow : Window
    {
        private MainWindow controller;
        private DreamPage dreamPage;
        private MonnyDbContext dbContext;

        /// <summary>
        /// Show input window
        /// </summary>
        public DreamInputWindow(MainWindow _mainWindow, DreamPage _dreamPage)
        {
            InitializeComponent();
            controller = _mainWindow;
            dreamPage = _dreamPage;
            dbContext = new MonnyDbContext();

        }

        /// <summary>
        /// Logic for button (save new dream in database and on dream page)
        /// </summary>
        private void add_Click(object sender, RoutedEventArgs e)
        {
                if (dbContext.Set<Dream>().Where(d => d.UserId == controller.user.Id).ToList().Count != 0)
                {
                    dbContext.Set<Dream>().RemoveRange(dbContext.Set<Dream>().Where(d => d.UserId == controller.user.Id));
                }
                dreamPage.dreamName = DreamName.Text;
                dreamPage.dreamPrice = Double.Parse(DreamPrice.Text);
                dreamPage.dreamNameLabel.Content = dreamPage.dreamName + " " + dreamPage.dreamPrice;

                if (Double.TryParse(DreamPrice.Text, out double amount))
                {
                    Dream dream = new Dream();
                    dream.UserId = controller.user.Id;
                    dream.Name = DreamName.Text;
                    dream.Price = amount;

                    dbContext.Set<Dream>().Add(dream);
                    dbContext.SaveChanges();

                    dreamPage.UpdateProgressBar();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong price entered");
                }
            
        }


    }
}
