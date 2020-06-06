using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public int Balance { get; set; }

        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            Balance -= amount;
        }
    }
}
