// Main.java
import java.util.concurrent.Semaphore;

public class Main {
    public static void main(String[] args) {
        final int philosophers = 2;
        final int meals = 1;

        Object[] forks = new Object[philosophers];
        for (int i = 0; i < philosophers; i++) {
            forks[i] = new Object();
        }

        Semaphore officiant = new Semaphore(philosophers - 1);

        for (int i = 0; i < philosophers; i++) {
            Object leftFork = forks[i];
            Object rightFork = forks[(i + 1) % philosophers];
            Philosopher p = new Philosopher(i, leftFork, rightFork, officiant, meals);
            p.start();
        }
    }
}
