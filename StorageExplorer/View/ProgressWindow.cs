using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StorageExplorer.View
{
    public partial class ProgressWindow : Form
    {
        public int Index;
        private Controller.Controller controller;

        public ProgressWindow(Controller.Controller _controller, int _index, string _title)
        {
            InitializeComponent();
            this.Index = _index;
            this.controller = _controller;

            this.Text = _title;
            button1.Text = controller.GetTranslation("cancel");
            lbDestination.Text = controller.GetTranslation("destination")+": ";
            lbCurrentItem.Text = controller.GetTranslation("item")+": ";
            lbTotalProgress.Text = controller.GetTranslation("total_progress")+": ";
        }

        public void SetProgress(string _destination, string _currentItemName, int _movedItems, int _totalItems, float _currentItemProgress, float _totalProgress)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.lbDestination.Text = FormatPath(controller.GetTranslation("destination") + ": " + _destination);
                this.lbCurrentItem.Text = FormatPath(controller.GetTranslation("item") + ": " + _currentItemName);
                this.lbCounter.Text = _movedItems + "/" + _totalItems;
                this.lbItemProgPerc.Text = _currentItemProgress.ToString("0.0")+"%";
                this.lbTotalProgPerc.Text = _totalProgress.ToString("0.0")+"%";
                this.progressBar1.Value = Math.Max(Math.Min((int)Math.Round(_currentItemProgress), 100), 0);
                this.progressBar2.Value = Math.Max(Math.Min((int)Math.Round(_totalProgress), 100), 0);
            });
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string FormatPath(string _path)
        {
            // max. 60 karakter hosszú lehet a visszadobott szöveg
            int MAX_LENGTH = 60;

            // Az eredmény alapértéke a paraméterként kapott útvonal. 
            // Ha egyszer sem tud lefutni a ciklus, akkor visszaadjuk az eredeti szöveget
            string result = _path;

            // feldaraboljuk az útvonalat a \ jelek mentén
            string[] pieces = _path.Split('\\');

            // számláló: hány darab könyvtárnevet veszünk ki az útvonalból
            // minden ciklus körrel nő egyel az értéke, amíg nem lesz a szöveg kellően rövid
            int count = 1; 

            // A ciklus addig mehet, amíg az első és az utolsó darabon kívül ki lehet venni 'count' darabot,
            // és a maradék szöveg hossza meghaladja a maximális hosszt.
            while (pieces.Length - count >= 2 && result.Length > MAX_LENGTH)
            {
                // Az eredmény kezdetben üres lesz
                result = string.Empty;

                // Hozzáfűzzük az első (pieces.Length - count - 1) darab elemet
                // Az ezután következő 'count' darab elem kihagyásra kerül
                for (int i = 0; i < pieces.Length - 1 - count; i++)
                {
                    result += pieces[i] + "\\";
                }
                // a kihagyott elemeket a "...\" jelöléssel helyettesítjük, 
                // hogy lássa a felhasználó, hogy itt kimaradt egy darab az útvonalból
                result += "...\\";
                
                // Végül hozzáfűzzük az utolsó darabot
                result += pieces[pieces.Length - 1];

                // növeljül a kivágandó darabok számát, arra az esetre,
                // ha az előbb elkészített szöveg nem lenne elég rövid
                count++;
            }

            return result;
        }
    }
}
