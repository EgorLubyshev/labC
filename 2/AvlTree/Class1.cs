using System;

public class AVLTree<TKey, TValue> where TKey : IComparable<TKey>
{
    private class AVLNode
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public AVLNode Left { get; set; }
        public AVLNode Right { get; set; }
        public int Height { get; set; }

        public AVLNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Height = 1;
        }
    }

    private AVLNode root;
    public int Count { get; private set; }

    public void Insert(TKey key, TValue value)
    {
        root = Insert(root, key, value);
    }

    private AVLNode Insert(AVLNode node, TKey key, TValue value)
    {
        if (node == null)
        {
            Count++;
            return new AVLNode(key, value);
        }

        int compareResult = key.CompareTo(node.Key);
        if (compareResult < 0)
        {
            node.Left = Insert(node.Left, key, value);
        }
        else if (compareResult > 0)
        {
            node.Right = Insert(node.Right, key, value);
        }
        else
        {
            node.Value = value;
            return node;
        }

        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

        return Balance(node);
    }

    public bool Remove(TKey key)
    {
        int initialCount = Count;
        root = Remove(root, key);
        return initialCount != Count;
    }

    private AVLNode Remove(AVLNode node, TKey key)
    {
        if (node == null) return null;

        int compareResult = key.CompareTo(node.Key);
        if (compareResult < 0)
        {
            node.Left = Remove(node.Left, key);
        }
        else if (compareResult > 0)
        {
            node.Right = Remove(node.Right, key);
        }
        else
        {
            if (node.Left == null || node.Right == null)
            {
                AVLNode temp = node.Left ?? node.Right;
                if (temp == null)
                {
                    temp = node;
                    node = null;
                }
                else
                {
                    node = temp;
                }
                Count--;
            }
            else
            {
                AVLNode temp = MinValueNode(node.Right);
                node.Key = temp.Key;
                node.Value = temp.Value;
                node.Right = Remove(node.Right, temp.Key);
            }
        }

        if (node == null) return null;

        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

        return Balance(node);
    }

    private AVLNode MinValueNode(AVLNode node)
    {
        AVLNode current = node;
        while (current.Left != null)
        {
            current = current.Left;
        }
        return current;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        AVLNode node = root;
        while (node != null)
        {
            int compareResult = key.CompareTo(node.Key);
            if (compareResult < 0)
            {
                node = node.Left;
            }
            else if (compareResult > 0)
            {
                node = node.Right;
            }
            else
            {
                value = node.Value;
                return true;
            }
        }
        value = default;
        return false;
    }

    private AVLNode Balance(AVLNode node)
    {
        int balanceFactor = GetBalanceFactor(node);

        if (balanceFactor > 1)
        {
            if (GetBalanceFactor(node.Left) >= 0)
            {
                return RotateRight(node);
            }
            else
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
        }
        else if (balanceFactor < -1)
        {
            if (GetBalanceFactor(node.Right) <= 0)
            {
                return RotateLeft(node);
            }
            else
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
        }

        return node;
    }

    private AVLNode RotateRight(AVLNode y)
    {
        AVLNode x = y.Left;
        AVLNode T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
        x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

        return x;
    }

    private AVLNode RotateLeft(AVLNode x)
    {
        AVLNode y = x.Right;
        AVLNode T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
        y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

        return y;
    }

    private int Height(AVLNode node)
    {
        return node?.Height ?? 0;
    }

    private int GetBalanceFactor(AVLNode node)
    {
        return node == null ? 0 : Height(node.Left) - Height(node.Right);
    }

    public bool ContainsKey(TKey key)
    {
        return TryGetValue(key, out _);
    }
}