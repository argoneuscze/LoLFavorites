using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoLFavorites.Code
{
    public class Preset
    {
        public Preset()
        {
            chosen = new List<string>();
        }

        public string name;
        public List<string> chosen;
    }
}
