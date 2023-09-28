using MauiPopup;
using MauiPopup.Views;

namespace LaFija;

public partial class NumerosSorteadosPop : BasePopupPage
{
    DatabaseConfigSqlite databaseConfigSqlite = new DatabaseConfigSqlite(DatosComunes.BaseDeDatosLocal);
    ControlarBoletasFijas controlarBoletas = new ControlarBoletasFijas();
    BoletaFija _boletaFija=new BoletaFija();
    public NumerosSorteadosPop(BoletaFija boletaSeleccionada, Sorteos sorteoSeleccionado)
	{
        InitializeComponent();
        _boletaFija = boletaSeleccionada;
        string[] numerosBoletaFija = { boletaSeleccionada.num1, boletaSeleccionada.num2, boletaSeleccionada.num3, boletaSeleccionada.num4, boletaSeleccionada.num5, boletaSeleccionada.num6 };

        NumerosSorteados numerosSorteadosTradicional = databaseConfigSqlite.ObtenerNumerosSorteados(sorteoSeleccionado.id,1).FirstOrDefault();
        NumerosSorteados numerosSorteadosLaSegunda = databaseConfigSqlite.ObtenerNumerosSorteados(sorteoSeleccionado.id, 2).FirstOrDefault();
        NumerosSorteados numerosSorteadosRevancha = databaseConfigSqlite.ObtenerNumerosSorteados(sorteoSeleccionado.id, 3).FirstOrDefault();
        NumerosSorteados numerosSorteadosSaleOSale = databaseConfigSqlite.ObtenerNumerosSorteados(sorteoSeleccionado.id, 4).FirstOrDefault();

		lblDatosSorteo.Text = "Número de sorteo: " + sorteoSeleccionado.numeroSorteo + " Fecha: " + sorteoSeleccionado.fecha.ToString("dd-MM-yyyy");
        lblDatosBoletaFija.Text = "MI BOLETA FIJA: " + boletaSeleccionada.nombre;
		num1MiBoleta.Text = boletaSeleccionada.num1.PadLeft(2, '0');
        num2MiBoleta.Text = boletaSeleccionada.num2.PadLeft(2, '0');
        num3MiBoleta.Text = boletaSeleccionada.num3.PadLeft(2, '0');
        num4MiBoleta.Text = boletaSeleccionada.num4.PadLeft(2, '0');
        num5MiBoleta.Text = boletaSeleccionada.num5.PadLeft(2, '0');    
        num6MiBoleta.Text = boletaSeleccionada.num6.PadLeft(2, '0');

        ConfigurarEtiqueta(num1Tradicional, numerosSorteadosTradicional.num1, numerosBoletaFija);
        ConfigurarEtiqueta(num2Tradicional, numerosSorteadosTradicional.num2, numerosBoletaFija);
        ConfigurarEtiqueta(num3Tradicional, numerosSorteadosTradicional.num3, numerosBoletaFija);
        ConfigurarEtiqueta(num4Tradicional, numerosSorteadosTradicional.num4, numerosBoletaFija);
        ConfigurarEtiqueta(num5Tradicional, numerosSorteadosTradicional.num5, numerosBoletaFija);
        ConfigurarEtiqueta(num6Tradicional, numerosSorteadosTradicional.num6, numerosBoletaFija);

        ConfigurarEtiqueta(num1LaSegunda, numerosSorteadosLaSegunda.num1, numerosBoletaFija);
        ConfigurarEtiqueta(num2LaSegunda, numerosSorteadosLaSegunda.num2, numerosBoletaFija);
        ConfigurarEtiqueta(num3LaSegunda, numerosSorteadosLaSegunda.num3, numerosBoletaFija);
        ConfigurarEtiqueta(num4LaSegunda, numerosSorteadosLaSegunda.num4, numerosBoletaFija);
        ConfigurarEtiqueta(num5LaSegunda, numerosSorteadosLaSegunda.num5, numerosBoletaFija);
        ConfigurarEtiqueta(num6LaSegunda, numerosSorteadosLaSegunda.num6, numerosBoletaFija);

        ConfigurarEtiqueta(num1Revancha, numerosSorteadosRevancha.num1, numerosBoletaFija);
        ConfigurarEtiqueta(num2Revancha, numerosSorteadosRevancha.num2, numerosBoletaFija);
        ConfigurarEtiqueta(num3Revancha, numerosSorteadosRevancha.num3, numerosBoletaFija);
        ConfigurarEtiqueta(num4Revancha, numerosSorteadosRevancha.num4, numerosBoletaFija);
        ConfigurarEtiqueta(num5Revancha, numerosSorteadosRevancha.num5, numerosBoletaFija);
        ConfigurarEtiqueta(num6Revancha, numerosSorteadosRevancha.num6, numerosBoletaFija);

        ConfigurarEtiqueta(num1SiempreSale, numerosSorteadosSaleOSale.num1, numerosBoletaFija);
        ConfigurarEtiqueta(num2SiempreSale, numerosSorteadosSaleOSale.num2, numerosBoletaFija);
        ConfigurarEtiqueta(num3SiempreSale, numerosSorteadosSaleOSale.num3, numerosBoletaFija);
        ConfigurarEtiqueta(num4SiempreSale, numerosSorteadosSaleOSale.num4, numerosBoletaFija);
        ConfigurarEtiqueta(num5SiempreSale, numerosSorteadosSaleOSale.num5, numerosBoletaFija);
        ConfigurarEtiqueta(num6SiempreSale, numerosSorteadosSaleOSale.num6, numerosBoletaFija);
        
        aciertosSorteoExtra.Text = controlarBoletas.ControlarSorteoPozoExtra(boletaSeleccionada,sorteoSeleccionado.id).ToString();

    }

    void ConfigurarEtiqueta(Label etiqueta, int numero, string[] numerosBoletaFija)
    {
        string numeroFormateado = numero.ToString("D2");
        etiqueta.Text = numeroFormateado;

        if (Array.IndexOf(numerosBoletaFija, numeroFormateado) != -1)
        {
            etiqueta.BackgroundColor = Color.FromArgb("ef233c");
            etiqueta.TextColor = Color.FromArgb("#ffffff");
        }
    }
    private async void btnEliminarBoleta_Clicked(object sender, EventArgs e)
    {
       if(databaseConfigSqlite.EliminarBoletaFija(_boletaFija))
        {
            await DisplayAlert("Eliminar", "Boleta eliminada", "Ok");
            await PopupAction.ClosePopup(true);
        }
    }
}