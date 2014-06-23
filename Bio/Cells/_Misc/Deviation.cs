using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio
{
    class Deviation
    {
        private static readonly Random Random = new Random();

        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }

        public Deviation()
        {
            A = 1;
            B = 1;
            C = 1;
        }

        public void Rool(double delta)
        {
            A *= (1 + Math.Sign(Random.Next(2) - 1) * delta * Random.Next(1));
            B *= (1 + Math.Sign(Random.Next(2) - 1) * delta * Random.Next(1));
            C *= (1 + Math.Sign(Random.Next(2) - 1) * delta * Random.Next(1));
        }

        public double this[DeviationGroup dg]
        {
            get
            {
                if (dg == DeviationGroup.A)   
                    return A;

                if (dg == DeviationGroup.B)
                    return B;

                if (dg == DeviationGroup.C)
                    return C;

                throw new Exception();
            }
        }
    }

    internal enum DeviationGroup
    {
        None = 0,
        A = 1,
        B = 2,
        C = 3
    };
}
