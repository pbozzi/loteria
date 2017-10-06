using Loteria.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loteria.Modalidades
{
    public abstract class Loteria : ILoteria
    {
        public abstract int NumeroMinimo { get; }
        public abstract int NumeroMaximo { get; }
        public abstract int QuantidadeNumerosApostaMinimo { get; }
        public abstract int QuantidadeNumerosApostaMaximo { get; }
        public abstract int QuantidadeMinimaAcertosPremio { get; }

        private static readonly Random random = new Random();

        public List<Aposta> Jogos { get; set; }
        public Aposta Sorteio { get; set; }

        public Loteria()
        {
            Jogos = new List<Aposta>();
        }

        public Aposta Apostar(Aposta aposta)
        {
            if (Sorteio != null)
                throw new ArgumentNullException("Nao foi dessa vez. Reinicie o aplicativo para realizar apostas.");
            if (aposta == null)
                throw new ArgumentNullException(nameof(aposta));
            if (aposta.Numeros.Any(x => x < NumeroMinimo || x > NumeroMaximo))
                throw new ArgumentOutOfRangeException($"Aposta Invalida. Informar somente numeros entre {NumeroMinimo} e {NumeroMaximo}.", nameof(aposta));
            if (aposta.Numeros.Count() < QuantidadeNumerosApostaMinimo || aposta.Numeros.Count() > QuantidadeNumerosApostaMaximo)
                throw new ArgumentOutOfRangeException($"Aposta Invalida. A posta deve conter entre {QuantidadeNumerosApostaMinimo} e {QuantidadeNumerosApostaMaximo} numeros.", nameof(aposta));
            if (aposta.Numeros.GroupBy(x=>x).Where(y=>y.Count() > 1).Any())
                throw new ArgumentNullException("Aposta invalida. A aposta nao deve possuir numeros repetidos.", nameof(aposta));

            aposta.Id = Jogos.Count + 1;
            aposta.Data = DateTime.Now;
            Jogos.Add(aposta);

            return aposta;
        }

        public Aposta ApostarSurpresinha(int qtdeNumeros)
        {
            if (Sorteio != null)
                throw new ArgumentNullException("Nao foi dessa vez. Reinicie o aplicativo para realizar apostas.");
            if (qtdeNumeros < QuantidadeNumerosApostaMinimo || qtdeNumeros > QuantidadeNumerosApostaMaximo)
                throw new ArgumentException($"A quantidade de numeros da aposta deve ser entre {QuantidadeNumerosApostaMinimo} e {QuantidadeNumerosApostaMaximo}.", nameof(qtdeNumeros));

            var aposta = new Aposta { Numeros = Enumerable.Range(NumeroMinimo, NumeroMaximo).OrderBy(x => random.Next()).Take(qtdeNumeros).OrderBy(x => x).ToList() };
            aposta.Id = Jogos.Count + 1;
            aposta.Data = DateTime.Now;
            Jogos.Add(aposta);

            return aposta;
        }

        public Aposta Sortear()
        {
            if (Sorteio != null)
                throw new ArgumentNullException("Os numeros ja foram sorteados!");
            if (!Jogos.Any())
                throw new ArgumentNullException("Nao existem jogos cadastrados");

            Sorteio = new Aposta { Numeros = Enumerable.Range(NumeroMinimo, NumeroMaximo).OrderBy(x => random.Next()).Take(QuantidadeNumerosApostaMinimo).OrderBy(x => x).ToList() };

            return Sorteio;
        }

        public string Listar()
        {
            if (!Jogos.Any())
                throw new ArgumentException("Nao existem jogos cadastrados");

            var sb = new StringBuilder();
            Jogos.ForEach(x => sb.Append($"{x}\r\n"));

            return sb.ToString();
        }

        public string ObterResultado()
        {
            if (Sorteio == null)
                throw new ArgumentException("Nao houve sorteio. Ainda da tempo para realizar mais apostas!");

            var acertos = new List<Tuple<int, int>>();
            Jogos.ForEach(x => acertos.Add(new Tuple<int, int>(x.Id, x.Numeros.Intersect(Sorteio.Numeros).Count())));
            acertos.RemoveAll(x => x.Item2 < QuantidadeMinimaAcertosPremio);

            var sb = new StringBuilder();
            if (acertos.Count() > 0)
                acertos.ForEach(x => sb.Append($"Jogo {x.Item1}: {x.Item2} acertos\r\n"));
            else
                sb.Append("\r\nNenhuma aposta ganhadora");

            return sb.ToString();
        }
    }
}
