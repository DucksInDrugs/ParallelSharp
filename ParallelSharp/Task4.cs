using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSharp
{
    public class Task4
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
            Task4 fobj = new();
            double[] XArray = new double[N+1];
            List<double> XList = new();
            Stack<double> XStack = new();
            Queue<double> XQueue = new();

            double[] YArray = new double[N+1];
            List<double> YList = new();
            Stack<double> YStack = new();
            Queue<double> YQueue = new();

            for (int k = 0; k <= N; k++)
            {
                XArray[k] = 100 * Math.Cos(1.0 * k);
                XList.Add(100 * Math.Cos(1.0 * k));
                XStack.Push(100 * Math.Cos(1.0 * k));
                XQueue.Enqueue(100 * Math.Cos(1.0 * k));
            }

            Int64 Tms = (DateTime.Now).Ticks;
            int i = 0;
            foreach (double x in XArray) {
                YArray[i] = fobj.Func(x);
                i++;
            }
            Tms = (DateTime.Now).Ticks - Tms;
            TimeSpan Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательной операции foreach Array" + (Tmss.TotalSeconds).ToString() + " c");

            Tms = (DateTime.Now).Ticks;
            foreach (double x in XList) YList.Add(fobj.Func(x));
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательной операции foreach List" + (Tmss.TotalSeconds).ToString() + " c");

            Tms = (DateTime.Now).Ticks;
            foreach (double x in XStack) YStack.Push(fobj.Func(x));
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательной операции foreach Stack" + (Tmss.TotalSeconds).ToString() + " c");

            Tms = (DateTime.Now).Ticks;
            foreach (double x in XQueue) YQueue.Enqueue(fobj.Func(x));
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательной операции foreach Queue" + (Tmss.TotalSeconds).ToString() + " c");

            double[] YArrayParallel = new double[N + 1];
            List<double> YListParallel = new();
            Stack<double> YStackParallel = new();
            Queue<double> YQueueParallel = new();

            Object obj = new Object();
            i = 0;
            Tms = (DateTime.Now).Ticks;
            Parallel.ForEach(XArray,
                 x => {
                     double Tmp = fobj.Func(x);
                     lock (obj) { YArray[i] = Tmp; i++; }
                 }
                             );
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного метода ForEach Array" + (Tmss.TotalSeconds).ToString() + " c");

            obj = new Object();
            Tms = (DateTime.Now).Ticks;
            Parallel.ForEach(XList,
                 x => {
                     double Tmp = fobj.Func(x);
                     lock (obj) { YListParallel.Add(Tmp); }
                 }
                             );
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного метода ForEach List" + (Tmss.TotalSeconds).ToString() + " c");

            obj = new Object();
            Tms = (DateTime.Now).Ticks;
            Parallel.ForEach(XStack,
                 x => {
                     double Tmp = fobj.Func(x);
                     lock (obj) { YStackParallel.Push(Tmp); }
                 }
                             );
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного метода ForEach Stack" + (Tmss.TotalSeconds).ToString() + " c");

            obj = new Object();
            Tms = (DateTime.Now).Ticks;
            Parallel.ForEach(XQueue,
                 x => {
                     double Tmp = fobj.Func(x);
                     lock (obj) { YQueueParallel.Enqueue(Tmp); }
                 }
                             );
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного метода ForEach Queue" + (Tmss.TotalSeconds).ToString() + " c");
        }
    }
}
