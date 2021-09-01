using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();

        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSeries();
                        break;
                    case "3":
                        AtualizarSeries();
                        break;
                    case "4":
                        ExcluirSeries();
                        break;
                    case "5":
                        VisualizarSeries();
                        break;
                    case "C":
                        //Console.Clear();
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("\nObrigador por utilizar nossos serviços.");
            Console.WriteLine("");
        }

        private static void ListarSeries()
        {
            Console.WriteLine("\nListar séries \n");

            var lista = repositorio.Listar();

            if (lista.Count == 0)
            {
                Console.WriteLine("\nNenhuma série cadastrada \n");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                
                Console.WriteLine("#ID {0} - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "**EXCLUÍDO**" : ""));
            }
        }

        private static void InserirSeries()
        {
            Console.WriteLine("\nInserir nova série \n");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.WriteLine("\nCom base na classificação acima, digite o gênero da série: \n");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("\nDigite o título da série: \n");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("\nDigite o ano de início da série: \n");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("\nDescreva brevemente a série: \n");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        descricao: entradaDescricao,
                                        ano: entradaAno);
            
            repositorio.Inserir(novaSerie);
        }

        private static void AtualizarSeries()
        {
            Console.WriteLine("Digite o ID da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.WriteLine("Digite o gênero dentre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o ano de início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Descreva brevemente a série: ");
            string entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: indiceSerie,
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao);

            repositorio.Atualizar(indiceSerie, atualizaSerie);
        }

        private static void ExcluirSeries()
        {
            Console.WriteLine("\nDigite o ID da série: \n");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Excluir(indiceSerie);
        }

        private static void VisualizarSeries()
        {
            Console.WriteLine("\nDigite o ID da série: \n");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornarPorId(indiceSerie);

            Console.WriteLine(serie);
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine("");
            Console.WriteLine("DIO Séries - o seu repositório de séries \n");
            Console.WriteLine("Informe a opção desejada: \n");
            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine("");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine("");
            return opcaoUsuario;
        }
    }
}
