using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using OpenWeatherApiLibrary;
using System.Text.Json;
using System.Threading;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string apiKey = "82b797b6ebc625032318e16f1b42c016";
        WeatherApi client = new WeatherApi(apiKey);
        string city = "Москва";
        OneCallWeatherApiResponse casts;
        int dayIndex = 0;
        static CancellationTokenSource x = new CancellationTokenSource();


        public MainWindow()
        {
            InitializeComponent();
            try
            {
                casts = client.GetForcastByCityName(city);
                CityName.Content = city;
                stack.Children.Add(new DailyCastShower(casts.daily.ElementAt(dayIndex)));
                BackwardButton.IsEnabled = false;
            }
            catch (Exception e)
            {
                e.Message.Length.ToString();
            }
            x = new CancellationTokenSource();
            StartRace(x).Wait();
        }
        async Task Break(CancellationTokenSource load)
        {
            await Task.Delay(2000);
            load.Cancel();
            Dispatcher.Invoke(() => { ChangeLocation.IsEnabled = true; PG.Value = 100; });
        }
        async Task run(CancellationToken ct)
        {
            do
            {
                Dispatcher.Invoke(() => PG.Value++);
                await Task.Delay(100, ct);
            } while (!ct.IsCancellationRequested);
        }
        async Task GetForcastAsync(CancellationTokenSource load)
        {
            var response = client.GetForcastByCityName(city);
            await Task.Delay(0);
            casts = response;
            load.Cancel();
        }

        private void PushForward(object sender, RoutedEventArgs e)
        {
            if (dayIndex < 7)
            {
                dayIndex++;
                BackwardButton.IsEnabled = true;
                stack.Children.Clear();
                stack.Children.Add(new DailyCastShower(casts.daily.ElementAt(dayIndex)));
            }
            if (dayIndex == 7)
            {
                (sender as Button).IsEnabled = false;
            }

        }

        private void PushBackward(object sender, RoutedEventArgs e)
        {
            if (dayIndex > 0)
            {
                dayIndex--;
                ForwardButton.IsEnabled = true;
                stack.Children.Clear();
                stack.Children.Add(new DailyCastShower(casts.daily.ElementAt(dayIndex)));
            }
            if (dayIndex == 0)
            {
                (sender as Button).IsEnabled = false;

            }
        }


        private void ChangeLocationButtonClick(object sender, RoutedEventArgs e)
        {
            if (CityInput.Text != "")
            {
                CancellationTokenSource loader = new CancellationTokenSource();
                CancellationToken load = loader.Token;

                city = CityInput.Text;

                ChangeLocation.IsEnabled = false;

                Task.Run(() => run(load));
                Load(loader);
            }
        }
        async void Load(CancellationTokenSource loader)
        {
            int retries = 8;

            for (int retriesLeft = retries; retriesLeft > -1; retriesLeft--)
            {
                try
                {
                    await Task.Run(() => TryLoad(loader));

                    retriesLeft = 0;
                    stack.Children.Clear();
                    stack.Children.Add(new DailyCastShower(casts.daily.ElementAt(dayIndex)));
                    PG.Value = 100;
                    CityName.Content = city;
                }
                catch (Exception exptn)
                {
                    if (retriesLeft <= 0)
                    {
                        PG.Value = 100;
                        loader.Cancel();
                        if (exptn is NoConnectionException)
                        {
                            MessageBox.Show("Проблемы с интеренетом");
                        }
                        else if (exptn is BadSityNameException)
                        {
                            MessageBox.Show("Не получается найти такой город");
                        }
                        await Task.Delay(300);
                    }
                }
            }

            await Task.Delay(300);
            PG.Value = 0;
            ChangeLocation.IsEnabled = true;
        }
        async Task StartRace(CancellationTokenSource loader)
        {

            var token = x.Token;
            int[] taskId = { 1, 2, 3, 4, 5 };

            List<Task> tasks = new List<Task>();
            
            tasks.Add(Task.Run(async ()=> await MassiveResponse(2, token)));
            tasks.Add(Task.Run(async ()=> await MassiveResponse(3, token)));
            tasks.Add(Task.Run(async ()=> await MassiveResponse(1, token)));
            tasks.Add(Task.Run(async ()=> await MassiveResponse(4, token)));
            tasks.Add(Task.Run(async ()=> await MassiveResponse(5, token)));
            //foreach (var item in tasks)
            //{
            //    item.Start();
            //}

            var finishedTask = await Task.WhenAny(tasks);

                x.Cancel();
        }

        private async Task<bool> MassiveResponse(int arg1, CancellationToken token)
        {
            Random r = new Random((int)DateTime.Now.Ticks);

            if (!token.IsCancellationRequested)
            {
                await Task.Delay(r.Next(500, 2500));
            }
            if (!token.IsCancellationRequested)
            {
                await GetForcastAsync(new CancellationTokenSource());
            }

            if (!token.IsCancellationRequested)
            {
            //    MessageBox.Show("method " + arg1 + "ends"); ;

            }
            return true;
        }

        async Task TryLoad(CancellationTokenSource loader) //вынести графику выше //паттерн-заместитель 
        {
            Random r = new Random((int)DateTime.Now.Ticks);

            await Task.Delay(r.Next(250, 500));
            await GetForcastAsync(loader);
        }
    }
}
