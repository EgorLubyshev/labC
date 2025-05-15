using System;

public class AVLTree<TKey, TValue> where TKey : IComparable<TKey>
{
    private class Node
    {
        public TKey Key;
        public TValue Value;
        public Node Left, Right;
        public int Height;

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Height = 1;
        }
    }

    private Node root;

    public void Insert(TKey key, TValue value)
    {
        root = Insert(root, key, value);
    }

    private Node Insert(Node node, TKey key, TValue value)
    {
        if (node == null)
            return new Node(key, value);

        int cmp = key.CompareTo(node.Key);
        if (cmp < 0)
            node.Left = Insert(node.Left, key, value);
        else if (cmp > 0)
            node.Right = Insert(node.Right, key, value);
        else
            node.Value = value;

        return Balance(node);
    }

    public void Delete(TKey key)
    {
        root = Delete(root, key);
    }

    private Node Delete(Node node, TKey key)
    {
        if (node == null)
            return null;

        int cmp = key.CompareTo(node.Key);
        if (cmp < 0)
            node.Left = Delete(node.Left, key);
        else if (cmp > 0)
            node.Right = Delete(node.Right, key);
        else
        {
            if (node.Left == null)
                return node.Right;
            if (node.Right == null)
                return node.Left;

            Node min = GetMin(node.Right);
            node.Key = min.Key;
            node.Value = min.Value;
            node.Right = Delete(node.Right, min.Key);
        }

        return Balance(node);
    }

    private Node GetMin(Node node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }

    public bool Search(TKey key)
    {
        return Search(root, key) != null;
    }

    private Node Search(Node node, TKey key)
    {
        if (node == null)
            return null;

        int cmp = key.CompareTo(node.Key);
        if (cmp < 0)
            return Search(node.Left, key);
        else if (cmp > 0)
            return Search(node.Right, key);
        else
            return node;
    }

    private Node Balance(Node node)
    {
        UpdateHeight(node);
        int balance = GetBalance(node);

        if (balance > 1)
        {
            if (GetBalance(node.Right) < 0)
                node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        if (balance < -1)
        {
            if (GetBalance(node.Left) > 0)
                node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        return node;
    }

    private void UpdateHeight(Node node)
    {
        node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
    }

    private int GetHeight(Node node)
    {
        return node?.Height ?? 0;
    }

    private int GetBalance(Node node)
    {
        return GetHeight(node.Right) - GetHeight(node.Left);
    }

    private Node RotateLeft(Node node)
    {
        Node right = node.Right;
        node.Right = right.Left;
        right.Left = node;
        UpdateHeight(node);
        UpdateHeight(right);
        return right;
    }

    private Node RotateRight(Node node)
    {
        Node left = node.Left;
        node.Left = left.Right;
        left.Right = node;
        UpdateHeight(node);
        UpdateHeight(left);
        return left;
    }
}
