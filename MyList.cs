using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ConsoleApplication1
{
    public class MyList<T> : IList<T>
    {
        private T[] _list;
        private int _size;
        private int _capacity;


        // Конструкторы
        public MyList()
        {
            _size = 0;
            Capacity = 0;
        }
        public MyList(int capacity)
        {
            _list = new T[capacity];
            _size = 0;
            Capacity = capacity;
        }
        public MyList(T[] collection)
        {
            _list = (T[])collection.Clone();
            _size = collection.Length;
            Capacity = collection.Length;
        }
        private int Capacity
        {
            get => _capacity;
            set
            {
                if (value < 0) throw new ArgumentException("Размер листа не может быть отрицательным");
                _capacity = value;
            }
        }

        public int Count => _size;

        
        // Энумераторы
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return _list[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return _list[i];
        }
        // Добавить эоемент
        public void Add(T item)
        {
            if (_size == Capacity)
                Extend();

            _list[_size] = item;
            _size++;
        }

        // Расширить список
        public void Extend(int n = 1)
        {
            T[] tmp = new T[Capacity + n]; 
            _list.CopyTo(tmp, 0);
            Capacity = tmp.Length;
            _list = tmp;
        }

        // Очистить лист
        public void Clear() => _size = 0;
        
        // Проверить существование эоемента в списке
        public bool Contains(T item) => IndexOf(item) >= 0;
        
        // Копировать в другой список
        public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);
        
        // Удаляет элемент
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index < 0) return false;
            RemoveAt(index);
            return true;
        }

        public bool IsReadOnly => false;
        
        //Вернуть индекс элемента
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
                if (_list[i].Equals(item))
                    return i;

            return -1;
        }

        // Вставить элемент по индексу
        public void Insert(int index, T item)
        {
            CheckIndex(index);

            if (Count == Capacity) Extend();
            for (int i = Count - 1; i > index - 1; i--)
                _list[i + 1] = _list[i];
            _list[index] = item;
        }
        // Удалить по индексу
        public void RemoveAt(int index)
        {
            CheckIndex(index);
            T[] tmp = new T[Capacity]; 
            _list.CopyTo(tmp, 0);
            for (int i = index; i < Count - 1; i++)
                tmp[i] = _list[i + 1];
            _list = tmp;
            _size--;
        }

        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return _list[index];
            }
            set
            {
                CheckIndex(index);
                _list[index] = value;
            }
        }
        // Проверить корректность индекса
        private void CheckIndex(int index)
        {
            if (index > Count || index < 0) throw new ArgumentException("Неверный индекс");
        }
    }
}