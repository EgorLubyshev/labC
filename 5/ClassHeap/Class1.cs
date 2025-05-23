namespace ClassHeap
{
    public class BinaryHeap<T>
    {
        private List<T> _elements;
        private readonly IComparer<T> _comparer;

        public BinaryHeap(IComparer<T>? comparer = null)
        {
            _elements = new List<T>();
            _comparer = comparer ?? Comparer<T>.Default;
        }

        public int Count => _elements.Count;

        public void Insert(T item)
        {
            _elements.Add(item);
            HeapifyUp(_elements.Count - 1);
        }

        public T Peek()
        {
            if (_elements.Count == 0) throw new InvalidOperationException("Heap is empty.");
            return _elements[0];
        }

        public T Extract()
        {
            if (_elements.Count == 0) throw new InvalidOperationException("Heap is empty.");

            T result = _elements[0];
            _elements[0] = _elements[^1];
            _elements.RemoveAt(_elements.Count - 1);
            HeapifyDown(0);
            return result;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (_comparer.Compare(_elements[index], _elements[parent]) >= 0) break;

                (_elements[parent], _elements[index]) = (_elements[index], _elements[parent]);
                index = parent;
            }
        }

        private void HeapifyDown(int index)
        {
            int last = _elements.Count - 1;
            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int smallest = index;

                if (left <= last && _comparer.Compare(_elements[left], _elements[smallest]) < 0)
                    smallest = left;
                if (right <= last && _comparer.Compare(_elements[right], _elements[smallest]) < 0)
                    smallest = right;

                if (smallest == index) break;

                (_elements[index], _elements[smallest]) = (_elements[smallest], _elements[index]);
                index = smallest;
            }
        }
    }

}
