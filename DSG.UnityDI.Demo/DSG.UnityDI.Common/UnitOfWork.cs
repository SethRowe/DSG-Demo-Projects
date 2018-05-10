using System;

namespace DSG.UnityDI.Common
{
    public static class UnitOfWorkCounter
    {
        private static readonly object Lock = new object();
        private static int _counter = 0;

        public static int Count
        {
            get
            {
                lock (Lock)
                {
                    return _counter;
                }
            }
        }

        public static void Increment()
        {
            lock (Lock)
            {
                _counter++;
            }
        }
    }

    public class UnitOfWork : IUnitOfWork
    {
        public int RandomId { get; }

        public UnitOfWork()
        {
            UnitOfWorkCounter.Increment();
            RandomId = UnitOfWorkCounter.Count;
        }

        public bool Commit()
        {
            return true;
        }
    }
}