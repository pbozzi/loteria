namespace Loteria.Modalidades
{
    public class Quina : Loteria
    {
        public override int NumeroMinimo => 1;
        public override int NumeroMaximo => 80;
        public override int QuantidadeNumerosApostaMinimo => 5;
        public override int QuantidadeNumerosApostaMaximo => 15;
        public override int QuantidadeMinimaAcertosPremio => 4;
    }
}
