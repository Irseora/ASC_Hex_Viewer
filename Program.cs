using System;
using System.IO;

namespace ASC_Hex_Viewer
{
    class Program
    {
        // Numarul de caractere pe care le afiseaza programul pe o linie
        static int numberOfCharsToRead = 16;

        static void Main(string[] args)
        {
            // Citeste numele unui fisier
            Console.Clear();
            Console.WriteLine("Hex Viewer: Programul va afisa continutul unui fisier in format hexazecimal.");
            string path = ReadPath();
            Console.WriteLine("-------------------------------------------------------------------------------");

            try
            {
                HexViewer(path);
            }
            // Daca fisierul nu poate fi citit, afiseaza mesajul exceptiei
            catch (IOException e)
            {
                Console.WriteLine("Fisierul nu poate fi citit:");
                Console.WriteLine(e.Message);
            }

            // Programul se incheie dupa ce se apasa o tasta
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Apasati orice tasta pentru a incheia programul...");
            Console.ReadKey();
        }

        /// <summary> Citeste numele unui fisier </summary>
        /// <returns> Numele fisierului citit </returns>
        static string ReadPath()
        {
            Console.Write("Introduceti numele fisierului dorit: ");
            string path = Console.ReadLine();

            // Citeste pana primeste un fisier care exista
            while (!File.Exists(path))
            {
                Console.WriteLine("Fisierul introdus nu exista!");
                Console.Write("Introduceti numele fisierului dorit: ");

                path = Console.ReadLine();
            }

            return path;
        }

        /// <summary> Afiseaza continutul unui fisier in format hexazecimal </summary>
        /// <param name="path"> Numele fisierului care va fi citit </param>
        static void HexViewer(string path)
        {
            // Creeaza un StreamReader pentru a citi din fisier, se inchide la finalul directivei using
            using (var reader = new StreamReader(path))
            {
                // Citeste cate numberOfCharsToRead octeti pana la finalul fisierului
                int charactersReadTotal = 0, charactersReadCurrent = -1;
                char[] buffer = new char[numberOfCharsToRead];

                while (charactersReadCurrent != 0)
                {
                    charactersReadCurrent = reader.Read(buffer, 0, numberOfCharsToRead);

                    // Indexul la care se afla primul octet din bufferul curent
                    Console.Write("{0:X8}: ", charactersReadTotal);

                    // Afiseaza sirul de caractere citit sub forma unor cifre hexazecimale
                    Afis("hexa", buffer, charactersReadCurrent);

                    // Afiseaza sirul de caractere citit sub forma de text
                    Afis("text", buffer, charactersReadCurrent);                    

                    Console.WriteLine();
                    charactersReadTotal += charactersReadCurrent;
                }
            }
        }

        /// <summary> Afiseaza continutul unui sir de caractere in formatul dat </summary>
        /// <param name="format"> Formatul in care se va afisa sirul de caractere </param>
        /// <param name="buffer"> Sirul de caractere care va fi afisat </param>
        /// <param name="charactersReadCurrent"> Numarul de caractere care trebuie afisate </param>
        static void Afis(string format, char[] buffer, int charactersReadCurrent)
        {
            // Afiseaza continutul sirului de caractere
            int i;
            for (i = 0; i < charactersReadCurrent; i++)
            {
                // In format hexazecimal
                if (format == "hexa")
                {
                    Console.Write("{0:X2} ", (int)buffer[i]);

                    // Imparte in 2 jumatati sirul de cifre hexazecimale
                    if (i == 7)
                        Console.Write("| ");
                }
                // In format text
                else if (format == "text")
                {
                    // Afiseaza caracterul '|' pentru a delimita cifrele hexazecimale de text
                    if (i == 0)
                        Console.Write(" | ");

                    // Daca caracterul se poate afisa, il afiseaza (litera, cifra, semn de punctuatie, spatiu)
                    if (char.IsLetterOrDigit(buffer[i]) || char.IsPunctuation(buffer[i]) || buffer[i] == ' ')
                        Console.Write(buffer[i]);
                    // Daca caracterul nu se poate afisa, afiseaza caracterul '.'
                    else
                        Console.Write('.');
                }
            }

            // Daca nu s-au citit numberOfCharsToRead caractere
            for (int j = i; j < numberOfCharsToRead; j++)
            {
                // Completeaza cu 0-uri coloana cifrelor hexazecimale
                if (format == "hexa")
                {
                    Console.Write("00 ");

                    // Imparte in 2 jumatati sirul de cifre hexazecimale
                    if (j == 7) Console.Write("| ");
                }
                // Completeaza cu puncte coloana textului
                else if (format == "text")
                {
                    // Afiseaza caracterul '|' pentru a delimita cifrele hexazecimale de text
                    if (j == 0) Console.Write(" | ");

                    Console.Write('.');
                }
            }
        }
    }
}