public class Capacity {
    public int capacity { get; private set; }

    public Capacity() {}

    public Capacity (int capacity) {
        this.capacity = capacity;
    }

    public void ChangeCapacity(int capacity) {
        this.capacity = capacity;
    }

}