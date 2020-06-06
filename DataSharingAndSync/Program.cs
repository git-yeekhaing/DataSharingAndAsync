using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSharingAndSync
{
    class Program
    {
        static void Main(string[] args)
        {
            CriticalSession();
            Console.ReadLine();
        }

        public static void CriticalSession()
        {
            var tasks = new List<Task>();
            BankAccount ba = new BankAccount();

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    ba.Deposit(100);
                }
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    ba.Withdraw(100);
                }
            }));

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}.");
            Console.WriteLine("All done here.");
        }
    }

    class BankAccount
    {
        public object padlock = new object();
        public int balance;
        public int Balance { get; private set; }

        // interlocked class contains atomic operations on variables
        // atomic = cannot be interrupted
        public void Deposit(int amount)
        {
            Interlocked.Add(ref balance, amount);
        }

        public void Withdraw(int amount)
        {
            Interlocked.Add(ref balance, -amount);
        }
    }
}
