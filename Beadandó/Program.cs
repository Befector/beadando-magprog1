using System;
using System.Collections.Generic;
using static Beadandó.Classes;

namespace Beadandó
{
    internal class Program
    {
        public static List<A> Lista = new List<A>();
        public static List<Meres> meres = new List<Meres>();

        static void alg(ushort[] meresek)
        {
            Meres[] _m = new Meres[meresek.Length];
            _m[0] = new Meres(Meres.Tipus.Szf, meresek[0], 0);
            _m[_m.Length - 1] = new Meres(Meres.Tipus.Szf, meresek[meresek.Length - 1], (ushort)(meresek.Length - 1));

            for (ushort i = 1; i < _m.Length - 1; i++)
            {
                if (meresek[i] == 0)
                {
                    _m[i] = new Meres(Meres.Tipus.T, 0, i);
                }
                else
                {
                    _m[i] = new Meres(Meres.Tipus.Szf, meresek[i], i);
                }
            }

            Lista.Add(new A(_m[0]._tipus, new List<Meres> { _m[0] }));
            for (int i = 1; i < _m.Length; i++)
            {
                if (_m[i - 1]._tipus == _m[i]._tipus)
                {
                    Lista[Lista.Count - 1].Meresek.Add(_m[i]);
                }
                else
                {
                    Lista.Add(new A(_m[i]._tipus, new List<Meres>() { _m[i] }));
                }
            }

            int _x = 0;
            foreach (A x in Lista)
            {
                if (_x > 0 && _x < Lista.Count - 1 && x._tipus == Meres.Tipus.Szf && Lista[_x - 1]._tipus == Meres.Tipus.T && Lista[_x + 1]._tipus == Meres.Tipus.T)
                {       
                    x._tipus = Meres.Tipus.Sz;
                    foreach (Meres m in x.Meresek)
                    {
                        m._tipus = Meres.Tipus.Sz;
                    }
                }
                _x++;
            }

            foreach (Meres m in _m)
            {
                meres.Add(m);
            }
        }
        static void Avg(A[] _a, ushort maxHeight = 100)
        {
            List<A> Szigetek = new List<A>();

            foreach (A a in Lista)
            {
                if (a._tipus != Meres.Tipus.Sz)
                    continue;
                if (a.MaxHeight > maxHeight)
                    continue;
                Szigetek.Add(a);
            }
            Console.WriteLine("100 méter alatti magasságú szigetek száma: " + Szigetek.Count);

            ushort ind = 1;

            foreach (A a in Szigetek)
            {
                ushort sum = 0;
                foreach (Meres m in a.Meresek)
                {
                    sum += m.Magassag;
                }
                double avg = (sum / 1.0) / a.Meresek.Count;
                Console.WriteLine($"{ind}. sziget átlag magassága: {avg:0.0##}");
                ind++;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("##### Beadandó Vizsgadolgozat - Magas Szintű Programozási nyelvek I. #####\n");
            Random rgen = new Random();
            ushort[] mag = new ushort[0];
            ushort maxmag = 0;

            while (true)
            {
                Console.Write("Kérem a mérések számát (5-1000): ");
                string input = Console.ReadLine();
                if (ushort.TryParse(input, out ushort N))
                {
                    if (N < 5 || N > 1000)
                        continue;
                    else
                    {
                        while (true)
                        {
                            Console.WriteLine("\n### TIPP: Minél alacsonyabb a maximális magasság, annál nagyobb eséllyel fordulnak elő szigetek. ### ");
                            Console.Write("Kérlek add meg a maximális magasságot (50-9000): ");
                            if (ushort.TryParse(Console.ReadLine(), out maxmag)) {
                                if (maxmag < 50 || maxmag > 9000) continue;
                                break;
                            }
                            else
                                continue;
                        }

                        mag = new ushort[N];
                        for (ushort i = 0; i < N; i++)
                        {
                            mag[i] = (ushort)rgen.Next(0, maxmag);
                        }

                        while (mag[0] == 0 || mag[N - 1] == 0)
                        {
                            if (mag[0] == 0)
                                mag[0] = (ushort)rgen.Next(0, maxmag);
                            if (mag[N - 1] == 0)
                                mag[N - 1] = (ushort)rgen.Next(0, maxmag);
                        }

                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Hibás érték.");
                    continue;
                }
            }

            alg(mag);
            Avg(Lista.ToArray());

            bool _data = false;
            while (true)
            {
                Console.Write("\nKérsz részletes leírást a feldolgozott adatokról? (I/n): ");
                string input = Console.ReadLine();
                if (input.ToLower() == "i")
                {
                    _data = true;
                    break;
                }
                else if (input.ToLower() == "n")
                {
                    break;
                }
            }

            if (_data)
            {
                int _x = 0;
                foreach (A x in Lista)
                {
                    Console.WriteLine($"#{_x} -> Kezdő Index: {x.StartIndex}, Méret: {x.EndIndex + 1 - x.StartIndex} Vége: {x.EndIndex}, Típus: {x._tipus}");
                    Console.WriteLine($"Mérések(Össz.: {x.Meresek.Count}):");
                    foreach (Meres l in x.Meresek)
                    {
                        Console.WriteLine($"--> Index: {l.Index}, Magasság: {l.Magassag}");
                        _x++;
                    }
                }
            }

            Console.WriteLine("\nA program véget ért. Nyomd meg az ENTER billentyűt, hogy bezáródjon.");
            Console.ReadLine();
        }
    }
}
