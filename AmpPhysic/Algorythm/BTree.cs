using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoTest
{
    class BTreeSection<T>
    {
        public double minimum;
        public double maximum;
        public T data;

        public void AddLeft(T data)
        {
        }
        public void AddRight(T data)
        {
        }
    }

    class BTree<T>
    {
        protected BTreeSection<T> Root;
        public void Add(double Value, T data)
        {

            var tmp = new BTreeSection<T>();

        }

        public T GetAll(double Value)
        {
            return default(T);
        }
    }
}
