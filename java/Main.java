import java.util.concurrent.Semaphore;

public class Main {
    public static void main(String[] args) {
        final int philosophers = 2;
        final int meals = 1;

        Semaphore[] forks = new Semaphore[philosophers];
        for (int i = 0; i < philosophers; i++) {
            forks[i] = new Semaphore(1);
        }

        Semaphore officiant = new Semaphore(philosophers - 1);

        for (int i = 0; i < philosophers; i++) {
            Semaphore leftFork = forks[i];
            Semaphore rightFork = forks[(i + 1) % philosophers];
            Philosopher p = new Philosopher(i, leftFork, rightFork, officiant, meals);
            p.start();
        }
    }
}
