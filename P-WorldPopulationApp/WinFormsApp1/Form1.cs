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
        // Liste des �l�ments coch�s dans l'interface
        List<string> itemsChecked = new List<string>();

        // Liste des pays charg�s depuis le fichier CSV
        List<Country> countryList = new List<Country>();

        // Liste des ann�es disponibles dans le fichier CSV, avec leur index dans les colonnes
        List<(string Year, int Index)> columnYears = new List<(string Year, int Index)>();

        // Objet de graphique ScottPlot
        Plot plot;

        public Form1()
        {
            InitializeComponent();

            // Chemin du fichier CSV
            string path = "";

            // V�rifie si un chemin de fichier est sauvegard� et le charge si disponible
            if (File.Exists("path.txt"))
            {
                path = File.ReadAllLines("path.txt").ToArray()[0];
            }
            else
            {
                // Demande � l'utilisateur de choisir le fichier CSV s'il n'existe pas de chemin sauvegard�
                path = GetPathFileExplorer();
                File.WriteAllLines("path.txt", new string[] { path });
            }

            // Initialise le graphique ScottPlot
            plot = this.formsPlot1.Plot;

            // Initialise la liste de pays
            this.countryList = new List<Country>();

            // Lecture des donn�es du fichier CSV
            ReadCSV(path);

            // Affiche tous les pays dans l'interface
            DisplayAllCountry(countryList);
        }

        /// <summary>
        /// Ouvre une bo�te de dialogue pour choisir le fichier CSV
        /// </summary>
        /// <returns>
        /// Le chemin d'acc�s du fichier s�l�ctionner 
        /// </returns>
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

        /// <summary>
        /// G�re l'�v�nement de clic du bouton de recherche pour filtrer par ann�e
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            FilterByYear();
        }

        /// <summary>
        /// Lit et v�rifie les donn�es du fichier CSV
        /// </summary>
        /// <param name="path"></param>
        private void ReadCSV(string path)
        {
            countryList.Clear();

            try
            {
                List<string> lines = File.ReadAllLines(path).ToList();
                string[] header = lines[0].Split(',');

                // D�tecte les colonnes d'ann�es dans le header
                columnYears = header
                    .Select((column, index) => (Year: column, Index: index))
                    .Where(x => Regex.IsMatch(x.Year, "^[0-9]"))
                    .ToList();

                // Cr�e des objet country pour chaque ligne du fichier CSV
                lines
                    .Skip(1)
                    .ToList()
                    .ForEach(s =>
                    {
                        string[] values = s.Split(',');

                        Country country = new Country
                        {
                            Rank = values[0],
                            CCA = values[1],
                            CountryName = values[2],
                            Capital = values[3],
                            Continent = values[4],
                            Population = new Dictionary<int, int>()
                        };

                        columnYears.ForEach(x => country.Population.Add(int.Parse(x.Year), int.Parse(values[x.Index])));
                        countryList.Add(country);
                    });
            }
            catch (Exception ex)
            {
                // Supprime le chemin sauvegard� et affiche un message d'erreur en cas de probl�me de lecture
                File.Delete("path.txt");
                MessageBox.Show(
                    "Une erreur est survenue lors de la lecture des donn�es merci de bien vouloir v�rifier vos donn�es !",
                    "ERREUR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Filtre les donn�es affich�es par ann�e
        /// </summary>
        private void FilterByYear()
        {
            plot.Clear();
            int MaxYear = int.Parse(columnYears.Max().Year);
            int MinYear = int.Parse(columnYears.Min().Year);

            // Si les champs "From" ou "To" sont vides, affiche tous les pays
            if (this.fromText.Text == "" || this.toText.Text == "")
            {
                DisplayAllCountry(countryList);
                return;
            }

            // V�rifie que la plage d'ann�es est valide
            if (int.Parse(this.fromText.Text) > int.Parse(this.toText.Text))
            {
                MessageBox.Show("From value cannot be higher than To value !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // V�rifie que les ann�es sont dans la plage du dataset
            if ((int.Parse(this.toText.Text) > MaxYear || int.Parse(this.toText.Text) < MinYear) || (int.Parse(this.fromText.Text) > MaxYear || int.Parse(this.fromText.Text) < MinYear))
            {
                MessageBox.Show($"Please enter beetween {MinYear} and {MaxYear} !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // D�finit les limites du graphique selon les ann�es s�lectionn�es
            plot.Axes.SetLimitsX(int.Parse(this.fromText.Text), int.Parse(this.toText.Text));
            plot.Axes.AutoScale();

            DisplayAllCountry(countryList);

            this.checkBoxLock.Checked = true;
            this.formsPlot1.Refresh();
        }

        /// <summary>
        /// Affiche tous les pays sur le graphique et dans l'interface de s�lection
        /// </summary>
        /// <param name="list"></param>
        private void DisplayAllCountry(List<Country> list)
        {
            countryCheckBox.Items.Clear();
            plot.Clear();

            list.ForEach(country =>
            {
                int[] years = country.Population.Keys.ToArray();
                int[] pops = country.Population.Values.ToArray();

                // Ajoute le pays dans la liste de CheckBox
                this.countryCheckBox.Items.Add(country.CountryName);

                // Affiche les donn�es du pays sur le graphique
                plot.Add.Scatter(years, pops).LegendText = country.CountryName;
                plot.Legend.IsVisible = false;
                plot.Legend.Alignment = Alignment.MiddleCenter;
            });

            this.formsPlot1.Refresh();
        }

        /// <summary>
        /// G�re l'affichage des l�gendes du graphique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void legends_Click(object sender, EventArgs e)
        {
            this.legends.Text = !plot.Legend.IsVisible ? "Hide Legends" : "Display Legends";
            plot.Legend.IsVisible = !plot.Legend.IsVisible;
            this.formsPlot1.Refresh();
        }

        /// <summary>
        /// Active/desactive toutes les interactions sur le graphique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxLock_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLock.Checked)
                this.formsPlot1.Interaction.Disable();
            else
                this.formsPlot1.Interaction.Enable();
        }

        /// <summary>
        /// Autorise uniquement les saisies num�riques dans certains champs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Affiche uniquement les pays s�lectionn�s
        /// </summary>
        /// <param name="countries"></param>
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

        /// <summary>
        /// G�re la s�lection des pays dans la CheckBox et met � jour le graphique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void countryCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<Country> selectedCountry = new List<Country>();

            // Ajoute ou enl�ve les �l�ments coch�s de la liste
            // TODO: Changer la fa�on de Get l'item apr�s qu'il ait �t� coch�
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

        /// <summary>
        /// R�initialise la s�lection des pays et affiche tous les pays
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetButton_Click(object sender, EventArgs e)
        {
            while (countryCheckBox.CheckedIndices.Count > 0)
                countryCheckBox.SetItemChecked(countryCheckBox.CheckedIndices[0], false);

            DisplayAllCountry(countryList);
        }

        /// <summary>
        /// Ouvre une boite de dialogue pour choisir un nouveau fichier CSV et recharge les donn�es
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingButton_Click(object sender, EventArgs e)
        {
            string path = GetPathFileExplorer();
            File.WriteAllText("path.txt", path);
            ReadCSV(path);

            plot.Clear();
            formsPlot1.Refresh();
            DisplayAllCountry(countryList);
        }

        /// <summary>
        /// Adapte l'interface en fonction de la taille de la fen�tre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            formsPlot1.Size = new Size((int)(this.ClientSize.Width * 0.75), (int)(this.ClientSize.Height * 0.75));

            countryCheckBox.Height = (int)(formsPlot1.Height);

            countryCheckBox.Location = new Point(formsPlot1.Right + 50, formsPlot1.Top);

            checkBoxLock.Location = new Point(countryCheckBox.Right - checkBoxLock.Width, countryCheckBox.Bottom + 20);
            settingButton.Location = new Point(formsPlot1.Right - settingButton.Width, formsPlot1.Bottom + 20);
        }
    }
}
