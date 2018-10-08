
using System.ComponentModel;
using System.Reflection;

namespace System
{
    static public class EnumExtend
    {

        public static string GetDescribe<T>(this T t, object data = null) where T : Enum
        {
            string describe = "";

            MemberInfo[] memInfo = typeof(T).GetMember(t.ToString());


            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    describe = ((DescriptionAttribute)attrs[0]).Description;
            }

            return describe;
        }

    }
}
