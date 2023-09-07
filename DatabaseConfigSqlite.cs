using SQLite;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LaFija
{
    public class DatabaseConfigSqlite
    { 
        string _dbPath;
        public string StatusMensage { get; set; }

        private SQLiteConnection conn;

        private void Init()
        {
            if (conn is not null)            
                return;
        conn = new(_dbPath);
        conn.CreateTable<Sorteos>();
        conn.CreateTable<TiposSorteos>();
        conn.CreateTable<NumerosSorteados>();
        conn.CreateTable<BoletaFija>();

        }
        public DatabaseConfigSqlite(string dbPath)
        {
            _dbPath = dbPath;
            Debug.WriteLine(_dbPath);
        }
        public bool AgregarDatosInicio()
        {
            try
            {
                Init();
                Debug.WriteLine("BASE DE DATOS LOCAL: " + _dbPath);
                List<TiposSorteos> sorteosAlmacenados = conn.Table<TiposSorteos>().ToList();
                if (sorteosAlmacenados.Count == 0)
                {
                    List<string> nombresSorteos = new List<string> { "Tradicional", "La Segunda", "Revancha", "Siempre Sale" };
                    foreach (string nombreSorteo in nombresSorteos)
                    {
                        // Creas una nueva instancia de TiposSorteos
                        TiposSorteos nuevoSorteo = new TiposSorteos { nombre = nombreSorteo };
                        conn.Insert(nuevoSorteo);
                    }
                    return true;
                }
                else
                {
                    return true;
                }
            }catch (Exception ex)
            {
                Debug .WriteLine ("Error: " + ex.Message);
                return false;
            }
        }
        public int AgregarSorteoNuevo(Sorteos sorteo)
        {
            try
            {
                Init();
                bool sorteoExistente = conn.Table<Sorteos>()
                                    .Any(s => s.fecha == sorteo.fecha && s.numeroSorteo == sorteo.numeroSorteo);

                int idSorteoNuevo;
                if (!sorteoExistente) 
                {
                    conn.Insert (sorteo);
                    idSorteoNuevo = conn.ExecuteScalar<int>("SELECT last_insert_rowid()");
                    return idSorteoNuevo;
                }


                return 0;
            }catch (Exception ex) 
            {
                Debug.WriteLine("ERROR:  " +  ex.Message);
                return 0;
            }
        }
        public bool AgregarNumerosSorteados(NumerosSorteados numerosSorteados)
        {
            try
            {
                Init();
                bool numerosExisten = conn.Table<NumerosSorteados>()
                                    .Any(s => s.numSorteo == numerosSorteados.numSorteo && s.idTipoSorteo==numerosSorteados.idTipoSorteo);

                if (!numerosExisten)
                {
                    conn.Insert (numerosSorteados);
                }

                return true;
            }catch(Exception ex)
            {
                Debug.WriteLine("ERROR:  " + ex.Message);
                return false;
            }
        }
        public bool AgregarBoletaFija(BoletaFija boletaFija)
        {
            try
            {
                Init();
                conn.Insert(boletaFija);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR " + ex.Message);
                return false;
            }
            
        }
        public List<BoletaFija> ObtenerBoletasFijas()
        {
            
            try
            {
                Init();
                return conn.Table<BoletaFija>().ToList();                
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
            return new List<BoletaFija>();
        }
        public List<NumerosSorteados> ObtenerNumerosSorteados(int idSorteo, int idTipoSorteo)
        {
            try
            {
                Init();
                return conn.Table<NumerosSorteados>().Where(sorteo => sorteo.idSorteo == idSorteo && sorteo.idTipoSorteo == idTipoSorteo).ToList();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("error: " + ex.Message);
            }
            return new List<NumerosSorteados>();
        }
        public List<Sorteos> ObtenerListaSorteos()
        {
            try
            {
                Init();
                return conn.Table<Sorteos>().OrderByDescending(sorteo=> sorteo.fecha).ToList();

            }catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
            return new List<Sorteos>();
        }
    }

}
