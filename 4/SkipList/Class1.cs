using System;
using System.Collections.Generic;

public class SkipListNode<TKey, TValue> where TKey : IComparable<TKey>
{
    public TKey Key;
    public TValue Value;
    public SkipListNode<TKey, TValue>[] Forward;

    public SkipListNode(int level, TKey key, TValue value)
    {
        Forward = new SkipListNode<TKey, TValue>[level + 1];
        Key = key;
        Value = value;
    }
}

public class SkipList<TKey, TValue> where TKey : IComparable<TKey>
{
    private readonly int _maxLevel;
    private readonly double _probability;
    private int _level;
    private readonly SkipListNode<TKey, TValue> _header;
    private readonly Random _random = new Random();

    public SkipList(int maxLevel = 16, double probability = 0.5)
    {
        _maxLevel = maxLevel;
        _probability = probability;
        _level = 0;
        _header = new SkipListNode<TKey, TValue>(_maxLevel, default(TKey), default(TValue));
    }

    private int RandomLevel()
    {
        int lvl = 0;
        while (_random.NextDouble() < _probability && lvl < _maxLevel)
            lvl++;
        return lvl;
    }

    public void Insert(TKey key, TValue value)
    {
        var update = new SkipListNode<TKey, TValue>[_maxLevel + 1];
        var x = _header;

        for (int i = _level; i >= 0; i--)
        {
            while (x.Forward[i] != null && x.Forward[i].Key.CompareTo(key) < 0)
            {
                x = x.Forward[i];
            }
            update[i] = x;
        }

        x = x.Forward[0];

        if (x != null && x.Key.CompareTo(key) == 0)
        {
            x.Value = value;
        }
        else
        {
            int lvl = RandomLevel();
            if (lvl > _level)
            {
                for (int i = _level + 1; i <= lvl; i++)
                {
                    update[i] = _header;
                }
                _level = lvl;
            }

            var newNode = new SkipListNode<TKey, TValue>(lvl, key, value);
            for (int i = 0; i <= lvl; i++)
            {
                newNode.Forward[i] = update[i].Forward[i];
                update[i].Forward[i] = newNode;
            }
        }
    }

    public bool Search(TKey key, out TValue value)
    {
        var x = _header;
        for (int i = _level; i >= 0; i--)
        {
            while (x.Forward[i] != null && x.Forward[i].Key.CompareTo(key) < 0)
            {
                x = x.Forward[i];
            }
        }

        x = x.Forward[0];
        if (x != null && x.Key.CompareTo(key) == 0)
        {
            value = x.Value;
            return true;
        }

        value = default(TValue);
        return false;
    }

    public bool Delete(TKey key)
    {
        var update = new SkipListNode<TKey, TValue>[_maxLevel + 1];
        var x = _header;

        for (int i = _level; i >= 0; i--)
        {
            while (x.Forward[i] != null && x.Forward[i].Key.CompareTo(key) < 0)
            {
                x = x.Forward[i];
            }
            update[i] = x;
        }

        x = x.Forward[0];

        if (x != null && x.Key.CompareTo(key) == 0)
        {
            for (int i = 0; i <= _level; i++)
            {
                if (update[i].Forward[i] != x)
                    break;
                update[i].Forward[i] = x.Forward[i];
            }

            while (_level > 0 && _header.Forward[_level] == null)
            {
                _level--;
            }

            return true;
        }

        return false;
    }
}
