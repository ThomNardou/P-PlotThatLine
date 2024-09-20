using ScottPlot;
using System.Diagnostics.Metrics;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        List<Country> countryList;
        Plot plot;
        public Form1()
        {
            InitializeComponent();

            plot = this.formsPlot1.Plot;

            var limits = plot.Axes.GetLimits();




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

        private void button1_Click(object sender, EventArgs e)
        {
            FilterByYear();
        }

        private void FilterByYear()
        {
            plot.Clear();

            if (this.fromText.Text == "" || this.toText.Text == "")
            {
                DisplayAllCountry(countryList);
                return;
            }

            if (int.Parse(this.fromText.Text) > int.Parse(this.toText.Text))
            {
                MessageBox.Show("From value cannot be higher than To value !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int.Parse(this.toText.Text) > 2022 || int.Parse(this.toText.Text) < 2000) || (int.Parse(this.fromText.Text) > 2022 || int.Parse(this.fromText.Text) < 2000))
            {
                MessageBox.Show("Please enter beetween 2000 and 2022 !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            plot.Axes.SetLimitsX(int.Parse(this.fromText.Text), int.Parse(this.toText.Text));
            var limits = plot.Axes.GetLimits();

            this.formsPlot1.Plot.Axes.AutoScale();

            DisplayAllCountry(countryList);

            this.formsPlot1.Interaction.Disable();

            this.formsPlot1.Refresh();

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


                    plot.Add.Scatter(years, pops).LegendText = country.CountryName;
                    plot.Legend.IsVisible = false;
                    plot.Legend.Alignment = Alignment.MiddleCenter;



                });

            this.formsPlot1.Refresh();


        }

        private void legends_Click(object sender, EventArgs e)
        {
            if (!plot.Legend.IsVisible) this.legends.Text = "Hide Legends";
            else this.legends.Text = "Display Legends";

            plot.Legend.IsVisible = !plot.Legend.IsVisible;
            this.formsPlot1.Refresh();
        }
    }
}
