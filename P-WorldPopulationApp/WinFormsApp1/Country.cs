using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class Country
    {
        public string? Rank { get; set; }
        public string? CCA { get; set; }
        public string? CountryName { get; set; }
        public string? Capital { get; set; }
        public string? Continent { get; set; }
        public Dictionary<int, int>? Population { get; set; }
    }
}
