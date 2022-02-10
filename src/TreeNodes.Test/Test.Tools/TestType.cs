using System;

namespace TreeNodes.Test
{
    internal static class TestType
    {
        internal static bool IsPublic<T>() => IsPublic(typeof(T)); 
        internal static bool IsPublic(Type t)
        {
            return
              t.IsVisible
              && t.IsPublic
              && !t.IsNotPublic
              && !t.IsNested
              && !t.IsNestedPublic
              && !t.IsNestedFamily
              && !t.IsNestedPrivate
              && !t.IsNestedAssembly
              && !t.IsNestedFamORAssem
              && !t.IsNestedFamANDAssem;
        }
        internal static bool IsInternal<T>() => IsInternal(typeof(T));
        internal static bool IsInternal(Type t)
        { 
            return
                !t.IsVisible
                && !t.IsPublic
                && t.IsNotPublic
                && !t.IsNested
                && !t.IsNestedPublic
                && !t.IsNestedFamily
                && !t.IsNestedPrivate
                && !t.IsNestedAssembly
                && !t.IsNestedFamORAssem
                && !t.IsNestedFamANDAssem;
        }
        internal static bool IsProtected<T>() => IsProtected(typeof(T));
        internal static bool IsProtected(Type t)
        { 
            return
                !t.IsVisible
                && !t.IsPublic
                && !t.IsNotPublic
                && t.IsNested
                && !t.IsNestedPublic
                && t.IsNestedFamily
                && !t.IsNestedPrivate
                && !t.IsNestedAssembly
                && !t.IsNestedFamORAssem
                && !t.IsNestedFamANDAssem;
        }
        internal static bool IsPrivate<T>() => IsPrivate(typeof(T));
        internal static bool IsPrivate(Type t)
        { 
            return
                !t.IsVisible
                && !t.IsPublic
                && !t.IsNotPublic
                && t.IsNested
                && !t.IsNestedPublic
                && !t.IsNestedFamily
                && t.IsNestedPrivate
                && !t.IsNestedAssembly
                && !t.IsNestedFamORAssem
                && !t.IsNestedFamANDAssem;
        }
    }
}
