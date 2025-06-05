using System;
using System.Threading;

public class Philosopher
{
    private readonly int id;
    private readonly object leftFork;
    private readonly object rightFork;
    private readonly SemaphoreSlim waiter;
    private readonly int mealCount;

    public Philosopher(int id, object leftFork, object rightFork, SemaphoreSlim waiter, int mealCount)
    {
        this.id = id;
        this.leftFork = leftFork;
        this.rightFork = rightFork;
        this.waiter = waiter;
        this.mealCount = mealCount;
    }

    private void DoAction(string action)
    {
        Console.WriteLine($"Філософ {id} {action}");
        Thread.Sleep(100);
    }

    public void Run()
    {
        try
        {
            for (int i = 0; i < mealCount; i++)
            {
                DoAction("думає");

                waiter.Wait();

                lock (leftFork)
                {
                    DoAction("взяв ліву виделку");

                    lock (rightFork)
                    {
                        DoAction("взяв праву виделку — їсть");
                        DoAction("поклав праву виделку");
                    }

                    DoAction("поклав ліву виделку");
                }

                waiter.Release();

                DoAction($"завершив прийом їжі №{i + 1}");
            }

            DoAction("встав з-за столу");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Філософ {id} отримав помилку: {ex.Message}");
        }
    }
}
