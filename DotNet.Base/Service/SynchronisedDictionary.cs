using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DotNet.Base.Service 
{
    public class SynchronisedDictionary<TKey, TValue> : IDictionary<TKey, TValue> 
    {
        private Dictionary<TKey, TValue> innerDict;
        private ReaderWriterLockSlim readWriteLock;

        public SynchronisedDictionary() 
        {
            readWriteLock = new ReaderWriterLockSlim();
            innerDict = new Dictionary<TKey, TValue>();
        }

        public void Add(KeyValuePair<TKey, TValue> item) 
        {
            using (new AcquireWriteLock(readWriteLock)) 
            {
                innerDict[item.Key] = item.Value;
            }
        }

        public void Add(TKey key, TValue value) 
        {
            using (new AcquireWriteLock(readWriteLock)) 
            {
                this.innerDict[key] = value;
            }
        }

        public void Clear() 
        {
            using (new AcquireWriteLock(this.readWriteLock)) 
            {
                innerDict.Clear();
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) 
        {
            using (new AcquireReadLock(readWriteLock)) 
            {
                return innerDict.Contains<KeyValuePair<TKey, TValue>>(item);
            }
        }

        public bool ContainsKey(TKey key) 
        {
            using (new AcquireReadLock(readWriteLock)) 
            {
                return innerDict.ContainsKey(key);
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) 
        {
            using (new AcquireReadLock(readWriteLock)) 
            {
                innerDict.ToArray<KeyValuePair<TKey, TValue>>().CopyTo(array, arrayIndex);
            }
        }

        public IEnumerator GetEnumerator() 
        {
            using (new AcquireReadLock(readWriteLock)) 
            {
                return innerDict.GetEnumerator();
            }
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() 
        {
            using (new AcquireReadLock(this.readWriteLock)) 
            {
                return innerDict.GetEnumerator();
            }
        }

        public bool Remove(TKey key) 
        {
            bool isRemoved;
            using (new AcquireWriteLock(readWriteLock)) 
            {
                isRemoved = innerDict.Remove(key);
            }
            return isRemoved;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item) 
        {
            using (new AcquireWriteLock(readWriteLock)) 
            {
                return innerDict.Remove(item.Key);
            }
        }

        public bool TryGetValue(TKey key, out TValue value) 
        {
            using (new AcquireReadLock(readWriteLock)) 
            {
                return innerDict.TryGetValue(key, out value);
            }
        }

        public int Count 
        {
            get 
            {
                using (new AcquireReadLock(this.readWriteLock)) 
                {
                    return innerDict.Count;
                }
            }
        }

        public bool IsReadOnly 
        {
            get { return false; }
        }

        public TValue this[TKey key] 
        {
            get 
            {
                using (new AcquireReadLock(readWriteLock)) 
                {
                    return innerDict[key];
                }
            }
            set 
            {
                using (new AcquireWriteLock(readWriteLock))
                {
                    innerDict[key] = value;
                }
            }
        }

        public ICollection<TKey> Keys 
        {
            get 
            {
                using (new AcquireReadLock(this.readWriteLock)) 
                {
                    return this.innerDict.Keys;
                }
            }
        }

        public ICollection<TValue> Values 
        {
            get 
            {
                using (new AcquireReadLock(this.readWriteLock)) 
                {
                    return this.innerDict.Values;
                }
            }
        }

        private class AcquireReadLock : IDisposable 
        {
            private ReaderWriterLockSlim rwLock;
            private bool disposedValue;

            public AcquireReadLock(ReaderWriterLockSlim rwLock) 
            {
                this.rwLock = new ReaderWriterLockSlim();
                disposedValue = false;
                this.rwLock = rwLock;
                this.rwLock.EnterReadLock();
            }

            public void Dispose() 
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing) 
            {
                if (!disposedValue && disposing) 
                {
                    rwLock.ExitReadLock();
                }
                disposedValue = true;
            }
        }

        private class AcquireWriteLock : IDisposable 
        {
            private ReaderWriterLockSlim rwLock;
            private bool disposedValue;

            public AcquireWriteLock(ReaderWriterLockSlim rwLock) 
            {
                this.rwLock = new ReaderWriterLockSlim();
                disposedValue = false;
                this.rwLock = rwLock;
                this.rwLock.EnterWriteLock();
            }

            public void Dispose() 
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing) 
            {
                if (!disposedValue && disposing) 
                {
                    rwLock.ExitWriteLock();
                }
                disposedValue = true;
            }
        }
    }


}
