using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoLFavorites.Code
{
    public class Main
    {
        public static void load()
        {
            // load everything
            LoadChampions.loadAllChampions();
            PresetOptions.loadPresets();
            Config.loadConfig();

            // first run settings
            if (Config.firstRun)
                Config.onFirstRun();

            // check for updates
            if (Config.autoUpdate)
                AutoUpdate.checkUpdates();
        }
    }
}
