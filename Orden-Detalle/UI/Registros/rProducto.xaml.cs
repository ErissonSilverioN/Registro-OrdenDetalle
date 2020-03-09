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
    /// Interaction logic for rProducto.xaml
    /// </summary>
    public partial class rProducto : Window
    {
        public rProducto()
        {
            InitializeComponent();
            idTextBox.Text = "0";
        }

        private void LimpiarCampos()
        {
            descripcionTextBox.Text = string.Empty;
            precioTextBox.Text = string.Empty;
            inventarioTextBox.Text = string.Empty;
            idTextBox.Text = "0";

        }

        private Productos LlenaClase()
        {
            Productos productos = new Productos();

            productos.ProductoId = Convert.ToInt32(idTextBox.Text);
            productos.Descripcion = descripcionTextBox.Text;
            productos.Precio = Convert.ToDecimal(precioTextBox.Text);
            //  productos.Inventario = Convert.ToDecimal(inventarioTextBox.Text);
            


            return productos;
        }

        private void LlenaCampo(Productos productos)
        {

            idTextBox.Text = Convert.ToString(productos.ProductoId);
            descripcionTextBox.Text = productos.Descripcion;
            precioTextBox.Text = Convert.ToString(productos.Precio);
            // inventarioTextBox.Text = Convert.ToString(productos.Inventario);
            


        }

        private bool VerificarExistencia()
        {
            Productos productos = ProductoBLL.Buscar((int)Convert.ToInt32(idTextBox.Text));
            return (productos != null);
        }


        private bool ValidarCampos()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(descripcionTextBox.Text))
            {
                MessageBox.Show("Llenar Campo Descripcion!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(precioTextBox.Text))
            {
                MessageBox.Show("Llenar Campo Precio!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                paso = false;
            }

            return paso;

        }

        private void guardarButton_Click(object sender, RoutedEventArgs e)
        {
            Productos productos;
            bool paso = false;

            if (!ValidarCampos())
                return;

            productos = LlenaClase();

            if (Convert.ToInt32(idTextBox.Text) == 0)
                paso = ProductoBLL.Guardar(productos);

            else
            {
                if (!VerificarExistencia())
                {
                    MessageBox.Show("Producto No Existe!!");
                }

                paso = ProductoBLL.Modificar(productos);
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

        private void buscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(idTextBox.Text, out id);
            Productos productos = new Productos();

            productos = ProductoBLL.Buscar(id);

            if (productos != null)
            {
                MessageBox.Show("Producto Encontrado");
                LlenaCampo(productos);
            }
            else
            {
                MessageBox.Show("Producto NO Encontrado!!");
            }
        }

        private void eliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(idTextBox.Text, out id);

            if (ProductoBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado!!", "Existo", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se Elimino!!");
            }
        }

        private void nuevoButton_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }
    }
}
