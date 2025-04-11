using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class MyStack<T>
    {
        private T[] _items;
        private int _count;
        private const int _defaultCapacity = 4;

        public MyStack()
        {
            _items = new T[_defaultCapacity];
        }

        public int Count => _count;

        public void Push(T item)
        {
            if (_count == _items.Length)
                EnsureCapacity();

            _items[_count++] = item;
        }

        public T Pop()
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty");

            T item = _items[--_count];
            _items[_count] = default; // очистка ссылки
            return item;
        }

        public T Peek()
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty");

            return _items[_count - 1];
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (Equals(_items[i], item))
                    return true;
            }
            return false;
        }

        private void EnsureCapacity()
        {
            int newCapacity = _items.Length * 2;
            if (newCapacity == 0)
                newCapacity = _defaultCapacity;

            T[] newArray = new T[newCapacity];
            Array.Copy(_items, newArray, _items.Length);
            _items = newArray;
        }
    }
}
