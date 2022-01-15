using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test3
{
    public class TestStatic : MarshalByRefObject, ICollection
    {
        private static int count = 1;

        public int Count
        {
            get
            {
                count = count * 2;
                return count;
            }
        }

        #region 未实现代码
        public bool IsSynchronized { get { throw new NotImplementedException(); } }

        public object SyncRoot { get { throw new NotImplementedException(); } }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
