using Autofac;
using Loteria.Contratos;
using Loteria.Modalidades;
using System;
using System.Linq;

namespace Loteria
{
    class Program
    {
        static ILoteria loteria;

        /// <summary>
        /// Registra a modalidade da loteria via injecao dependencia
        /// </summary>
        /// <returns></returns>
        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MegaSena>().As<ILoteria>();
            //builder.RegisterType<Quina>().As<ILoteria>();
            //builder.RegisterType<LotoFacil>().As<ILoteria>();
            return builder.Build();
        }

        static void Main(string[] args)
        {
            // Obtem a instancia do contrato
            var container = BuildContainer();
            loteria = container.Resolve<ILoteria>();

            while (true)
            {
                var opcao = Menu();

                switch (opcao)
                {
                    case 1:
                        MenuCadastrarJogo();
                        break;
                    case 2:
                        MenuCadastrarJogoSurpresinha();
                        break;
                    case 3:
                        MenuCadastrarJogoSurpresinhaQuantidade();
                        break;
                    case 4:
                        MenuListarJogos();
                        break;
                    case 5:
                        MenuSortear();
                        break;
                    case 6:
                        MenuExibirResultado();
                        break;
                    case 7:
                        Console.WriteLine();
                        Console.WriteLine("Fim");
                        Console.WriteLine();
                        Console.ReadKey();
                        return;
                    default:
                        Menu();
                        break;
                }
            }
        }

        private static int Menu()
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Aplicativo Loteria - {loteria.GetType().Name}");
            Console.WriteLine();
            Console.WriteLine("[1] Cadastrar jogo");
            Console.WriteLine("[2] Cadastrar jogo (surpresinha)");
            Console.WriteLine("[3] Cadastrar multiplos jogos (surpresinha)");
            Console.WriteLine("[4] Listar jogos cadastrados");
            Console.WriteLine("[5] Sortear");
            Console.WriteLine("[6] Exibir resultado");
            Console.WriteLine("[7] Sair");
            Console.WriteLine();
            Console.Write("Informe a opcao desejada: ");

            var opcao = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(opcao) || !int.TryParse(opcao, out _))
            {
                Console.WriteLine();
                Console.WriteLine("Opção inválida");
            }

            return int.TryParse(opcao, out _) ? int.Parse(opcao) : 0;
        }

        private static void MenuCadastrarJogo()
        {
            Console.WriteLine();
            Console.Write("Digite os numeros do jogo separados por (,): ");
            var numeros = Console.ReadLine().Split(',').Select(int.Parse).OrderBy(x => x).ToList();
            var jogo = new Aposta { Numeros = numeros };

            try
            {
                var aposta = loteria.Apostar(jogo);
                Console.WriteLine(aposta);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private static void MenuCadastrarJogoSurpresinha()
        {
            Console.Write($"Digite a quantidade de numeros desejada [{loteria.QuantidadeNumerosApostaMinimo}]: ");

            var qtde = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(qtde)) qtde = loteria.QuantidadeNumerosApostaMinimo.ToString();

            try
            {
                var aposta = loteria.ApostarSurpresinha(int.Parse(qtde));
                Console.WriteLine(aposta);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private static void MenuCadastrarJogoSurpresinhaQuantidade()
        {
            Console.Write($"Digite a quantidade de numeros desejada [{loteria.QuantidadeNumerosApostaMinimo}]: ");
            var qtde = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(qtde)) qtde = loteria.QuantidadeNumerosApostaMinimo.ToString();

            Console.Write($"Digite a quantidade de jogos desejada [100]: ");
            var qtdeJogos = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(qtdeJogos)) qtdeJogos = "100";

            try
            {
                for (int i = 0; i < int.Parse(qtdeJogos); i++)
                    loteria.ApostarSurpresinha(int.Parse(qtde));

                MenuListarJogos();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private static void MenuListarJogos()
        {
            try
            {
                var str = loteria.Listar();
                Console.WriteLine();
                Console.WriteLine("Jogos cadastrados: ");
                Console.Write(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private static void MenuSortear()
        {
            try
            {
                loteria.Sortear();

                Console.WriteLine();
                Console.WriteLine($"Numeros sorteados: {String.Join(" - ", loteria.Sorteio.Numeros)}");
                Console.WriteLine();
                Console.WriteLine("Resultado:");
                Console.Write(loteria.ObterResultado());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private static void MenuExibirResultado()
        {
            try
            {
                var str = loteria.ObterResultado();
                Console.WriteLine();
                Console.WriteLine("Resultado:");
                Console.Write(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}
