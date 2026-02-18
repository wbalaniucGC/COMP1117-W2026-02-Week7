using System.Collections.Generic;

public class CircularBuffer<T>  // Generic Circular Buffer
{
    // Collection itself
    private List<T> buffer;
    // Capacity
    private int capacity;

    // Constructor - Allow me to create a CircularBuffer with a given capacity
    public CircularBuffer(int capacity)
    {
        buffer = new List<T>(capacity);
        this.capacity = capacity;
    }

    // Public property
    /*
    public int Count
    {
        get
        {
            return buffer.Count;
        }
    }
    */
    public int Count => buffer.Count;   // The exact same as the read-only property above.


    // Buffer Operations
    // =================
    // 1. Push (Adding new information to the buffer)
    public void Push(T item)
    {
        // Check if my buffer is at or above capacity
        if(buffer.Count >= capacity)
        {
            buffer.RemoveAt(0);     // Removes the oldest data.
        }

        buffer.Add(item);
    }
    // 2. Pop (Removing the next piece of information)
    public T Pop()
    {
        if (buffer.Count == 0) return default(T);  

        int lastIndex = buffer.Count - 1;
        T item = buffer[lastIndex];       // Create a copy of the item in buffer[lastIndex] and stores it in 'item'
        buffer.RemoveAt(lastIndex);         // Removes the item at lastIndex.

        return item;
    }
}