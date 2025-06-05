using System;
using System.Threading;

class Program
{
    static void Main()
    {
        int philosophers = 2;
        int meals = 1;

        object[] forks = new object[philosophers];
        for (int i = 0; i < philosophers; i++)
        {
            forks[i] = new object();
        }

        SemaphoreSlim waiter = new SemaphoreSlim(philosophers - 1);

        for (int i = 0; i < philosophers; i++)
        {
            object leftFork = forks[i];
            object rightFork = forks[(i + 1) % philosophers];
            Philosopher p = new Philosopher(i, leftFork, rightFork, waiter, meals);
            new Thread(p.Run).Start();
        }
    }
}
