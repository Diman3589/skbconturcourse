using System;
using System.Diagnostics;

namespace MemoryControl
{
    public class Timer : IDisposable
    {
        private readonly Stopwatch _timer;
        public long ElapsedMilliseconds { get; private set; }
        public Timer()
        {
            _timer = new Stopwatch();
        }

        public Timer Start()
        {
            _timer.Reset();
            _timer.Start();
            return this;
        }

        public Timer Continue()
        {
            _timer.Start();
            return this;
        }

        public void Dispose()
        {
            _timer.Stop();
            ElapsedMilliseconds = _timer.ElapsedMilliseconds;
        }

    }

    
    public class Program
    {
        public static void Main1(string[] args)
        {
            var timer = new Timer();
            using (timer.Start())
            {
                Console.WriteLine("Inside using start");   
            }
            Console.WriteLine(timer.ElapsedMilliseconds);

            using (timer.Continue())
            {
                Console.WriteLine("Inside using continue");   
            }
            Console.WriteLine(timer.ElapsedMilliseconds);
            Console.ReadLine();

        }
    }
}
