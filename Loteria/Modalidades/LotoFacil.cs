namespace Loteria.Modalidades
{
    public class LotoFacil : Loteria
    {
        public override int NumeroMinimo => 1;
        public override int NumeroMaximo => 25;
        public override int QuantidadeNumerosApostaMinimo => 15;
        public override int QuantidadeNumerosApostaMaximo => 18;
        public override int QuantidadeMinimaAcertosPremio => 11;
    }
}
