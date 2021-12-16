

using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class MyOneLinkedList<T> : IEnumerable<T>
    {
        // Узел
        public class OneLinkedNode<T>
        {
            public T Data { get; set; }
            public OneLinkedNode<T> Next { get; set; }

            public OneLinkedNode(T data)
            {
                Data = data;
            }
        }
        // Поля
        private OneLinkedNode<T> _head;
        private OneLinkedNode<T> _last;
        private int _size;

        
        // Конструкторы
        public MyOneLinkedList()
        {
            _size = 0;
        }
        public MyOneLinkedList(IEnumerable<T> collections)
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
            OneLinkedNode<T> current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this).GetEnumerator();
        // Добавление элемента в конец
        public void Add(T item)
        {
            OneLinkedNode<T> node = new OneLinkedNode<T>(item);
            if (_head == null)
                _head = node;
            else
                _last = node;
            _size++;
        }
        // Добавление в начало
        public void AddFirst(OneLinkedNode<T> node)
        {
            IsEmpty(node);
            node.Next = _head;
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
            _size--;
        }
        // Размер списка
        public int Size() => _size;
        // Узел по индексу
        public OneLinkedNode<T> NodeAt(int index)
        {
            if (index >= _size || index < 0)
                throw new IndexOutOfRangeException("Индекс выходит за приделы списка");
            OneLinkedNode<T> result = _head;
            for (int i = 0; i < index; i++)
                result = result.Next;
            return result;
        }
        // Значение по индексу
        public T ElementAt (int index) => NodeAt(index).Data;

    }
}