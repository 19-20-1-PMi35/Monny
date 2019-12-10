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
        Random rand;
        public MainWindow mainWindow;
        private MonnyDbContext dbContext = new MonnyDbContext();
        public List<int> mounth = new List<int>();
        
        private List<Expense> expenses;
        private List<Expense> expenses2;
        public StatisticPage(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            Axis();
           
            DrawText(canvas1, "20", new Point (-7, canvas1.Height-20), 15, HorizontalAlignment.Center, VerticalAlignment.Bottom);
            DrawText(canvas1, "10", new Point(-7, canvas1.Height - 10), 15, HorizontalAlignment.Center, VerticalAlignment.Bottom);
            DrawText(canvas1, "30", new Point(-7, canvas1.Height - 30), 15, HorizontalAlignment.Center, VerticalAlignment.Bottom);
            DrawText(canvas1, "40", new Point(-7, canvas1.Height - 40), 15, HorizontalAlignment.Center, VerticalAlignment.Bottom);
            DrawText(canvas1, "50", new Point(-7, canvas1.Height - 50), 15, HorizontalAlignment.Center, VerticalAlignment.Bottom);
            DrawText(canvas1, "60", new Point(-7, canvas1.Height - 60), 15, HorizontalAlignment.Center, VerticalAlignment.Bottom);
            DrawText(canvas1, "70", new Point(-7, canvas1.Height - 70), 15, HorizontalAlignment.Center, VerticalAlignment.Bottom);
            DrawText(canvas1, "80", new Point(-7, canvas1.Height - 80), 15, HorizontalAlignment.Center, VerticalAlignment.Bottom);
        }
        
        private void but_Click(object sender, RoutedEventArgs e)
        {
            rand = new Random();
            expenses = dbContext.Set<Expense>().ToList().FindAll(u => (u.Date.Month==mounth[0]));
            expenses2 = dbContext.Set<Expense>().ToList().FindAll(u => (u.Date.Month == mounth[1]));
            CreatePoints();
        }
        private Point ToCanvas(double lat, double lon)
        {
            double x = ((lon * canvas1.ActualWidth) / 360.0) - 180.0;
            double y = ((lat * canvas1.ActualHeight) / 360.0) - 180.0;
            return new Point(x, y);
        }
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
                polyLine.Points.Add(ToCanvas(i*10,money ));
                polyLine2.Points.Add(ToCanvas(i*10, money2));
            }
        }
        private void analize_Click(object sender, RoutedEventArgs e)
        {
           // mainWindow.OpenPage(MainWindow.pages.analize);
        }
        public void checking()
        {
            if (mounth.Capacity==3)
            {
                mounth.RemoveAt(0);
            }
        }
        private void Axis()
        {
            Line axies_X = new Line();
            axies_X.Stroke = System.Windows.Media.Brushes.Black;
            axies_X.X1 = 0;
            axies_X.X2 = 0;
            axies_X.Y1 = 0;
            axies_X.Y2 = canvas1.Height;

            Line axies_Y = new Line();
            axies_Y.Stroke = System.Windows.Media.Brushes.Black;
            axies_Y.X1 = 0;
            axies_Y.X2 = canvas1.Width+50;
            axies_Y.Y1 = canvas1.Height;
            axies_Y.Y2 = canvas1.Height;

            canvas1.Children.Add(axies_X);
            canvas1.Children.Add(axies_Y);
        }
        // Position a label at the indicated point.
        private void DrawText(Canvas can, string text, Point location,
            double font_size,
            HorizontalAlignment halign, VerticalAlignment valign)
        {
            // Make the label.
            Label label = new Label();
            label.Content = text;
            label.FontSize = font_size;
            can.Children.Add(label);

            // Position the label.
            label.Measure(new Size(double.MaxValue, double.MaxValue));

            double x = location.X;
            if (halign == HorizontalAlignment.Center)
                x -= label.DesiredSize.Width / 2;
            else if (halign == HorizontalAlignment.Right)
                x -= label.DesiredSize.Width;
            Canvas.SetLeft(label, x);

            double y = location.Y;
            if (valign == VerticalAlignment.Center)
                y -= label.DesiredSize.Height / 2;
            else if (valign == VerticalAlignment.Bottom)
                y -= label.DesiredSize.Height;
            Canvas.SetTop(label, y);
        }
        private void febrary_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(2);
        }
        private void january_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(1);
        }
        private void march_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(3);
        }
        private void april_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(4);
        }
        private void may_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(5);
        }
        private void june_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(6);

        }
        private void july_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(7);
        }
        private void august_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(8);
        }
        private void september_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(9);
        }
        private void october_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(10);
        }
        private void november_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(11);
        }
        private void december_Checked(object sender, RoutedEventArgs e)
        {
            checking();
            mounth.Add(12);
        }
    }
}
