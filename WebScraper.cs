using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaFija
{
    internal class ObtenerDatosSitioWeb
    {
        public async Task<List<string>> ObtenerNumerosSorteo(string url)
        {
            HttpClient client = new HttpClient();
            string html = await client.GetStringAsync(url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Buscar el elemento <div> por su id
            HtmlNode divNode = doc.GetElementbyId("q_pnlResultados");

            // Buscar los elementos <td> dentro de la tabla
            HtmlNodeCollection tdNodes = divNode.SelectNodes("//table//td[@class='nro']");

            // Extraer el contenido de los elementos <td> (los números)
            List<string> numeros = new List<string>();
            foreach (HtmlNode tdNode in tdNodes)
            {
                string numero = tdNode.InnerText.Trim();
                numeros.Add(numero);
            }

            return numeros;
        }
        public async Task<string> ObtenerDatosSorteo(string url)
        {
            HttpClient client = new HttpClient();
            string html = await client.GetStringAsync(url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Buscar el elemento <div> por su id
            HtmlNode etiquetaP = doc.DocumentNode.SelectSingleNode("//p[@class='lead']");

            // Extraer el texto dentro de la etiqueta <strong>
            string texto = etiquetaP.SelectSingleNode("strong").InnerText;

            // Imprimir el resultado
            return texto.Trim();
                
        }
    }
}
