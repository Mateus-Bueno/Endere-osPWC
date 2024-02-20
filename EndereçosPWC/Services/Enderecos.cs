using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using EndereçosPWC.Exceptions;

namespace EndereçosPWC.Services
{
    public static class Enderecos
    {
        private static readonly string arquivo = "ArquivoEnderecos.txt";

        // Remove as vírgulas e separa o endereço passado por subgrupos de palavras
        public static string[] SepararStrings(string adress)
        {
            TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
            adress = adress.Replace(",", "");
            adress = textInfo.ToTitleCase(adress);

            string[] substrings = adress.Split(" ");
            if (substrings.Length < 2)
            {
                throw new UsoInvalidoException();
            }

            bool haveNumber = false;
            foreach (string ss in substrings)
            {
                haveNumber = Regex.IsMatch(ss, @"[0-9]+");

                if (haveNumber)
                {
                    break;
                }
            }
            
            if (haveNumber)
            {
                return substrings;
            }
            else
            {
                throw new NenhumNumeroEncontradoException();
            }
            
        }

        // Busca pelo número do endereço entre as sustrings passadas
        public static string BuscarNumero(string[] substrings)
        {
            string number = "";

            // Define os padrões que serão usados para analisar cada string com Regex
            string[] numberPatterns = {@"\b[0-9]+\b", @"\b[0-9]+[a-zA-Z]{1}\b"};
            string[] abreviattionPatterns = {@"\bn\b", @"\bno\b", @"\bN\b", @"\bNo\b", @"\bNO\b"};
            bool numberValidate = false;
            int ssCount = substrings.Count() - 1;
            int count = 0;
            

            // Faz uma verificação alternada entre o final e início do endereço passado até encontrar o número
            do
            {
                foreach (string nPattern in numberPatterns)
                {
                    if (Regex.IsMatch(substrings[ssCount], nPattern))
                    {
                        // Busca por N ou No antes do número
                        foreach (string pattern in abreviattionPatterns)
                        {
                            if (Regex.IsMatch(substrings[ssCount - 1], pattern))
                            {
                                number += substrings[ssCount - 1] + " ";
                                break;
                            }
                        }
                        
                        // Adiciona o número encontrado à string "number"
                        number += substrings[ssCount];

                        // Busca por letras ou blocos após o número
                        if (count != 0 && Regex.IsMatch(substrings[ssCount + 1], @"\b[a-zA-Z]{1}\b"))
                        {
                            number += " " + substrings[ssCount + 1];
                        }

                        numberValidate = true;
                        break;
                    }
                    else if (Regex.IsMatch(substrings[count], nPattern))
                    {
                        foreach (string pattern in abreviattionPatterns)
                        {
                            if (count != 0 && Regex.IsMatch(substrings[count - 1], pattern))
                            {
                                number += substrings[count - 1] + " ";
                            }
                        }

                        number += substrings[count];

                        if (Regex.IsMatch(substrings[count + 1], @"\b[a-zA-Z]{1}\b"))
                        {
                            number += " " + substrings[count + 1];
                        }

                        numberValidate = true;
                        break;
                    }
                }

                ssCount--;
                count++;
            }
            while (!numberValidate);

            return number;
        }

        // Remove o número encontrado do endereço, sobrando apenas a rua/avenida/..
        public static string RemoverNumero(string number, string[] substrings)
        {
            List<string> noNumberSubstrings = new List<string>();

            for (int i = 0, n = substrings.Length; i < n; i++)
            {
                if (!number.Contains(substrings[i]))
                {
                    noNumberSubstrings.Add(substrings[i]);
                }
            }

            string street = string.Join(" ", noNumberSubstrings);
            
            return street;
        }

        public static void ArquivarEndereco(string street, string number)
        {
            string text = $"\"{street}\", \"{number}\"";
            bool added;

            // Verifica se o mesmo endereço já foi passado anteriormente
            using (StreamReader sr = new StreamReader(arquivo))
            {
                added = sr.ReadToEnd().ToLower().Contains(text.ToLower());
            }

            if (added) 
            {
                throw new EnderecoJaFoiSalvoException();
            }
            else
            {
                // Adiciona ao arquivo e ordena alfabeticamente
                List<string> enderecosSalvos = File.ReadAllLines(arquivo).ToList();
                enderecosSalvos.Add(text);
                enderecosSalvos.Sort();
                File.WriteAllLines(arquivo, enderecosSalvos);
            }
        }

        public static void VisualizarArquivo()
        {
            FileInfo infoArquivo = new FileInfo(arquivo);

            if (infoArquivo.Length == 0)
            {
                Console.WriteLine("Nao existem dados salvos!");
                return;
            }
            else
            {
                using (StreamReader sr = new StreamReader(arquivo))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
        }
    }
}