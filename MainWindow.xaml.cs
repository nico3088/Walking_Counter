using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.IO;

namespace Walking_Debt
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        public class Walk
        {
            public string Who { get; set; }
            public int Block { get; set; }
            public DateTime Date { get; set; }
            public int Blocks_Fran = 0;
            public int Blocks_Negro = 0;

            public Walk(string who, int block, DateTime date)
            {
                Who = who;
                Block = block;
                Date = date;
            }

            public override string ToString()
            {
                return $"{Who} | {Block.ToString("D2")} | {Date.ToString("dd/MM/yyyy")}";
            }
        }

        private List<Walk> list = new List<Walk>();

        public int Blocks_Fran { get; private set; }
        public int Blocks_Negro { get; private set; }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string who = Combo.SelectedValue != null ? ((ComboBoxItem)Combo.SelectedItem).Content.ToString() : "";
            int block = (int)Numeric_UpDown.Value;
            DateTime date = DateTime.Now;

            Walk data = new Walk(who, block, date);

            list.Add(data);
            lista.ItemsSource = null;
            lista.ItemsSource = list;
            if (who == "Fran")
            {
                Blocks_Fran += block;
            }
            else if (who == "Negro")
            {
                Blocks_Negro += block;
            }


            Contador_Fran.Content = $"{Blocks_Fran.ToString("D2")} ";
            Contador_Negro.Content = $"{Blocks_Negro.ToString("D2")} ";
            int Debt_Negro = Blocks_Fran - Blocks_Negro;
            int Debt_Fran = Blocks_Negro - Blocks_Fran;
            string Mayor_Debt_Who = "";
            int Mayor_Debt = Debt_Negro;

            if (Debt_Fran > Debt_Negro)
            {
                Mayor_Debt_Who = "Fran";
                Mayor_Debt = Debt_Fran;
            }
            else if (Debt_Negro > Debt_Fran)
            {
                Mayor_Debt_Who = "Negro";
                Mayor_Debt = Debt_Negro;
            }

            Debt.Text = Mayor_Debt.ToString("D2");
            if (Mayor_Debt_Who != "")
            {
                debt_textblock.Text = $"{Mayor_Debt_Who} debt's";

            }

        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Blocks_Fran = 0;
            Blocks_Negro = 0;
            list.Clear();
            lista.ItemsSource = null;
            Contador_Fran.Content = $"{Blocks_Fran:00} ";
            Contador_Negro.Content = $"{Blocks_Negro:00} ";
            debt_textblock.Text = "";
            Debt.Text = "00";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "Archivos de Texto (*.txt)|*.txt";

            if (dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;
                List<string> items = new List<string>();
                foreach (var item in lista.Items)
                {
                    items.Add($"{((Walk)item).Who}\t{((Walk)item).Block}\t{((Walk)item).Date:dd/MM/yyyy}");
                }

                File.WriteAllLines(filePath, items);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }

}
