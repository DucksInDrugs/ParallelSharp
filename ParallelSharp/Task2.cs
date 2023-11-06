using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSharp
{
    public class Task2
    {
        public double Func(double x)
        {
            int N = Math.Max(20, (int)Math.Floor(20 * Math.Abs(x)));
            double S = 0;
            double x2 = x * x;
            for (int k = 1; k <= N; k++)
                for (int j = 1; j <= N; j++)
                    S += ((x2 + x) * (k - j)) / (x2 + Math.Pow(k, 3) + Math.Pow(j, 3)) * Math.Sin(k * x) * Math.Cos(j * x); 
            return S;
        }

        public void Run()
        {
            const int N = 200;
            Task2 fobj = new();
            double[] X = new double[N + 1];
            for (int k = 0; k <= N; k++)
            {
                X[k] = 100 * Math.Cos(1.0 * k);
            }
            double S = 0;
            Int64 Tms = (DateTime.Now).Ticks;
            foreach (double x in X) S += fobj.Func(x);
            Tms = (DateTime.Now).Ticks - Tms;
            TimeSpan Tmss = new TimeSpan(Tms);
            Console.WriteLine("S=" + S.ToString());
            Console.WriteLine("Время выполнения последовательной операции foreach " + (Tmss.TotalSeconds).ToString() + " c");

            Object obj = new Object();
            S = 0;
            Tms = (DateTime.Now).Ticks;
            Parallel.ForEach(X,
                 x => {
                     double Tmp = fobj.Func(x);
                     lock (obj) { S += Tmp; }
                 }
                             );
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("S=" + S.ToString());
            Console.WriteLine("Время выполнения параллельного метода ForEach " + (Tmss.TotalSeconds).ToString() + " c");
        }
    }
}
