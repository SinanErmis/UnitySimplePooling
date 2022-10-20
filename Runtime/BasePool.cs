using System.Collections.Generic;

namespace SimplePooling.Runtime
{
    public abstract class BasePool<T>
    {
        protected readonly T Original;
        private readonly Stack<T> _objectPool = new Stack<T>();

        protected BasePool(T original)
        {
            Original = original;
        }

        public void LoadPool(int number)
        {
            for (int i = 0; i < number; i++)
            {
                T obj = CreateObject();
                AddObject(obj);
            }
        }

        public T[] GetAll()
        {
            return _objectPool.ToArray();
        }

        public T GetObject()
        {
            if (!_objectPool.TryPop(out var obj))
            {
                obj = CreateObject();
            }

            OnBeforeGetObject(obj);
            
            return obj;
        }

        public virtual void AddObject(T obj)
        {
            OnBeforeAddObject(obj);
            _objectPool.Push(obj);
        }

        protected abstract T CreateObject();

        protected virtual void OnBeforeAddObject(T obj)
        {
        }

        protected virtual void OnBeforeGetObject(T obj)
        {
        }
    }
}