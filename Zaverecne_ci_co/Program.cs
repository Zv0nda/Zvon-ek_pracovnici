using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlTypes;

namespace Zaverecne_ci_co
{
    class Osoba
    {
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string RodneCislo { get; set; }
        public string Bydliste { get; set; }
        public int PocetOdpracovanychHodin { get; set; }

        public Osoba()
        {
            this.Jmeno = Jmeno;
            this.Prijmeni = Prijmeni;
            this.Bydliste = Bydliste;
            this.PocetOdpracovanychHodin = PocetOdpracovanychHodin;
        }

    }

    class Zamestnanec : Osoba
    {
        public int PocetLetPraxe { get; set; }
        public int PocetDeti { get ; set; }

        public Zamestnanec()
        {
            this.Jmeno = Jmeno;
            this.Prijmeni = Prijmeni;
            this.Bydliste = Bydliste;
            this.PocetOdpracovanychHodin = PocetOdpracovanychHodin;
            this.PocetLetPraxe = PocetLetPraxe;
            this.PocetDeti = PocetDeti;
        }

        public double VypocetMzdy(double hodinovaSazba)
        {
            double hrubaMzda = PocetOdpracovanychHodin * hodinovaSazba;
            double cistaMzda = hrubaMzda - 0.3 * hrubaMzda + 0.02 * PocetDeti * hrubaMzda;
            return cistaMzda;
        }
    }

    class Brigadnik : Osoba
    {
        public double VypocetMzdy(double hodinovaSazba)
        {
            double hrubaMzda = PocetOdpracovanychHodin * hodinovaSazba;
            double cistaMzda = hrubaMzda - 0.15 * hrubaMzda;
            return cistaMzda;
        }
    }

    internal class Program
    {
        public static List<Osoba> evidence = new List<Osoba>();

        public static void Mzdy()
        {
            Console.WriteLine("Zadejte hodinovou sazbu: ");
            double hodinovaSazba = double.Parse(Console.ReadLine());

            evidence.OrderBy(v => v.Prijmeni);
            foreach(Osoba pracovnik in evidence)
            {
                if(pracovnik is Zamestnanec)
                {
                    Zamestnanec zamestnanec = (Zamestnanec)pracovnik;
                    double castkaStravenky = zamestnanec.PocetOdpracovanychHodin / 8 * 100;
                    Console.WriteLine($"{zamestnanec.Jmeno} {zamestnanec.Prijmeni}: {zamestnanec.VypocetMzdy(hodinovaSazba)} {castkaStravenky}");
                }
                else if (pracovnik is Brigadnik)
                {
                    Brigadnik brigadnik = (Brigadnik)pracovnik;
                    double cistaMzda = brigadnik.VypocetMzdy(hodinovaSazba);
                    Console.WriteLine($"{brigadnik.Jmeno} {brigadnik.Prijmeni}: {cistaMzda}");
                }

            }

            Console.ReadKey();
            Console.Clear();
        }

        public static void PridejZamestnance() 
        {
            Console.WriteLine("Počet zaměstnanců: ");
            int pocetZamestnancu = int.Parse(Console.ReadLine());
            for (int i = 0; i < pocetZamestnancu; i++)
            {
                Zamestnanec zamestnanec = new Zamestnanec();

                Console.WriteLine("Zadejte jméno zaměstnance: ");
                zamestnanec.Jmeno = Console.ReadLine();

                Console.WriteLine("Zadejte příjmení zaměstnance: ");
                zamestnanec.Prijmeni = Console.ReadLine();

                Console.WriteLine("Zadejte rodné číslo zaměstnance: ");
                zamestnanec.RodneCislo = Console.ReadLine();

                Console.WriteLine("Zadejte bydliště zaměstnance: ");
                zamestnanec.Bydliste = Console.ReadLine();

                Console.WriteLine("Zadejte počet odpracovaných hodin zaměstnance: ");
                zamestnanec.PocetOdpracovanychHodin = int.Parse(Console.ReadLine());

                Console.WriteLine("Zadejte počet let praxe zaměstnance: ");
                zamestnanec.PocetLetPraxe = int.Parse(Console.ReadLine());

                Console.WriteLine("Zadejte počet dětí zaměstnance: ");
                zamestnanec.PocetDeti = int.Parse(Console.ReadLine());

                evidence.Add(zamestnanec);
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void PridejBriganiky()
        {
            Console.WriteLine("Zadejte počet brigádníků: ");
            int pocetBrigadniku = int.Parse(Console.ReadLine());
            for (int i = 0; i < pocetBrigadniku; i++)
            {
                Brigadnik brigadnik = new Brigadnik();

                Console.WriteLine("Zadejte jméno brigádníka:");
                brigadnik.Jmeno = Console.ReadLine();

                Console.WriteLine("Zadejte příjmení brigádníka:");
                brigadnik.Prijmeni = Console.ReadLine();

                Console.WriteLine("Zadejte rodné číslo brigádníka:");
                brigadnik.RodneCislo = Console.ReadLine();

                Console.WriteLine("Zadejte bydliště brigádníka:");
                brigadnik.Bydliste = Console.ReadLine();

                Console.WriteLine("Zadejte počet odpracovaných hodin brigádníka:");
                brigadnik.PocetOdpracovanychHodin = int.Parse(Console.ReadLine());

                evidence.Add(brigadnik);

                Console.ReadKey();
                Console.Clear();
            }

            

        }

        public static void VypisZamestnance()
        {
            foreach(Osoba osoba in evidence)
            {
                if(osoba is Zamestnanec)
                {
                    Console.WriteLine($"{osoba.Jmeno} {osoba.Prijmeni}");
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        public static void VypisBrigadniky()
        {
            foreach (Osoba osoba in evidence)
            {
                if (osoba is Brigadnik)
                {
                    Console.WriteLine($"{osoba.Jmeno} {osoba.Prijmeni}");
                }
            }
            Console.ReadKey();
            Console.Clear();
        }
        static void Main(string[] args)
        {
            int choiceMenu = 0;
            Console.Clear();
            do
            {
                Console.WriteLine("Hlavní menu");
                Console.WriteLine("1. Přidat zaměstnance");
                Console.WriteLine("2. Pridat brigádníka/y");
                Console.WriteLine("3. Konec programu");
                Console.WriteLine("4. Vypsat zaměstnance");
                Console.WriteLine("5. Vypsat brigádníky");
                Console.WriteLine("6. Všichni zaměstnanci");

                Console.Write("Volba: ");
                choiceMenu = int.Parse(Console.ReadLine());

                Console.Clear();

                switch (choiceMenu)
                {
                    case 1:
                        Console.Clear();
                        PridejZamestnance();
                        break;
                    case 2:
                        PridejBriganiky();
                        break;
                    case 4:
                        VypisZamestnance();
                        break;
                    case 5:
                        VypisBrigadniky();
                        break;
                    case 6:
                        Mzdy();
                        break;
                }

            } while (choiceMenu != 3);

        }
    }

}
