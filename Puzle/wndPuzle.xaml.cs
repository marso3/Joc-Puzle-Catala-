using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Puzle
{
    /// <summary>
    /// Interaction logic for wndPuzle.xaml
    /// </summary>
    public partial class wndPuzle : Window
    {
        List<SuperButton> listButtons = new();
        DispatcherTimer rellotge;
        TimeSpan tempsTranscorregut;
        SolidColorBrush pinzellCorrecte = new(Color.FromArgb(255, 184, 255, 103));
        SolidColorBrush pinzellIncorrecte = new(Color.FromArgb(255, 255, 200, 120));
        public wndPuzle(int nFiles, int nColumnes)
        {
            InitializeComponent();
            grdPuzle.NCorrectes = 0;
            IniciarRellotge();

            grdPuzle.NFiles = nFiles;
            grdPuzle.NColumnes = nColumnes;

            int[] posicionsFitxes = CrearArrayRandomitzat();
            while (!EsResoluble(posicionsFitxes))
                posicionsFitxes = CrearArrayRandomitzat();
            grdPuzle.PosicionsFitxes = posicionsFitxes;
            InicialitzarGrid(posicionsFitxes);
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            SuperButton btn = (SuperButton)sender;
            if (grdPuzle.Forat.Fila == btn.Fila)
            {
                if (grdPuzle.Forat.Columna < btn.Columna)
                {
                    int posicions = btn.Columna - grdPuzle.Forat.Columna;
                    int nForat = grdPuzle.Forat.NActual;
                    for (int i = 0; i < posicions; i++)
                    {
                        Moure(listButtons[nForat + i]);
                    }
                }
                else if (grdPuzle.Forat.Columna > btn.Columna)
                {
                    int posicions = grdPuzle.Forat.Columna - btn.Columna;
                    for (int i = posicions - 1; i >= 0; i--)
                    {
                        Moure(listButtons[btn.NActual + i - 1]);
                    }
                }
            }
            else if (grdPuzle.Forat.Columna == btn.Columna)
            {
                if (grdPuzle.Forat.Fila < btn.Fila)
                {
                    int posicions = btn.Fila - grdPuzle.Forat.Fila;
                    for (int i = 0; i < posicions; i++)
                    {
                        Moure(listButtons[grdPuzle.Forat.NActual - 1 + grdPuzle.NColumnes]);
                    }
                }
                else if (grdPuzle.Forat.Fila > btn.Fila)
                {
                    int posicions = grdPuzle.Forat.Fila - btn.Fila;
                    for (int i = posicions; i > 0; i--)
                    {
                        Moure(listButtons[grdPuzle.Forat.NActual - 1 - grdPuzle.NColumnes]);
                    }
                }
            }
        }
        private void Moure(SuperButton botoClicat)
        {
            botoClicat.Refresh = true;
            listButtons.Swap(botoClicat, grdPuzle.Forat);
            RefrescarGrid();
        }

        private void InicialitzarGrid(int[] array)
        {
            for (int i = 0; i < grdPuzle.NFiles; i++)
                grdPuzle.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < grdPuzle.NColumnes; i++)
                grdPuzle.ColumnDefinitions.Add(new ColumnDefinition());

            int n = 0;
            for (int i = 0; i < grdPuzle.NFiles; i++)
            {
                for (int j = 0; j < grdPuzle.NColumnes; j++)
                {
                    SuperButton btn = new();
                    btn.Click += Btn_Click;
                    btn.NActual = n + 1;
                    btn.NObjectiu = array[n];
                    btn.Content = btn.NObjectiu;

                    if (btn.NActual == btn.NObjectiu)
                    {
                        btn.Background = pinzellCorrecte;
                        grdPuzle.NCorrectes++;
                    }
                    else
                        btn.Background = pinzellIncorrecte;
                    btn.Fila = i;
                    btn.Columna = j;

                    listButtons.Add(btn);

                    SuperGrid.SetRow(btn, i);
                    SuperGrid.SetColumn(btn, j);
                    grdPuzle.Children.Add(btn);

                    n++;
                }
            }
            sbiPercentatgeCompletat.Content = (100 * grdPuzle.NCorrectes / grdPuzle.PosicionsFitxes.Length) + "%";
            sbiNCorrectes.Content = grdPuzle.NCorrectes;
            grdPuzle.Forat = listButtons.Last();
            grdPuzle.Forat.Visibility = Visibility.Collapsed;
        }
        public void ReiniciarGrid(int[] array)
        {
            grdPuzle.NCorrectes = 0;
            grdPuzle.Children.Clear();
            listButtons.Clear();
            int n = 0;
            for (int i = 0; i < grdPuzle.NFiles; i++)
            {
                for (int j = 0; j < grdPuzle.NColumnes; j++)
                {
                    SuperButton btn = new();
                    btn.Click += Btn_Click;
                    btn.NActual = n + 1;
                    btn.NObjectiu = array[n];
                    btn.Content = btn.NObjectiu;

                    if (btn.NActual == btn.NObjectiu)
                    {
                        btn.Background = pinzellCorrecte;
                        grdPuzle.NCorrectes++;
                    }
                    else
                        btn.Background = pinzellIncorrecte;
                    btn.Fila = i;
                    btn.Columna = j;

                    listButtons.Add(btn);

                    SuperGrid.SetRow(btn, i);
                    SuperGrid.SetColumn(btn, j);
                    grdPuzle.Children.Add(btn);

                    n++;
                }
            }
            sbiPercentatgeCompletat.Content = (100 * grdPuzle.NCorrectes / grdPuzle.PosicionsFitxes.Length) + "%";
            sbiNCorrectes.Content = grdPuzle.NCorrectes;
            grdPuzle.Forat = listButtons.Last();
            grdPuzle.Forat.Visibility = Visibility.Collapsed;
        }
        private void RefrescarGrid()
        {
            grdPuzle.NCorrectes = 0;
            grdPuzle.Children.Clear();
            int n = 0;
            for (int i = 0; i < grdPuzle.NFiles; i++)
            {
                for (int j = 0; j < grdPuzle.NColumnes; j++)
                {
                    SuperButton btn = listButtons[n];
                    btn.Refresh = false;
                    btn.NActual = n + 1;
                    if (btn.NActual == btn.NObjectiu)
                    {
                        btn.Background = pinzellCorrecte;
                        grdPuzle.NCorrectes++;
                    }
                    else 
                        btn.Background = pinzellIncorrecte;
                    btn.Fila = i;
                    btn.Columna = j;

                    SuperGrid.SetRow(btn, i);
                    SuperGrid.SetColumn(btn, j);
                    grdPuzle.Children.Add(btn);
                    n++;
                }
            }
            sbiPercentatgeCompletat.Content = (100 * grdPuzle.NCorrectes / grdPuzle.PosicionsFitxes.Length) + "%";
            sbiNCorrectes.Content = grdPuzle.NCorrectes;
            grdPuzle.Forat.Visibility = Visibility.Collapsed;
            if ((100 * grdPuzle.NCorrectes / grdPuzle.PosicionsFitxes.Length) == 100)
                Acabar();
        }

        public int[] CrearArrayRandomitzat()
        {
            int n = grdPuzle.NFiles * grdPuzle.NColumnes;
            int[] array = new int[n];
            for (int i = 0; i < array.Length; i++)
                array[i] = i + 1;

            Random r = new Random();
            
            for (int i = n - 2; i > 0; i--)
            {
                int j = r.Next(0, i + 1);

                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
            return array;
        }
        public bool EsResoluble(int[] array)
        {
            bool result = false;
            int n = array.Length;
            int nDesordres = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (array[i] > array[j]) nDesordres++;
                }
            }
            if (nDesordres % 2 == 0)
                result = true;
            else
                result = false;
            return result;
        }

        public void IniciarRellotge()
        {
            tempsTranscorregut = TimeSpan.Zero;
            rellotge = new DispatcherTimer();
            rellotge.Interval = TimeSpan.FromMilliseconds(100);
            rellotge.Tick += Rellotge_Tick;
            rellotge.Start();
        }
        private void Rellotge_Tick(object? sender, EventArgs e)
        {
            tempsTranscorregut = tempsTranscorregut.Add(rellotge.Interval);
            ActualitzaTextRellotge(sbiRellotge, tempsTranscorregut);
        }
        private void ActualitzaTextRellotge(StatusBarItem sbiText, TimeSpan periode)
        {
            String cadena = String.Format("{0:00}:{1:00}",
                periode.Minutes,
                periode.Seconds);
            sbiText.Content = cadena;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.P)
            {
                rellotge.Stop();

                wndPausa wnd = new();
                wnd.Owner = this;
                wnd.ShowDialog();
                rellotge.Start();
            }
            else if (e.Key == Key.Down)
            {
                SuperButton btn = listButtons.Last();
                if (grdPuzle.Forat.Fila < btn.Fila)
                    Moure(listButtons[grdPuzle.Forat.NActual + grdPuzle.NColumnes - 1]);
            }
            else if (e.Key == Key.Up)
            {
                SuperButton btn = listButtons.Last();
                if (grdPuzle.Forat.Fila > 0)
                    Moure(listButtons[grdPuzle.Forat.NActual - grdPuzle.NColumnes - 1]);
            }
            else if (e.Key == Key.Right)
            {
                SuperButton btn = listButtons.Last();
                if (grdPuzle.Forat.Columna < btn.Columna)
                    Moure(listButtons[grdPuzle.Forat.NActual]);
            }
            else if (e.Key == Key.Left)
            {
                SuperButton btn = listButtons.Last();
                if (grdPuzle.Forat.Columna > 0)
                    Moure(listButtons[grdPuzle.Forat.NActual - 2]);
            }
        }
        private void Acabar()
        {
            rellotge.Stop();
            MessageBox.Show($"Felicitats!\nTEMPS: {sbiRellotge.Content}", "Has guanyat!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Owner.Show();
        }
    }
}
