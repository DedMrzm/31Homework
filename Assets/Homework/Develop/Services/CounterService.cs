using System;
using System.Collections.Generic;

public class CounterService<T>
{
    public event Action Added;
    public event Action Removed;

    private int _addedCounter = 0;
    private int _removedCounter = 0;

    private List<T> _items = new();

    public int RemovedCounter => _removedCounter;
    public int AddedCounter => _addedCounter;
    public IReadOnlyList<T> Items => _items;

    public void Add(T item)
    {
        _items.Add(item);
        _addedCounter++;

        Added?.Invoke();
    }

    public void Remove(T item)
    {
        _items.Remove(item);

        _addedCounter--;
        _removedCounter++;

        Removed?.Invoke();
    }

    public void Restart()
    {
        _removedCounter = 0;
        _addedCounter = 0;
    }
}
