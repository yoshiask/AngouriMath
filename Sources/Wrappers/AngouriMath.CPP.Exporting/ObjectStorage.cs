﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AngouriMath.CPP.Exporting
{
    partial class Exports
    {
        internal static class ObjStorage<T>
        {
            private static ObjRef lastId = new(0);
            private readonly static Dictionary<ObjRef, T> allocations = new();
            internal static ObjRef Alloc(T obj)
            {
                lastId = lastId.Next();
                allocations[lastId] = obj;
                return lastId;
            }
            internal static ObjRef Alloc(ObjRef ptr, T obj)
            {
                allocations[ptr] = obj;
                return ptr;
            }
            internal static void Dealloc(ObjRef ptr)
            {
                if (!allocations.ContainsKey(ptr))
                    throw new DeallocationException();
                allocations.Remove(ptr);
            }
            internal static T Get(ObjRef ptr)
            {
                if (!allocations.ContainsKey(ptr))
                    throw new NonExistentObjectAddressingException();
                return allocations[ptr];
            }
        }
    }
}
