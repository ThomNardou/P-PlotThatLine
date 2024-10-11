using ScottPlot;
using ScottPlot.TickGenerators.TimeUnits;
using System.Data.Common;
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
        List<(string Year, int Index)> columnYears = new List<(string Year, int Index)>();
        Plot plot;

        public Form1()
        {
            InitializeComponent();

            WinFormsApp1.Settings.Default.IsFirstRun = true;
            WinFormsApp1.Settings.Default.Save();

            string path = "";

            if (WinFormsApp1.Settings.Default.IsFirstRun == true)
            {



                path = GetPathFileExplorer();

                WinFormsApp1.Settings.Default.IsFirstRun = false;
                WinFormsApp1.Settings.Default.Save();
            }

            plot = this.formsPlot1.Plot;


            this.countryList = new List<Country>();

            ReadCSV(path);


            DisplayAllCountry(countryList);
        }

        private string GetPathFileExplorer()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return "";

            return openFileDialog.FileName;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            FilterByYear();
        }

        private void ReadCSV(string path)
        {
            countryList.Clear();
            List<string> lines = File.ReadAllLines(path).ToList();

            string[] header = lines[0].Split(',');

            columnYears = header
                .Select((column, index) => (Year: column, Index: index))
                .Where(x => Regex.IsMatch(x.Year, "^[0-9]"))
                .ToList();

            lines
                .Skip(1)
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

                    columnYears.ForEach(x => country.Population.Add(int.Parse(x.Year), int.Parse(values[x.Index])));

                    countryList.Add(country);
                });


        }

        private void FilterByYear()
        {
            plot.Clear();
            int MaxYear = int.Parse(columnYears.Max().Year);
            int MinYear = int.Parse(columnYears.Min().Year);

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

            if ((int.Parse(this.toText.Text) > MaxYear || int.Parse(this.toText.Text) < MinYear) || (int.Parse(this.fromText.Text) > MaxYear || int.Parse(this.fromText.Text) < MinYear))
            {
                MessageBox.Show($"Please enter beetween {MinYear} and {MaxYear} !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            countryCheckBox.Items.Clear();
            plot.Clear();
            list
                .Where(c => c.Continent == "Europe")
                .ToList()
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
            countries.ForEach(country =>
            {
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

        private void resetButton_Click(object sender, EventArgs e)
        {
            while (countryCheckBox.CheckedIndices.Count > 0)
                countryCheckBox.SetItemChecked(countryCheckBox.CheckedIndices[0], false);

            DisplayAllCountry(countryList);
        }

        private void settingButton_Click(object sender, EventArgs e)
        {
            string path = GetPathFileExplorer();

            ReadCSV(path);
            
            plot.Clear();
            formsPlot1.Refresh();
            DisplayAllCountry(countryList);
        }
    }
}
