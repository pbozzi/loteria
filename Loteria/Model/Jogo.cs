using System;
using System.Collections.Generic;

namespace Loteria.Modalidades
{
    public class Aposta
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public List<int> Numeros { get; set; }

        public override string ToString()
        {
            return $"Aposta: {Id} | Data: {Data.ToString("dd/MM/yyyy hh:mm:ss")} | Numeros: {String.Join(" - ", Numeros)}";
        }
    }
}
