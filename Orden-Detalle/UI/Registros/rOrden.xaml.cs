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
    /// Interaction logic for rOrden.xaml
    /// </summary>
    public partial class rOrden : Window
    {
        public List<OrdenDetalle> Detalle { get; set; }
        public rOrden()
        {
            InitializeComponent();
            this.Detalle = new List<OrdenDetalle>();
            idTextBox.Text = "0";
            LlenaComboCliente();
            LlenaComboProducto();
        }

        private void LimpiarCampos()
        {
            idTextBox.Text = "0";
            clienteComboBox.SelectedIndex = 0;
            productoComboBox.SelectedIndex = 0;

            this.Detalle = new List<OrdenDetalle>();
            CargarDataGrid();
        }

        private Ordenes LlenaClase()
        {
            Ordenes ordenes = new Ordenes();
            ordenes.OrdenId = Convert.ToInt32(idTextBox.Text);

            if (Convert.ToInt32(idTextBox.Text) == 0)
            {
                ordenes.ClienteId = Convert.ToInt32(clienteComboBox.SelectedIndex);
            }
            else
            {
                ordenes.ClienteId = Convert.ToInt32(clienteComboBox.SelectedIndex);

            }

            ordenes.Producto = productoComboBox.SelectedIndex;
            ordenes.OrdenId = Convert.ToInt32(idTextBox.Text);
            ordenes.Fecha = fechaDatePicker.DisplayDate;
          //  ordenes.Cantidad = Convert.ToInt32(cantidadTextBox.Text);

            ordenes.Detalle = this.Detalle;

            return ordenes;
        }

        private void LlenaCampos(Ordenes ordenes)
        {
            idTextBox.Text = Convert.ToString(ordenes.OrdenId);
            clienteComboBox.SelectedIndex = ordenes.ClienteId;
            productoComboBox.SelectedIndex = ordenes.Producto;
            fechaDatePicker.SelectedDate = ordenes.Fecha;
            cantidadTextBox.Text = Convert.ToString(ordenes.Cantidad);

            this.Detalle = ordenes.Detalle;
            CargarDataGrid();

        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Ordenes ordenes = OrdenBLL.Buscar(Convert.ToInt32(idTextBox.Text));
            return (ordenes != null);
        }

        private bool Validar()
        {
            bool paso = true;
            

            if (string.IsNullOrWhiteSpace(clienteComboBox.Text))
            {
                MessageBox.Show("Debe Seleccionar un Cliente!!!");
                paso = false;
            }


            if (string.IsNullOrWhiteSpace(productoComboBox.Text))
            {
                MessageBox.Show("Debe Seleccionar un Producto!!!");
                paso = false;
            }


            

            return paso;
        }

        private void guardarButton_Click(object sender, RoutedEventArgs e)
        {
            Ordenes ordenes;
            bool paso = false;
            if (!Validar())
                return;

            ordenes = LlenaClase();

            if (Convert.ToInt32(idTextBox.Text) == 0)
                paso = OrdenBLL.Guardar(ordenes);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede Modificar una persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = OrdenBLL.Modificar(ordenes);
            }
            if (paso)
            {
              
                MessageBox.Show("Guardado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
            }
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo");
        }

        private void buscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Ordenes ordenes = new Ordenes();
            int.TryParse(idTextBox.Text, out id);
            ordenes = OrdenBLL.Buscar(id);

            if (ordenes != null)
            {
                MessageBox.Show("Persona Encontrada", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                LlenaCampos(ordenes);

            }
            else
            {
                MessageBox.Show("Persona no econtrada", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                LimpiarCampos();
            }
        }

        private void eliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(idTextBox.Text, out id);


            if (OrdenBLL.Eliminar(id))
            {

              MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Erro Al eliminar una persona");
        }


        private void CargarDataGrid()
        {
            detalleDataGrid.ItemsSource = null;
            detalleDataGrid.ItemsSource = this.Detalle;
        }

        private void agregarButton_Click(object sender, RoutedEventArgs e)
        {
            Productos productos = new Productos();

            if (detalleDataGrid.SelectedItem != null)
                this.Detalle = (List<OrdenDetalle>)detalleDataGrid.ItemsSource;
            Productos prec = ProductoBLL.Buscar(Convert.ToInt32(idTextBox.Text));

            this.Detalle.Add(
                new OrdenDetalle(
                    id:  0,
                    ordenId: Convert.ToInt32(idTextBox.Text),
                    articuloId: productoComboBox.SelectedIndex,
                    cantidad: Convert.ToInt32(cantidadTextBox.Text),
                    precio: prec.Precio

                    ));

            CargarDataGrid();
            productoComboBox.SelectedIndex = 0;
            clienteComboBox.SelectedIndex = 0;
            cantidadTextBox.Clear();

                  
        }

        private void removerButton_Click(object sender, RoutedEventArgs e)
        {
            if (detalleDataGrid.Columns.Count > 0 && detalleDataGrid.SelectedCells != null)
            {
                Detalle.RemoveAt(detalleDataGrid.SelectedIndex);
                CargarDataGrid();
            }
        }

        private void LlenaComboCliente()
        {
            clienteComboBox.ItemsSource = ClienteBLL.GetList(c => true);
            clienteComboBox.DisplayMemberPath = "Nombre";
            clienteComboBox.SelectedValuePath = "ClienteId";
        }

        private void LlenaComboProducto()
        {
            productoComboBox.ItemsSource = ProductoBLL.GetList(c => true);
            productoComboBox.DisplayMemberPath = "Descripcion";
            productoComboBox.SelectedValuePath = "ProductoId";
        }

        private void agregarcliente_Click(object sender, RoutedEventArgs e)
        {
            rCliente registro = new rCliente();
            registro.ShowDialog();
        }

        private void agregarproducto_Click(object sender, RoutedEventArgs e)
        {
            rProducto registro = new rProducto();
            registro.ShowDialog();
        }
    }
}
