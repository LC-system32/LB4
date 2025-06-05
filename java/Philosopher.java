import java.util.concurrent.Semaphore;

public class Philosopher extends Thread {
    private final int id;
    private final Object leftFork;
    private final Object rightFork;
    private final Semaphore officiant;
    private final int mealCount;

    public Philosopher(int id, Object leftFork, Object rightFork, Semaphore waiter, int mealCount) {
        this.id = id;
        this.leftFork = leftFork;
        this.rightFork = rightFork;
        this.officiant = waiter;
        this.mealCount = mealCount;
    }

    private void doAction(String action) throws InterruptedException {
        System.out.printf("Філософ %d %s%n", id, action);
        Thread.sleep(100);
    }

    @Override
    public void run() {
        try {
            for (int i = 0; i < mealCount; i++) {
                doAction("думає");

                officiant.acquire();

                synchronized (leftFork) {
                    doAction("взяв ліву виделку");
                    synchronized (rightFork) {
                        doAction("взяв праву виделку — їсть");
                        doAction("поклав праву виделку");
                    }
                    doAction("поклав ліву виделку");
                }

                officiant.release();
                doAction(String.format("завершив прийом їжі №%d", i + 1));
            }

            doAction("встав з-за столу");
        } catch (InterruptedException e) {
            System.out.printf("Філософ %d був перерваний%n", id);
        }
    }
}
