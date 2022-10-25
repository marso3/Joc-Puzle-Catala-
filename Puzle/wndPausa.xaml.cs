using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Puzle
{
    /// <summary>
    /// Interaction logic for wndPausa.xaml
    /// </summary>
    public partial class wndPausa : Window
    {
        public wndPausa()
        {
            InitializeComponent();
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReiniciar_Click(object sender, RoutedEventArgs e)
        {
            wndPuzle wndPare = (wndPuzle)Window.GetWindow(this.Owner);
            wndPare.ReiniciarGrid(wndPare.grdPuzle.PosicionsFitxes);
            wndPare.IniciarRellotge();
            this.Close();
        }

        private void btnInici_Click(object sender, RoutedEventArgs e)
        {
            wndPuzle wndPare = (wndPuzle)Window.GetWindow(this.Owner);
            wndPare.Close();
            this.Close();
        }
    }
}
