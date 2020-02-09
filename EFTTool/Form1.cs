using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace EFTTool
{
    public partial class Form1 : Form
    {
        int currentMapIndex = 0; // Selected map
        int currentAmmoIndex = 0; // Selected ammo type

        private string[] maps = new string[] {"Customs","Factory", "Interchange", "Labs - Basement", "Labs - 1st Floor", "Labs - 2nd Floor", "Reserve", "Shoreline", "Woods"}; // Map names

        private Image[] mapImages = new Image[] { Resource1.CustomsMap, Resource1.FactoryMap,Resource1.InterchangeMap, Resource1.LabsBasementMap, Resource1.LabsFirstFloorMap, Resource1.LabsSecondFloorMap,Resource1.ReserveMap,
            Resource1.ShorelineMap, Resource1.WoodsMap }; // Map images

        private string[] ammo = new string[] {".366 TKM","4.6x30mm HK","5.7x28mm FN","5.45x39mm","5.56x45mm NATO","7.62x25mm Tokarev","7.62x39mm","7.62x51mm NATO","7.62x54mmR","9x18mm Makarov","9x19mm Parabellum","9x21mm Gyurza",
            "9x39mm","12.7x55mm STs-130","12.7x108mm","12x70mm","20x70mm"}; // Ammo type names

        private string[] urls = new string[] { "" }; // wiki links to ammo type pages

        public Form1()
        {
            InitializeComponent();
            InitializeMapTab();
            ParsePage("https://escapefromtarkov.gamepedia.com/7.62x39mm");
        }

        private async void ParsePage(string url)
        {
            HttpClient httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url); // Get page html

            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(html);

            var tableHtml = htmlDocument.DocumentNode.Descendants("table");

            var tableList = tableHtml.Take(1);
        }

        private void InitializeMapTab()
        {
            lbl1.Text = "Map:"; // Set correct label for drop down menu
            comboBox1.Items.Clear(); // Clear current list of items in drop menu

            foreach (string map in maps) // Add all maps to drop menu
            {
                comboBox1.Items.Add(map);
            }

            comboBox1.SelectedIndex = currentMapIndex; // Set the current map
            pictureBox1.Image = mapImages[currentMapIndex];
        }

        private void InitializeAmmoTab()
        {
            lbl1.Text = "Ammo:"; // Set correct label for drop down menu
            comboBox1.Items.Clear(); // Clear current list of items in drop menu

            foreach (string ammo in ammo) // Add all ammo types to drop menu
            {
                comboBox1.Items.Add(ammo);
            }

            comboBox1.SelectedIndex = currentAmmoIndex; // Set the current ammo type

        }

            private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) // On map tab
            {
                currentMapIndex = comboBox1.SelectedIndex;
                pictureBox1.Image = mapImages[currentMapIndex];
            }
            else // On ammo tab
            {
                currentAmmoIndex = comboBox1.SelectedIndex;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(tabControl1.SelectedIndex)
            {
                case 0: // Map tab selected
                    InitializeMapTab();
                    break;
                case 1: // Ammo tab selected
                    InitializeAmmoTab();
                    break;
            }
        }
    }

    struct Ammo
    {
        Image icon;
        string name;
        uint damage;
        uint pen; // penetration power
        uint armorDamage; // Armor Damage %
        int accuracy; // Accuracy %
        int recoil; // Recoil %
        float fragChance; // Fragmentation chance
        float ricochetChance;
        uint speed; // Projectile Speed (m/s)
        string specialEffects;
        string seller; // Sold by
    }
}
