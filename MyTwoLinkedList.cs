using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class MyTwoLinkedList<T>: IEnumerable<T>
    {
        // Узел
        public class TwoLinkedNode<T>
        {
            public T Data { get; set; }
            public TwoLinkedNode<T> Previous { get; set; }
            public TwoLinkedNode<T> Next { get; set; }
            public TwoLinkedNode(T data)
            {
                Data = data;
            }
        }
        // Поля
        private TwoLinkedNode<T> _head;
        private TwoLinkedNode<T> _last;
        private int _size;

        
        // Конструкторы
        public MyTwoLinkedList()
        {
            _size = 0;
        }
        public MyTwoLinkedList(IEnumerable<T> collections)
        {
            IsEmpty(collections);
            _size = 0;
            foreach (var elem in collections)
            {
                Add(elem);
            }
        }
        private static void IsEmpty(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Объект не может быть пустым");
        }
        public IEnumerator<T> GetEnumerator()
        {
            TwoLinkedNode<T> current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) this).GetEnumerator();
        
        public IEnumerable<T> ReverseEnumerator()
        {
            TwoLinkedNode<T> current = _last;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
            }
        }
        // Добавление элемента в конец
        public void Add(T item)
        {
            TwoLinkedNode<T> node = new TwoLinkedNode<T>(item);
            if (_head == null)
                _head = node;
            else
            {
                _last.Next = node;
                node.Previous = _last;
            }
            _last = node;
            _size++;
        }
        // Добавление в начало
        public void AddFirst(TwoLinkedNode<T> node)
        {
            IsEmpty(node);
            node.Next = _head;
            _head.Previous = node;
            _head = node;
            if (_size == 0) _last = _head;
            _size++;
        }
        // Удаление из начала
        public void RemoveFirst()
        {
            if (_size == 0) 
                throw new Exception("Список должен быть не пустым");
            _head = _head.Next;
            _head.Previous = null;
            _size--;
        }
        // Удаление из конца
        public void RemoveLast()
        {
            if (_size == 0)
                throw new Exception("Список должен быть не пустым");
            _last = _last.Previous;
            _last.Next = null;
            _size--;
        }
        // Размер списка
        public int Size() => _size;
        // Узел по индексу
        public TwoLinkedNode<T> NodeAt(int index)
        {
            if (index >= _size || index < 0)
                throw new IndexOutOfRangeException("Индекс выходит за приделы списка");
            TwoLinkedNode<T> result = _head;
            if (index <= _size / 2)
            {
                for (int i = 0; i < index; i++)
                    result = result.Next;
                return result;
            }
            for (int i = _size; i > index; i--)
                result = result.Previous;
            return result;
        }
        // Значение по индексу
        public T ElementAt (int index) => NodeAt(index).Data;
    }
}