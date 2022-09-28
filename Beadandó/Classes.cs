using System.Collections.Generic;
using System.Linq;

namespace Beadandó
{
    internal class Classes
    {
        public class Meres
        {
            public enum Tipus { Szarazfold, Sziget, Tenger }

            public ushort Index;
            public ushort Magassag;
            public Tipus _tipus;
            public Meres(Tipus t, ushort _Magassag, ushort index)
            {
                _tipus = t;
                Magassag = _Magassag;
                Index = index;
            }
        }

        public class A
        {
            public Meres.Tipus _tipus;
            public List<Meres> Meresek = new List<Meres>();
            public ushort StartIndex
            {
                get { return Meresek[0].Index; }
            }
            public ushort EndIndex
            {
                get { return Meresek[Meresek.Count() -1].Index; }
            }

            public ushort MaxHeight
            {
                get {
                    ushort max = 0;
                    foreach (Meres m in Meresek)
                    {
                        if (m.Magassag > max)
                            max = m.Magassag;
                    }

                    return max;
                }
            }

            public A(Meres.Tipus tipus, List<Meres> meresek)
            {
                _tipus = tipus;
                foreach (Meres m in meresek)
                {
                    Meresek.Add(m);
                }
            }
        }
    }
}
