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
using OpenWeatherApiLibrary;
namespace WeatherApp
{
    /// <summary>
    /// Логика взаимодействия для DailyCastShower.xaml
    /// </summary>
    public partial class DailyCastShower : UserControl
    {
        public DailyCastShower(DailyInfo day)
        {
            InitializeComponent();

            string url = String.Format("http://openweathermap.org/img/wn/{0}@4x.png", day.Weather[0].IconName);

            WeatherImg.ImageSource = new BitmapImage(new Uri(url));

            var dateTime = new DateTime(1970, 1, 1).AddSeconds(day.DateUnix);

            Date.Content = dateTime.ToString().Substring(0, 10);

            var table = new List<TableHelper>()
            {
                new TableHelper(day.Temperature.Morning, day.FeelsLikeTemperature.Morning),
                new TableHelper(day.Temperature.Day, day.FeelsLikeTemperature.Day),
                new TableHelper(day.Temperature.Evening, day.FeelsLikeTemperature.Evening),
                new TableHelper(day.Temperature.Night, day.FeelsLikeTemperature.Night),
            };

            tableDG.ItemsSource = table;
        }
        class TableHelper
        {
            public double Температура { get; set; }
            public double Ощущается_как { get; set; }
            public TableHelper(double x, double y)
            {
                Температура = x;
                Ощущается_как = y;
            }
        }

        private void tableDG_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            switch (e.Row.GetIndex())
            {
                case 0: e.Row.Header = "утро"; break;
                case 1: e.Row.Header = "день"; break;
                case 2: e.Row.Header = "вечер"; break;
                case 3: e.Row.Header = "ночь"; break;
                default: break;
            }

        }
    }
}
