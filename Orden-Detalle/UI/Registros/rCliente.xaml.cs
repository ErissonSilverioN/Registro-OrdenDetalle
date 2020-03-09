using Orden_Detalle.BLL;
using Orden_Detalle.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Orden_Detalle.UI.Registros
{
    /// <summary>
    /// Interaction logic for rCliente.xaml
    /// </summary>
    public partial class rCliente : Window
    {
        public rCliente()
        {
            InitializeComponent();
        }

        private void LimpiarCampos()
        {
            nombreTextBox.Text = string.Empty;
            idTextBox.Text = "0";

        }


        private Clientes LlenaClase()
        {
            Clientes clientes = new Clientes();

            clientes.ClienteId = Convert.ToInt32(idTextBox.Text);
            clientes.Nombre = nombreTextBox.Text;
            

            return clientes;
        }

        private void LlenaCampo(Clientes clientes)
        {

            idTextBox.Text = Convert.ToString(clientes.ClienteId);
            nombreTextBox.Text = clientes.Nombre;
           

        }

        private bool VerificarExistencia()
        {
            Clientes clientes = ClienteBLL.Buscar((int)Convert.ToInt32(idTextBox.Text));
            return (clientes != null);
        }

        private bool ValidarCampos()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(nombreTextBox.Text))
            {
                MessageBox.Show("Llenar Campo Nombre!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                paso = false;
            }

            

            return paso;
        }

        private void guardarButton_Click(object sender, RoutedEventArgs e)
        {
            Clientes clientes;
            bool paso = false;

            if (!ValidarCampos())
                return;

            clientes = LlenaClase();

            if (Convert.ToInt32(idTextBox.Text) == 0)
                paso = ClienteBLL.Guardar(clientes);

            else
            {
                if (!VerificarExistencia())
                {
                    MessageBox.Show("Cliente No Existe!!");
                }

                paso = ClienteBLL.Modificar(clientes);
            }

            if (paso)
            {
                MessageBox.Show("Guardado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al Guardar!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        private void eliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(idTextBox.Text, out id);

            if (ClienteBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado!!", "Existo", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se Elimino!!");
            }
        }



        private void buscarButton1_Click(object sender, RoutedEventArgs e)
        {
            int id;

            Clientes clientes = new Clientes();
            int.TryParse(idTextBox.Text, out id);


            clientes = ClienteBLL.Buscar(id);

            if (clientes != null)
            {
                MessageBox.Show("Esncotrado!!");
                LlenaCampo(clientes);
            }
        }

        private void nuevoButton_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }
    }
}
