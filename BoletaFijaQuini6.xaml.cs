using System.Diagnostics;
using System.Drawing.Text;
using System.Linq.Expressions;

namespace LaFija;

public partial class BoletaFijaQuini6 : ContentPage
{
    //private List<int> numeros;
    DatabaseConfigSqlite databaseConfigSqlite = new DatabaseConfigSqlite(DatosComunes.BaseDeDatosLocal);
    ControlarBoletasFijas controlarBoletas = new ControlarBoletasFijas();
    //FuncionesGlobales funcionesGlobales= new FuncionesGlobales();
    //List<BoletaFija> boletasGuardadas= new List<BoletaFija>(); 
    public BoletaFijaQuini6()
	{
		InitializeComponent();

        var sorteos= databaseConfigSqlite.ObtenerListaSorteos();

        pckSorteosGuardados.ItemsSource = sorteos;

        if (sorteos.Any())
        {
            pckSorteosGuardados.SelectedIndex = 0; // El último elemento, ya que está ordenado por fecha descendente.
        }

        ControlarJugadas();
       
        num1.TextChanged += Entry_TextChanged;
        num2.TextChanged += Entry_TextChanged;
        num3.TextChanged += Entry_TextChanged;
        num4.TextChanged += Entry_TextChanged;
        num5.TextChanged += Entry_TextChanged;
        num6.TextChanged += Entry_TextChanged;

        num1.Unfocused += Entry_Unfocused;
        num2.Unfocused += Entry_Unfocused;
        num3.Unfocused += Entry_Unfocused;
        num4.Unfocused += Entry_Unfocused;
        num5.Unfocused += Entry_Unfocused;
        num6.Unfocused += Entry_Unfocused;
    }

    private void ControlarJugadas()
    {
        List<BoletaFija> boletasFijas = new List<BoletaFija>();
        boletasFijas = databaseConfigSqlite.ObtenerBoletasFijas();


        Sorteos sorteoSeleccionado = pckSorteosGuardados.SelectedItem as Sorteos;

        List<ResultadosSorteos> resultadosSorteos = new List<ResultadosSorteos>();
        foreach (BoletaFija boleta in boletasFijas)
        {
            ResultadosSorteos resultadosBoleta = new ResultadosSorteos();
            resultadosBoleta.boletaFija = boleta;
            resultadosBoleta.aciertosTradicional = controlarBoletas.ControlarSorteoTradicional(boleta, sorteoSeleccionado.id);
            resultadosBoleta.aciertosSegundaVuelta = controlarBoletas.ControlarSorteoLaSegunda(boleta, sorteoSeleccionado.id);
            resultadosBoleta.aciertosRevancha = controlarBoletas.ControlarSorteoRevancha(boleta, sorteoSeleccionado.id);
            resultadosBoleta.aciertosSiempreSale = controlarBoletas.ControlarSorteoSiempreSale(boleta, sorteoSeleccionado.id);
            resultadosBoleta.aciertosSorteoExtra = controlarBoletas.ControlarSorteoPozoExtra(boleta, sorteoSeleccionado.id);
            resultadosSorteos.Add(resultadosBoleta);
        }

        boletasFijasListView.ItemsSource = resultadosSorteos;
    }
    private void Entry_Unfocused(object sender, EventArgs e)
    {
        if (sender is Entry entry && !string.IsNullOrEmpty(entry.Text))
        {
            if (int.TryParse(entry.Text, out int num))
            {
                // Formatea el número con ceros a la izquierda y establece el resultado en el Entry
                entry.Text = num.ToString("D2"); // "D2" significa formato decimal con 2 dígitos
            }
        }
    }
    private void Entry_TextChanged(object sender, EventArgs e)
    {
        if (sender is Entry entry && !string.IsNullOrEmpty(entry.Text))
        {
            // Convierte el valor del Entry a un número entero
            if (int.TryParse(entry.Text, out int value))
            {
                // Verifica si el número supera el límite de 45
                if (value > 45)
                {
                    // Si el número supera el límite, muestra una advertencia
                    DisplayAlert("Advertencia", "El número no puede superar 45", "OK");
                    entry.Text = string.Empty; // Vacía el contenido del Entry
                }                

            }
            else
            {
                // Si el valor ingresado no es un número válido, muestra una advertencia
                DisplayAlert("Advertencia", "Ingrese un número válido", "OK");
                entry.Text = string.Empty; // Vacía el contenido del Entry
            }
        }
    }

    private void btnGuardar_Clicked(object sender, EventArgs e)
    {

        try
        {
            BoletaFija boletaFija = new BoletaFija()
            {
                nombre = nombreBoletaEntry.Text,
                num1 = num1.Text,
                num2 = num2.Text,
                num3 = num3.Text,
                num4 = num4.Text,
                num5 = num5.Text,
                num6 = num6.Text,
            };
            if (databaseConfigSqlite.AgregarBoletaFija(boletaFija))
            {
                DisplayAlert("Mensaje","Se guardo la boleta fija","Ok");
                num1.Text = string.Empty;
                num2.Text = string.Empty;
                num3.Text = string.Empty;   
                num4.Text = string.Empty;
                num5.Text = string.Empty;
                num6.Text = string.Empty;
                nombreBoletaEntry.Text = string.Empty;
                ControlarJugadas();
                //boletasFijas.ItemsSource = databaseConfigSqlite.ObtenerBoletasFijas();
            }
            
        }catch (Exception ex)
        {
            Debug.WriteLine("ERROR: " + ex);
        }
    }

    private void boletasFijas_ItemTapped(object sender, ItemTappedEventArgs e)
    {

    }

    private void pckSorteosGuardados_SelectedIndexChanged(object sender, EventArgs e)
    {
        ControlarJugadas();
    }
}