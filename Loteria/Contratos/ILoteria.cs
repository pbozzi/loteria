using Loteria.Modalidades;
using System.Collections.Generic;

namespace Loteria.Contratos
{
    public interface ILoteria
    {
        /// <summary>
        /// menor numero que pode conter a aposta
        /// </summary>
        int NumeroMinimo { get; }

        /// <summary>
        /// Maior numero que pode conter a aposta
        /// </summary>
        int NumeroMaximo { get; }

        /// <summary>
        /// Quantidade minima de numeros que uma aposta pode conter
        /// </summary>
        int QuantidadeNumerosApostaMinimo { get; }

        /// <summary>
        /// Quantidade maxima de numeros que uma aposta pode conter
        /// </summary>
        int QuantidadeNumerosApostaMaximo { get; }

        /// <summary>
        /// Quantidade minima de acertos para receber a premiacao
        /// </summary>
        int QuantidadeMinimaAcertosPremio { get; }

        /// <summary>
        /// Apostas realizadas
        /// </summary>
        List<Aposta> Jogos { get; set; }

        /// <summary>
        /// Jogo sorteado
        /// </summary>
        Aposta Sorteio { get; set; }

        /// <summary>
        /// Realiza a aposta de um jogo
        /// </summary>
        /// <param name="jogo">Jogo contendo os numeros da aposta</param>
        /// <returns></returns>
        Aposta Apostar(Aposta jogo);

        /// <summary>
        /// Realiza a aposta de um jogo aleatorio (surpresinha)
        /// </summary>
        /// <param name="qtdeNumeros">Quantidade de numeros desejados na aposta</param>
        /// <returns></returns>
        Aposta ApostarSurpresinha(int qtdeNumeros);

        /// <summary>
        /// Simula o sorteio de um jogo
        /// </summary>
        /// <returns></returns>
        Aposta Sortear();

        /// <summary>
        /// Lista as apostas realizadas
        /// </summary>
        /// <returns></returns>
        string Listar();

        /// <summary>
        /// Obtem o resultado das apostas com o jogo sorteado
        /// </summary>
        /// <returns></returns>
        string ObterResultado();
    }
}
