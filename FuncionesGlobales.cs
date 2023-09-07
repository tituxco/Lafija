using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaFija
{
    public class FuncionesGlobales
    {
        public string CompletarCeros(int numero,int cantidadCaracteresTotales)
        {

            string cadenaConCeros = numero.ToString().PadLeft(cantidadCaracteresTotales, '0');
            return cadenaConCeros;
        }
    }
}
