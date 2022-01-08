using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 测试工程
{

    public class DynamicObjectTest
    {
        public static void Test()
        {
            dynamic wroxDyn = new WroxDynamicObject();
            wroxDyn.FirstName = "FirstName1";
            wroxDyn.SecondName = "SecondName1";
            Console.WriteLine(wroxDyn.FirstName);
            Console.WriteLine(wroxDyn.SecondName);

            Func<DateTime, string> GetTomorrow = today => today.AddDays(1).ToShortDateString();
            wroxDyn.GetTomorrow = GetTomorrow;
            Console.WriteLine(wroxDyn.GetTomorrow(DateTime.Now));
        }
    }

    public class WroxDynamicObject :DynamicObject
    {
        private Dictionary<string, object> _dynamicData = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            bool success = false;
            result = null;
            if (_dynamicData.ContainsKey(binder.Name))
            {
                result = _dynamicData[binder.Name];
                success = true;
            }
            else
            {
                result = "not find property";
                success = false;
            }

            return success;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _dynamicData[binder.Name] = value;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            dynamic method = _dynamicData[binder.Name];
            result = method((DateTime)args[0]);
            return result != null;
        }
    }
}
