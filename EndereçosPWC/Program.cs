using System;
using EndereçosPWC.Exceptions;
using EndereçosPWC.Services;


class Program
{
    public static void Main (string[] args)
    {

        Console.WriteLine("Bem vindo ao sistema!");
        Console.ReadLine();
        bool menu = true;
        while (menu)
        {
            try
            {
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

                        Console.WriteLine($"O endereço {street}, número {number} foi salvo");
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

            catch(EnderecoJaFoiSalvoException)
            {
                Console.WriteLine("Este endereço já está em nossos arquivos");
                Console.WriteLine("No menu, digite 2 caso queira verificar os endereços salvos.");
                Console.ReadLine();
            }
        }
            
    }
}