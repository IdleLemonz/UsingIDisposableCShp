using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// To ensure consistent behaviour between implementations, IDisposable has a few rules that should
// be followed:
//  1. The method should be able to be called more than once without consequence
//  2. The object should implement a finalize method, which calls Dispose in the event that
//      Dispose was not explicitly called
//  3. Dispose should called GC.SuppressFinalize to prevent the GC from calling Finalize
//  4. Methods called after Dispose should throw an ObjectDisposedException if that code relies
//      on disposed data

namespace UsingIDisposable
{
    class Program
    {
        static void Main(string[] args)
        {
            MyTestClass myTestClass = new MyTestClass();
            Console.ReadLine();
            myTestClass.Dispose();
            Console.ReadLine();            
        }
        
    }

    class MyTestClass : IDisposable
    {
        public MyTestClass()
        {
            Console.WriteLine("Constructing");
        }
        public void Dispose()
        {
            if (isDisposed)
            {
                isDisposed = true;
                Console.WriteLine("Disposing");
                GC.SuppressFinalize(this);
                //Dispose of resources here
            }
            else
            {
                throw new ObjectDisposedException("MyTestClass", "Already disposed");
            }
        }
        ~MyTestClass()
        {
            Console.WriteLine("Finalizing");
            this.Dispose();
        }

        public void DoSomething()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException("MyTestClass");
            }
            //Use resources if they haven't been disposed
        }

        private bool isDisposed = false;
    }
}
