using System;
using System.Threading;

class Program
{
    static void Main()
    {
        int philosophers = 2;
        int meals = 1;

        SemaphoreSlim[] forks = new SemaphoreSlim[philosophers];
        for (int i = 0; i < philosophers; i++)
        {
            forks[i] = new SemaphoreSlim(1, 1);
        }

        Semaphore waiter = new Semaphore(philosophers - 1, philosophers - 1);

        for (int i = 0; i < philosophers; i++)
        {
            SemaphoreSlim leftFork = forks[i];
            SemaphoreSlim rightFork = forks[(i + 1) % philosophers];

            Philosopher p = new Philosopher(i, leftFork, rightFork, waiter, meals);
            new Thread(p.Run).Start();
        }
    }
}
