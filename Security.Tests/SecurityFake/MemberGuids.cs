using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Security.Tests.SecurityFakeDatabase
{
    public class MemberGuids
    {
        public static readonly ReadOnlyDictionary<int, Guid> Keys = new ReadOnlyDictionary<int, Guid>(new Dictionary<int, Guid>()
        {
            { 1, Guid.NewGuid()},
            { 2, Guid.NewGuid()},
            { 3, Guid.NewGuid()},
            { 4, Guid.NewGuid()},
            { 5, Guid.NewGuid()},
            { 6, Guid.NewGuid()},
            { 7, Guid.NewGuid()},
        }); 
    }
}