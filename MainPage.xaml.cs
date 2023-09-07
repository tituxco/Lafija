using System.Globalization;

namespace LaFija;

public partial class MainPage : ContentPage
{
	int count = 0;
    DatabaseConfigSqlite databaseConfigSqlite=new DatabaseConfigSqlite(DatosComunes.BaseDeDatosLocal);
	public MainPage()
	{
		InitializeComponent();        
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		await GetWebsiteData();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await GetWebsiteData();
    }
    private async Task GetWebsiteData()
    {
        try
        {
            activityIndicator.IsRunning = true;
            databaseConfigSqlite.AgregarDatosInicio();

            string url = "https://www.quini-6-resultados.com.ar/";
            ObtenerDatosSitioWeb scraper = new ObtenerDatosSitioWeb();
            List<string> numerosObtener = await scraper.ObtenerNumerosSorteo(url);
            string sorteoObtener = await scraper.ObtenerDatosSorteo(url);

            int numSorteo = int.Parse(sorteoObtener.Substring(sorteoObtener.Length - 4).Trim());
            DateTime fecha;

            if (DateTime.TryParseExact(sorteoObtener.Substring(15, 10), "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out fecha))
            {
                // La cadena contiene una fecha válida
                Console.WriteLine("La cadena contiene una fecha válida: " + fecha.ToString("dd/MM/yyyy"));
            }

            Sorteos sorteo = new Sorteos()
            {
                numeroSorteo = numSorteo,
                fecha = fecha,
            };

            int idSorteoNuevo = databaseConfigSqlite.AgregarSorteoNuevo(sorteo);

            string resultados = "";
            for (int i = 0; i < numerosObtener.Count; i++)
            {
                if (i == 0)
                {
                    resultados = "resultados tradicional \n" + numerosObtener[i] + "\n";
                    string[] NumerosTradicional = numerosObtener[i].Split('-');

                    List<int> listaNumeros = new List<int>();

                    foreach (string numero in NumerosTradicional)
                    {
                        if (int.TryParse(numero, out int numeroEntero))
                        {
                            listaNumeros.Add(numeroEntero);
                        }
                    }

                    NumerosSorteados numerosSorteados = new NumerosSorteados()
                    {
                        idSorteo = idSorteoNuevo,
                        idTipoSorteo = 1,
                        numSorteo = numSorteo,
                        num1 = listaNumeros[0],
                        num2 = listaNumeros[1],
                        num3 = listaNumeros[2],
                        num4 = listaNumeros[3],
                        num5 = listaNumeros[4],
                        num6 = listaNumeros[5],
                    };
                    databaseConfigSqlite.AgregarNumerosSorteados(numerosSorteados);
                }
                else if (i == 1)
                {
                    resultados += "resultados la segunda \n" + numerosObtener[i] + "\n";

                    string[] NumerosTradicional = numerosObtener[i].Split('-');

                    List<int> listaNumeros = new List<int>();

                    foreach (string numero in NumerosTradicional)
                    {
                        if (int.TryParse(numero, out int numeroEntero))
                        {
                            listaNumeros.Add(numeroEntero);
                        }
                    }

                    NumerosSorteados numerosSorteados = new NumerosSorteados()
                    {
                        idSorteo = idSorteoNuevo,
                        idTipoSorteo = 2,
                        numSorteo = numSorteo,
                        num1 = listaNumeros[0],
                        num2 = listaNumeros[1],
                        num3 = listaNumeros[2],
                        num4 = listaNumeros[3],
                        num5 = listaNumeros[4],
                        num6 = listaNumeros[5],
                    };
                    databaseConfigSqlite.AgregarNumerosSorteados(numerosSorteados);
                }
                else if (i == 2)
                {
                    resultados += "resultados revancha \n" + numerosObtener[i] + "\n";
                    string[] NumerosTradicional = numerosObtener[i].Split('-');

                    List<int> listaNumeros = new List<int>();

                    foreach (string numero in NumerosTradicional)
                    {
                        if (int.TryParse(numero, out int numeroEntero))
                        {
                            listaNumeros.Add(numeroEntero);
                        }
                    }

                    NumerosSorteados numerosSorteados = new NumerosSorteados()
                    {
                        idSorteo = idSorteoNuevo,
                        idTipoSorteo = 3,
                        numSorteo = numSorteo,
                        num1 = listaNumeros[0],
                        num2 = listaNumeros[1],
                        num3 = listaNumeros[2],
                        num4 = listaNumeros[3],
                        num5 = listaNumeros[4],
                        num6 = listaNumeros[5],
                    };
                    databaseConfigSqlite.AgregarNumerosSorteados(numerosSorteados);

                }
                else if (i == 3)
                {
                    resultados += "resultados Siempre sale \n" + numerosObtener[i];
                    string[] NumerosTradicional = numerosObtener[i].Split('-');

                    List<int> listaNumeros = new List<int>();

                    foreach (string numero in NumerosTradicional)
                    {
                        if (int.TryParse(numero, out int numeroEntero))
                        {
                            listaNumeros.Add(numeroEntero);
                        }
                    }

                    NumerosSorteados numerosSorteados = new NumerosSorteados()
                    {
                        idSorteo = idSorteoNuevo,
                        idTipoSorteo = 4,
                        numSorteo = numSorteo,
                        num1 = listaNumeros[0],
                        num2 = listaNumeros[1],
                        num3 = listaNumeros[2],
                        num4 = listaNumeros[3],
                        num5 = listaNumeros[4],
                        num6 = listaNumeros[5],
                    };
                    databaseConfigSqlite.AgregarNumerosSorteados(numerosSorteados);

                }
            }


            string fechaSorteo = fecha.ToString("dd/MM/yyyy");

            myLabel.Text = "Fecha de sorteo: " + fechaSorteo + " Numero de sorteo: " + numSorteo + "\n";
            myLabel.Text += resultados;
        }catch (Exception ex)
        {
            await DisplayAlert("Error","Error: " + ex.Message, "Ok");
        }
        finally
        {
            activityIndicator.IsRunning = false;
        }
        
    }

    private void CounterBtn2_Clicked(object sender, EventArgs e)
    {

        var nuevaPagina = new BoletaFijaQuini6();

        // Navegar a la nueva página
        Navigation.PushAsync(nuevaPagina);

    }
}

