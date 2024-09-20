using ScottPlot;
using ScottPlot.WPF;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Country> countryList;

        public MainWindow()
        {
            InitializeComponent();





            bool isFirst = true;

            this.countryList = new List<Country>();

            File.ReadAllLines("../../../../world_population.csv").ToList().ForEach(s =>
            {
                if (!isFirst)
                {
                    string[] values = s.Split(',');

                    Country country = new Country();
                    country.Rank = values[0];
                    country.CCA = values[1];
                    country.CountryName = values[2];
                    country.Capital = values[3];
                    country.Contient = values[4];

                    country.Population = new Dictionary<int, int>();

                    country.Population.Add(2022, int.Parse(values[6]));
                    country.Population.Add(2020, int.Parse(values[7]));
                    country.Population.Add(2015, int.Parse(values[8]));
                    country.Population.Add(2010, int.Parse(values[9]));
                    country.Population.Add(2000, int.Parse(values[10]));


                    countryList.Add(country);
                }

                isFirst = false;

            });


            DisplayAllCountry(countryList);
        }

        public void DisplayLegends(object sender, RoutedEventArgs e)
        {
            if (!this.WpfPlot1.Plot.Legend.IsVisible) displayButton.Content = "Hide Legends";
            else displayButton.Content = "Display Legends";
            
            this.WpfPlot1.Plot.Legend.IsVisible = !this.WpfPlot1.Plot.Legend.IsVisible;
            WpfPlot1.Refresh();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void FilterByYear(object sender, RoutedEventArgs e)
        {
            WpfPlot1.Plot.Clear();

            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;

            if (this.fromText.Text == "" || this.toText.Text == "")
            {
                DisplayAllCountry(countryList);
                return;
            }

            if (int.Parse(this.fromText.Text) > int.Parse(this.toText.Text))
            {
                MessageBox.Show("From value cannot be higher than To value !", "Error", button, icon, MessageBoxResult.OK);
                return;
            }

            if ((int.Parse(this.toText.Text) > 2022 || int.Parse(this.toText.Text) < 2000) || (int.Parse(this.fromText.Text) > 2022 || int.Parse(this.fromText.Text) < 2000))
            {
                MessageBox.Show("Please enter beetween 2000 and 2022 !", "Error", button, icon, MessageBoxResult.OK);
                return;
            }

            Plot plot = this.WpfPlot1.Plot;
            plot.Clear();

            countryList
                .Where(c  => c.Contient == "Europe")
                .ToList()
                .ForEach(country =>
                {
                    Dictionary<int, int> pop = country.Population
                    .Where(p => p.Key >= int.Parse(this.fromText.Text) && p.Key <= int.Parse(this.toText.Text))
                    .ToDictionary();


                    int[] years = pop.Select(p=> p.Key).ToArray();
                    int[] pops = pop.Select(p=> p.Value).ToArray();


                    plot.Add.Scatter(years, pops).LegendText = country.CountryName;
                    plot.Legend.IsVisible = false;
                    plot.Legend.Alignment = Alignment.MiddleCenter;

                    WpfPlot1.Refresh();
                });

            Console.Write(this.fromText.Text);

        } 

        private void DisplayAllCountry(List<Country> list)
        {
            list
                .Where(p => p.Contient == "Europe").ToList()
                .ForEach(country =>
                {
                    int[] years = { 2000, 2010, 2015, 2020, 2022 };

                    int[] pops = {
                        country.Population[2000],
                        country.Population[2010],
                        country.Population[2015],
                        country.Population[2020],
                        country.Population[2022]
                    };


                    WpfPlot1.Plot.Add.Scatter(years, pops).LegendText = country.CountryName;
                    WpfPlot1.Plot.Legend.IsVisible = false;
                    WpfPlot1.Plot.Legend.Alignment = Alignment.MiddleCenter;
                    WpfPlot1.Refresh();
                });
        }
    }
}