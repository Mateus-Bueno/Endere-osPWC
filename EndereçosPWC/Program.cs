using System;
using EndereçosPWC.Services;


class Program
{
    public static void Main (string[] args)
    {

        Console.WriteLine("Bem vindo ao sistema!");
        Console.WriteLine("selecione uma opção:\n");
        Console.WriteLine("1 - Salvar um endereço         2 - Verificar endereços salvos");
        Console.WriteLine("3 - Encerrar");
        string option = Console.ReadLine();
        bool menu = true;
        while (menu)
        {
            switch(option)
            {
                case "1":
                    Console.WriteLine("Digite o endereço que deseja salvar:");
                    string adress = Console.ReadLine();
                    string[] substrings = Enderecos.SepararStrings(adress);
                    string number = Enderecos.BuscarNumero(substrings);
                    string street = Enderecos.RemoverNumero(number, substrings);

                    Console.WriteLine($"O logradouro na rua {street} e número {number} foi salvo");
                    Console.ReadLine();
                    break;

                case "2":
                    break;

                case "3":
                    Console.WriteLine("Até a próxima!");
                    Console.ReadKey();
                    menu = false;
                    break;
            }
        }
    }
}