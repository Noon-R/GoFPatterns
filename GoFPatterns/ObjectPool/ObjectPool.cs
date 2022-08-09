using System;
using System.Collections.Generic;
using System.Text;

namespace GoFPatterns.ObjectPool
{
    interface IPoolable : IDisposable {
        void Init();
        void Reset();
        void PostProcess();

    }

    abstract class ObjectPool<T>
        where T : IPoolable
    {
        bool isDisposed = false;
        Queue<T> _PoolQueue;

        protected int MaxPoolCount
        {
            get
            {
                return int.MaxValue;
            }
        }

        public int Count
        {
            get
            {
                if (_PoolQueue == null) return 0;
                return _PoolQueue.Count;
            }
        }

        protected abstract T CreateInstance();

        protected virtual void OnRent(T instance)
        {
            //instance.gameObject.SetActive(true);
        }

        protected virtual void OnReturn(T instance)
        {
            //instance.gameObject.SetActive(false);
        }

        protected virtual void OnClear(T instance)
        {
            if (instance == null) return;


        }
        
        public T Rent()
        {
            if (isDisposed) throw new ObjectDisposedException("ObjectPool was already disposed.");
            if (_PoolQueue == null) _PoolQueue = new Queue<T>();

            var instance = (_PoolQueue.Count > 0)
                ? _PoolQueue.Dequeue()
                : CreateInstance();

            OnRent(instance);
            return instance;
        }

        public void Return(T instance)
        {
            if (isDisposed) throw new ObjectDisposedException("ObjectPool was already disposed.");
            if (instance == null) throw new ArgumentNullException("instance");

            if (_PoolQueue == null) _PoolQueue = new Queue<T>();

            if ((_PoolQueue.Count + 1) == MaxPoolCount)
            {
                throw new InvalidOperationException("Reached Max PoolSize");
            }

            OnReturn(instance);
            _PoolQueue.Enqueue(instance);
        }

        public void Clear(bool callOnBeforeRent = false)
        {
            if (_PoolQueue == null) return;
            while (_PoolQueue.Count != 0)
            {
                var instance = _PoolQueue.Dequeue();
                if (callOnBeforeRent)
                {
                    OnRent(instance);
                }
                OnClear(instance);
            }
        }

        public void Shrink(float instanceCountRatio, int minSize, bool callOnRent = false)
        {
            if (_PoolQueue == null) return;

            if (instanceCountRatio <= 0) instanceCountRatio = 0;
            if (instanceCountRatio >= 1.0f) instanceCountRatio = 1.0f;

            var size = (int)(_PoolQueue.Count * instanceCountRatio);
            size = Math.Max(minSize, size);

            while (_PoolQueue.Count > size)
            {
                var instance = _PoolQueue.Dequeue();
                if (callOnRent)
                {
                    OnRent(instance);
                }
                OnClear(instance);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    Clear(false);
                }

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

    }

}
