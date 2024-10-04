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
        public int Population2022 { get; set; }
        public int Population2020 { get; set; }
        public int Population2015 { get; set; }
        public int Population2010 { get; set; }
        public int Population2000 { get; set; }
    }
}
