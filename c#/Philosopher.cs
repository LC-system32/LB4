using System;
using System.Threading;

public class Philosopher
{
    private readonly int id;
    private readonly Semaphore leftFork;
    private readonly Semaphore rightFork;
    private readonly Semaphore waiter;
    private readonly int mealCount;

    public Philosopher(int id, Semaphore leftFork, Semaphore rightFork, Semaphore waiter, int mealCount)
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

                waiter.WaitOne();

                leftFork.WaitOne();
                DoAction("взяв ліву виделку");

                rightFork.WaitOne();
                DoAction("взяв праву виделку — їсть");

                DoAction("поклав праву виделку");
                rightFork.Release();

                DoAction("поклав ліву виделку");
                leftFork.Release();

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
