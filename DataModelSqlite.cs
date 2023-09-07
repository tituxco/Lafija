using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaFija
{
    [Table("Sorteos")]
    public class Sorteos
    {
        [PrimaryKey,  AutoIncrement]
        public int id { get; set; }        
        public DateTime fecha { get; set; }
        public int numeroSorteo { get; set; }
    }

    [Table("TiposSorteos")]
    public class TiposSorteos
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string nombre { get; set; }               
    }

    [Table("NumerosSorteados")]
    public class NumerosSorteados
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int idSorteo { get; set; }
        public int numSorteo { get; set; }
        public int idTipoSorteo { get; set; }
        public int num1 { get; set; }
        public int num2 { get; set; }
        public int num3 { get; set; }
        public int num4 { get; set; }
        public int num5 { get; set; }
        public int num6 { get; set; }
    }

    [Table("BoletaFija")]
    public class BoletaFija
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string nombre { get; set; }
        public string num1 { get; set; }
        public string num2 { get; set; }
        public string num3 { get; set; }
        public string num4 { get; set; }
        public string num5 { get; set; }
        public string num6 { get; set; }
    }

    public class ResultadosSorteos
    {
        public BoletaFija boletaFija { get; set; } = new BoletaFija();
        public int aciertosTradicional { get; set; }
        public int aciertosSegundaVuelta { get; set; }
        public int aciertosRevancha { get; set; }
        public int aciertosSiempreSale { get; set; }
        public int aciertosSorteoExtra {get;set;}
    }
}
