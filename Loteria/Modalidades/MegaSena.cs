namespace Loteria.Modalidades
{
    public class MegaSena : Loteria
    {
        public override int NumeroMinimo => 1;
        public override int NumeroMaximo => 60;
        public override int QuantidadeNumerosApostaMinimo => 6;
        public override int QuantidadeNumerosApostaMaximo => 15;
        public override int QuantidadeMinimaAcertosPremio => 4;
    }
}
