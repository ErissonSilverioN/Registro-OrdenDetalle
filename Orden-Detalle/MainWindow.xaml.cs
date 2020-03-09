using Orden_Detalle.UI.Registros;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Orden_Detalle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegistrarCliente_Click(object sender, RoutedEventArgs e)
        {
            rCliente registro = new rCliente();
            registro.ShowDialog();
        }

        private void RegistrarProducto_Click(object sender, RoutedEventArgs e)
        {
            rProducto registro = new rProducto();
            registro.ShowDialog();
        }

        private void RegistrarOrder_Click(object sender, RoutedEventArgs e)
        {
            rOrden registro = new rOrden();
            registro.ShowDialog();
        }
    }
}
