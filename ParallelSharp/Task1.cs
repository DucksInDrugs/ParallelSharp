using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSharp
{
    public class Task1
    {
        public double My_Task(double x)
        {
            double S = 0;
            for (int k = 1; k <= Math.Max(20, 20 * Math.Abs(x)); k++)
            {
                for (int j = 1; j <= Math.Max(20, 20 * Math.Abs(x)); j++)
                {
                    double Tmp1 = ((x * x + x) * (k - j)) / (x * x + Math.Pow(k, 3) + Math.Pow(j, 3));
                    double Tmp2 = Math.Sin(k * x) * Math.Cos(j * x);
                    S += Tmp1 * Tmp2;
                }
            }
            return S;
        }

        public void Run()
        {
            const int N = 200;
            Task1 Obj = new();
            double[] V = new double[N+1];
            Int64 Tms = (DateTime.Now).Ticks;
            for (int k = 0; k < N; k++)
            {
                double x = 100 * Math.Cos(k);
                V[k] = Obj.My_Task(x);
            }
            Tms = (DateTime.Now).Ticks - Tms;
            TimeSpan Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательного цикла " + (Tmss.TotalSeconds).ToString() + " c");
            Tms = (DateTime.Now).Ticks;
            System.Threading.Tasks.Parallel.For(0, N, k => { V[k] = Obj.My_Task(k); });
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного цикла " + (Tmss.TotalSeconds).ToString() + " с");
        }
    }
}
