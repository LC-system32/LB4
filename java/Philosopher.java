import java.util.concurrent.Semaphore;

public class Philosopher extends Thread {
    private final int id;
    private final Semaphore leftFork;
    private final Semaphore rightFork;
    private final Semaphore officiant;
    private final int mealCount;

    public Philosopher(int id, Semaphore leftFork, Semaphore rightFork, Semaphore officiant, int mealCount) {
        this.id = id;
        this.leftFork = leftFork;
        this.rightFork = rightFork;
        this.officiant = officiant;
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

                leftFork.acquire();
                doAction("взяв ліву виделку");

                rightFork.acquire();
                doAction("взяв праву виделку — їсть");

                doAction("поклав праву виделку");
                rightFork.release();

                doAction("поклав ліву виделку");
                leftFork.release();

                officiant.release();

                doAction(String.format("завершив прийом їжі №%d", i + 1));
            }

            doAction("встав з-за столу");
        } catch (InterruptedException e) {
            System.out.printf("Філософ %d був перерваний%n", id);
        }
    }
}
