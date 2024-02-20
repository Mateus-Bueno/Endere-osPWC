using System;
using EndereçosPWC.Exceptions;
using EndereçosPWC.Services;

class Program
{
    public static void Main ()
    {
        // Inicializa o menu e o repete até o usuário encerrar
        bool menu = true;
        while (menu)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Bem vindo ao sistema!\n");
                Console.WriteLine("selecione uma opção:");
                Console.WriteLine("1 - Salvar um endereço         2 - Verificar endereços salvos");
                Console.WriteLine("3 - Encerrar");
                string option = Console.ReadLine();

                switch(option)
                {
                    case "1":
                        Console.WriteLine("Digite o endereço que deseja salvar:");
                        string adress = Console.ReadLine();
                        string[] substrings = Enderecos.SepararStrings(adress);
                        string number = Enderecos.BuscarNumero(substrings);
                        string street = Enderecos.RemoverNumero(number, substrings);
                        Enderecos.ArquivarEndereco(street, number);

                        Console.WriteLine($"O endereço \"{street}\", número \"{number}\" foi salvo");
                        Console.ReadKey();
                        break;

                    case "2":
                        Enderecos.VisualizarArquivo();
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.WriteLine("Até a próxima!");
                        Console.ReadKey();
                        menu = false;
                        break;
                    
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }

            catch (UsoInvalidoException)
            {
                Console.WriteLine("O endereço não pode ser formado por menos de duas palavras!");
                Console.ReadKey();
            }

            catch (NenhumNumeroEncontradoException)
            {
                Console.WriteLine("O endereço deve conter pelo menos um número escrito em algarismos.");
                Console.ReadKey();
            }

            catch (EnderecoJaFoiSalvoException)
            {
                Console.WriteLine("Este endereço já está em nossos arquivos");
                Console.WriteLine("No menu, digite 2 caso queira verificar os endereços salvos.");
                Console.ReadKey();
            }
        }
            
    }
}