using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IDictionary<TKey, TValue>
{
    private const int InitialCapacity = 16;
    private const float LoadFactorThreshold = 0.75f;
    private LinkedList<KeyValuePair<TKey, TValue>>[] _buckets;
    private int _count;
    private IEqualityComparer<TKey> _comparer;

    public HashTable() : this(EqualityComparer<TKey>.Default) { }

    public HashTable(IEqualityComparer<TKey> comparer)
    {
        _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[InitialCapacity];
        _comparer = comparer;
    }

    private void Resize()
    {
        var newBuckets = new LinkedList<KeyValuePair<TKey, TValue>>[_buckets.Length * 2];
        foreach (var bucket in _buckets)
        {
            if (bucket != null)
            {
                foreach (var pair in bucket)
                {
                    int newIndex = GetBucketIndex(pair.Key, newBuckets.Length);
                    if (newBuckets[newIndex] == null)
                        newBuckets[newIndex] = new LinkedList<KeyValuePair<TKey, TValue>>();
                    newBuckets[newIndex].AddLast(pair);
                }
            }
        }
        _buckets = newBuckets;
    }

    private int GetBucketIndex(TKey key, int length = -1)
    {
        if (length == -1)
            length = _buckets.Length;
        return Math.Abs(_comparer.GetHashCode(key)) % length;
    }

    public void Add(TKey key, TValue value)
    {
        if (_count >= _buckets.Length * LoadFactorThreshold)
            Resize();

        int index = GetBucketIndex(key);
        if (_buckets[index] == null)
            _buckets[index] = new LinkedList<KeyValuePair<TKey, TValue>>();

        foreach (var pair in _buckets[index])
        {
            if (_comparer.Equals(pair.Key, key))
                throw new ArgumentException("Key already exists.");
        }

        _buckets[index].AddLast(new KeyValuePair<TKey, TValue>(key, value));
        _count++;
    }

    public bool Remove(TKey key)
    {
        int index = GetBucketIndex(key);
        if (_buckets[index] == null)
            return false;

        var node = _buckets[index].First;
        while (node != null)
        {
            if (_comparer.Equals(node.Value.Key, key))
            {
                _buckets[index].Remove(node);
                _count--;
                return true;
            }
            node = node.Next;
        }
        return false;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = GetBucketIndex(key);
        if (_buckets[index] != null)
        {
            foreach (var pair in _buckets[index])
            {
                if (_comparer.Equals(pair.Key, key))
                {
                    value = pair.Value;
                    return true;
                }
            }
        }
        value = default;
        return false;
    }

    public TValue this[TKey key]
    {
        get
        {
            if (TryGetValue(key, out var value))
                return value;
            throw new KeyNotFoundException();
        }
        set
        {
            int index = GetBucketIndex(key);
            if (_buckets[index] == null)
                _buckets[index] = new LinkedList<KeyValuePair<TKey, TValue>>();

            var node = _buckets[index].First;
            while (node != null)
            {
                if (_comparer.Equals(node.Value.Key, key))
                {
                    node.Value = new KeyValuePair<TKey, TValue>(key, value);
                    return;
                }
                node = node.Next;
            }

            _buckets[index].AddLast(new KeyValuePair<TKey, TValue>(key, value));
            _count++;
        }
    }

    public ICollection<TKey> Keys => this.Select(p => p.Key).ToList();
    public ICollection<TValue> Values => this.Select(p => p.Value).ToList();
    public int Count => _count;
    public bool IsReadOnly => false;

    public bool ContainsKey(TKey key)
    {
        return TryGetValue(key, out _);
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        Add(item.Key, item.Value);
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        if (TryGetValue(item.Key, out var value))
            return EqualityComparer<TValue>.Default.Equals(value, item.Value);
        return false;
    }

    public void Clear()
    {
        _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[InitialCapacity];
        _count = 0;
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        foreach (var pair in this)
        {
            array[arrayIndex++] = pair;
        }
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        if (Contains(item))
            return Remove(item.Key);
        return false;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        foreach (var bucket in _buckets)
        {
            if (bucket != null)
            {
                foreach (var pair in bucket)
                    yield return pair;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
