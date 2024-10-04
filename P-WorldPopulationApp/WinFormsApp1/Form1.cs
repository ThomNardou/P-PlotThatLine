using ScottPlot;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        List<string> itemsChecked = new List<string>();
        List<Country> countryList = new List<Country>();
        Plot plot;
        public Form1()
        {
            InitializeComponent();

            plot = this.formsPlot1.Plot;


            bool isFirst = true;

            this.countryList = new List<Country>();

            List<string> lines = File.ReadAllLines("../../../../world_population.csv").ToList();

            string[] header = lines[0].Split(',');


            lines.Skip(1)
                .ToList()
                .ForEach(s =>
                {
                    string[] values = s.Split(',');

                    Country country = new Country();
                    country.Rank = values[0];
                    country.CCA = values[1];
                    country.CountryName = values[2];
                    country.Capital = values[3];
                    country.Continent = values[4];

                    country.Population = new Dictionary<int, int>();

                    int index = 8;
                    header
                    .ToList()
                    .ForEach(h =>
                    {
                        if (int.TryParse(h, out _))
                        {
                            int year = int.Parse(h);
                            country.Population.Add(year, int.Parse(values[index]));
                            index++;
                        }

                    });


                    countryList.Add(country);
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

            this.checkBoxLock.Checked = true;

            this.formsPlot1.Refresh();

        }

        private void DisplayAllCountry(List<Country> list)
        {
            list
                .Where(p => p.Continent == "Europe").ToList()
                .ForEach(country =>
                {
                    int[] years = country.Population.Keys.ToArray();

                    int[] pops = country.Population.Values.ToArray();

                    this.countryCheckBox.Items.Add(country.CountryName);
                    

                    plot.Add.Scatter(years, pops).LegendText = country.CountryName;
                    plot.Legend.IsVisible = false;
                    plot.Legend.Alignment = Alignment.MiddleCenter;
                });

            

            this.formsPlot1.Refresh();


        }

        private void legends_Click(object sender, EventArgs e)
        {
            this.legends.Text = !plot.Legend.IsVisible ? "Hide Legends" : "Display Legends";

            plot.Legend.IsVisible = !plot.Legend.IsVisible;
            this.formsPlot1.Refresh();
        }

        private void checkBoxLock_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLock.Checked)
                this.formsPlot1.Interaction.Disable();
            else
                this.formsPlot1.Interaction.Enable();

        }

        private void checkOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void DisplayCountrySelected(List<Country> countries)
        {
            countries.ForEach(country => {
                int[] years = country.Population.Keys.ToArray();

                int[] pops = country.Population.Values.ToArray();


                plot.Add.Scatter(years, pops).LegendText = country.CountryName;
                plot.Legend.IsVisible = false;
                plot.Legend.Alignment = Alignment.MiddleCenter;

                this.formsPlot1.Refresh();
            });
        }

        private void countryCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<Country> selectedCountry = new List<Country>();
            

            // TODO: Changer la façon de Get l'item après qu'il ait été coché
            if (e.NewValue == CheckState.Checked)
                itemsChecked.Add(countryCheckBox.Items[e.Index].ToString());
            else
                itemsChecked.Remove(countryCheckBox.Items[e.Index].ToString());


            itemsChecked.ForEach(i =>
            {
                countryList
                    .Where(c => c.CountryName == i.ToString())
                    .ToList()
                    .ForEach(country => selectedCountry.Add(country));
            });

            plot.Clear();
            formsPlot1.Refresh();

            DisplayCountrySelected(selectedCountry);
        }
    }
}
