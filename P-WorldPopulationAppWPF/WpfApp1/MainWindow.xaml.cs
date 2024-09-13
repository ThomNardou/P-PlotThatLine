using ScottPlot;
using ScottPlot.WPF;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
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
                    country.Contient = values[5];
                    country.Population2022 = int.Parse(values[6]);
                    country.Population2020 = int.Parse(values[7]);
                    country.Population2015 = int.Parse(values[8]);
                    country.Population2010 = int.Parse(values[9]);
                    country.Population2000 = int.Parse(values[10]);


                    countryList.Add(country);
                }

                isFirst = false;

            });


            countryList.ForEach(country =>
            {
                int[] years = {2000, 2010, 2015, 2020, 2022};

                int[] pops = {
                    country.Population2000, 
                    country.Population2010, 
                    country.Population2015, 
                    country.Population2020, 
                    country.Population2022
                };


                WpfPlot1.Plot.Add.Scatter(years, pops).LegendText = country.CountryName;
                WpfPlot1.Plot.Legend.IsVisible = false;
                WpfPlot1.Plot.Legend.Alignment = Alignment.MiddleCenter;
                WpfPlot1.Refresh();
            });
        }

        public void DisplayLegends(object sender, RoutedEventArgs e)
        {
            this.WpfPlot1.Plot.Legend.IsVisible = !this.WpfPlot1.Plot.Legend.IsVisible;
            WpfPlot1.Refresh();
        }
    }
}