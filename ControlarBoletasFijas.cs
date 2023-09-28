using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LaFija
{
   
    public class ControlarBoletasFijas
    {
        DatabaseConfigSqlite databaseConfigSqlite=new DatabaseConfigSqlite(DatosComunes.BaseDeDatosLocal);
        //public int idSorteo { get; }
        //public int idBoletaFija { get; }

        //public BoletaFija boletaFija =new BoletaFija();        
        //public void init()
        //{
        //    boletaFija=databaseConfigSqlite.ObtenerBoletasFijas().FirstOrDefault(boleta=>boleta.id==idBoletaFija);            
        //}
        public int ControlarSorteoTradicional(BoletaFija boletaFija,int idSorteo)
        {
            //init();
            
            string[] numerosBoleta = { boletaFija.num1, boletaFija.num2, boletaFija.num3, boletaFija.num4, boletaFija.num5, boletaFija.num6 };
            NumerosSorteados sorteoTradicional = databaseConfigSqlite.ObtenerNumerosSorteados(idSorteo, 1).FirstOrDefault(); ;

            List<int> numerosTradicional=new List<int> { sorteoTradicional.num1, sorteoTradicional.num2, sorteoTradicional.num3, sorteoTradicional.num4, sorteoTradicional.num5, sorteoTradicional.num6};

            int aciertos = numerosTradicional
            .Count(numeroSorteado => numerosBoleta.Contains(numeroSorteado.ToString("D2")));
                                     
            return aciertos;         
        }
        public int ControlarSorteoLaSegunda(BoletaFija boletaFija, int idSorteo)
        {
            string[] numerosBoleta = { boletaFija.num1, boletaFija.num2, boletaFija.num3, boletaFija.num4, boletaFija.num5, boletaFija.num6 };
            NumerosSorteados sorteoLaSegunda = databaseConfigSqlite.ObtenerNumerosSorteados(idSorteo, 2).FirstOrDefault();
            List<int> numerosLaSegunda = new List<int> { sorteoLaSegunda.num1, sorteoLaSegunda.num2, sorteoLaSegunda.num3, sorteoLaSegunda.num4, sorteoLaSegunda.num5, sorteoLaSegunda.num6 };

            int aciertos = numerosLaSegunda
            .Count(numeroSorteado => numerosBoleta.Contains(numeroSorteado.ToString("D2")));

            return aciertos;
        }
        public int ControlarSorteoRevancha(BoletaFija boletaFija, int idSorteo)
        {
            string[] numerosBoleta = { boletaFija.num1, boletaFija.num2, boletaFija.num3, boletaFija.num4, boletaFija.num5, boletaFija.num6 };
            NumerosSorteados sorteoRevancha = databaseConfigSqlite.ObtenerNumerosSorteados(idSorteo, 3).FirstOrDefault();

            List<int> numerosRevancha = new List<int> { sorteoRevancha.num1, sorteoRevancha.num2, sorteoRevancha.num3, sorteoRevancha.num4, sorteoRevancha.num5, sorteoRevancha.num6 };

            int aciertos = numerosRevancha
            .Count(numeroSorteado => numerosBoleta.Contains(numeroSorteado.ToString("D2")));

            return aciertos;
        }
        public int ControlarSorteoSiempreSale(BoletaFija boletaFija, int idSorteo)
        {
            string[] numerosBoleta = { boletaFija.num1, boletaFija.num2, boletaFija.num3, boletaFija.num4, boletaFija.num5, boletaFija.num6 };
            NumerosSorteados sorteoSiempreSale = databaseConfigSqlite.ObtenerNumerosSorteados(idSorteo, 4).FirstOrDefault();


            List<int> numerosSiempreSale = new List<int> { sorteoSiempreSale.num1, sorteoSiempreSale.num2, sorteoSiempreSale.num3, sorteoSiempreSale.num4, sorteoSiempreSale.num5, sorteoSiempreSale.num6 };

            int aciertos = numerosSiempreSale
            .Count(numeroSorteado => numerosBoleta.Contains(numeroSorteado.ToString("D2")));

            return aciertos;
        }
        public int ControlarSorteoPozoExtra(BoletaFija boletaFija, int idSorteo)

        {
            NumerosSorteados sorteoTradicional = databaseConfigSqlite.ObtenerNumerosSorteados(idSorteo, 1).FirstOrDefault();
            NumerosSorteados sorteoLaSegunda = databaseConfigSqlite.ObtenerNumerosSorteados(idSorteo, 2).FirstOrDefault();
            NumerosSorteados sorteoRevancha = databaseConfigSqlite.ObtenerNumerosSorteados(idSorteo, 3).FirstOrDefault();

            List<int> numerosTradicional = new List<int> { sorteoTradicional.num1, sorteoTradicional.num2, sorteoTradicional.num3, sorteoTradicional.num4, sorteoTradicional.num5, sorteoTradicional.num6 };
            List<int> numerosLasegunda = new List<int> { sorteoLaSegunda.num1, sorteoLaSegunda.num2, sorteoLaSegunda.num3, sorteoLaSegunda.num4, sorteoLaSegunda.num5, sorteoLaSegunda.num6 };
            List<int> numerosRevancha = new List<int> { sorteoRevancha.num1, sorteoRevancha.num2, sorteoRevancha.num3, sorteoRevancha.num4, sorteoRevancha.num5, sorteoRevancha.num6 };

            var conjuntoTradicional=new HashSet<int>(numerosTradicional);
            var conjuntoLaSegunda = new HashSet<int>(numerosLasegunda);
            var conjuntoRevancha = new HashSet<int>(numerosRevancha);

            conjuntoTradicional.UnionWith(conjuntoLaSegunda);
            conjuntoTradicional.UnionWith(conjuntoRevancha);
            List<int>numerosUnicosSorteadosPozoExtra=conjuntoTradicional.ToList();

            string[] numerosBoleta = { boletaFija.num1, boletaFija.num2, boletaFija.num3, boletaFija.num4, boletaFija.num5, boletaFija.num6 };
            int aciertos = numerosUnicosSorteadosPozoExtra
            .Count(numeroSorteado => numerosBoleta.Contains(numeroSorteado.ToString("D2"))); // Corregido aquí
        


            return aciertos;
        }
    }
}
