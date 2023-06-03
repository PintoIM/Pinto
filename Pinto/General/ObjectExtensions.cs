using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PintoNS.General
{
    public static class ObjectExtensions
    {
        public static T CastTo<T>(this object obj) => (T)obj;

        public static object CastToReflected(this object obj, Type type)
        {
            return typeof(ObjectExtensions)
                .GetMethod(nameof(CastTo), BindingFlags.Static | BindingFlags.Public)
                .MakeGenericMethod(new Type[] { type })
                .Invoke(null, new object[] { obj });
        }
    }
}
