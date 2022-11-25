using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Scrieti un program C# care sa functioneze ca un viewer hexazecimal pentru continutul unui fisier.
 
// Programul va primi ca input numele unui fisier de pe disc si va afisa octeti fisierului in format hexazecimal
// (fiecare octet = 2 cifre hexazecimale).
// Numarul de octeti din output afisat pe o linie va fi un parametru al afisarii. 

// Prima coloana este un index numeric (in hexa). 
// Pe fiecare linie sunt afisati cate 16 octeti din fisier prin 32 de cifre hexa. 
// Pe ultima coloana este afisat si continutul fisierului sub forma de text (pentru caracterele neafisabile din fisier se foloseste caracterul punct). 

namespace ASC_Hex_Viewer
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = ReadPath();

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
                Console.WriteLine("Introduceti numele fisierului dorit: ");

                path = Console.ReadLine();
            }

            return path;
        }

        /// <summary> Afiseaza continutul unui fisier text in format hexazecimal </summary>
        /// <param name="path"> Numele fisierului care va fi citit </param>
        static void HexViewer(string path)
        {
            // Creeaza un StreamReader pentru a citi din fisier, se inchide la finalul directivei using
            using (var reader = new StreamReader(path))
            {
                // Citeste cate 16 octeti pana la finalul fisierului
                int charactersRead = 0;
                char[] buffer = {};
                do
                {
                    reader.Read(buffer, 0, 16);
                    charactersRead += buffer.Length;

                    Console.WriteLine("{0:X}", charactersRead)

                } while (buffer != null);

                


            }
        }
    }
}