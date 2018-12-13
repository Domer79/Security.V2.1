using System;
using System.Collections.Generic;
using Security.Model;

namespace Security.Tests.Infrastructure
{
    public class UserComparer : Comparer<User>
    {
        public override int Compare(User x, User y)
        {
            if (x.Status.CompareTo(y.Status) != 0)
                return x.Status.CompareTo(y.Status);
            if (x.Id.CompareTo(y.Id) != 0)
                return x.Id.CompareTo(y.Id);
            if (x.IdMember.CompareTo(y.IdMember) != 0)
                return x.IdMember.CompareTo(y.IdMember);
            if (x.Login.CompareTo(y.Login) != 0)
                return x.Login.CompareTo(y.Login);
            if (x.Name.CompareTo(y.Name) != 0)
                return x.Name.CompareTo(y.Name);
            if (x.DateCreated.CompareTo(y.DateCreated) != 0)
                return x.DateCreated.CompareTo(y.DateCreated);
            if (x.Email.CompareTo(y.Email) != 0)
                return x.Email.CompareTo(y.Email);
            if (x.FirstName.CompareTo(y.FirstName) != 0)
                return x.FirstName.CompareTo(y.FirstName);
            if (x.LastName.CompareTo(y.LastName) != 0)
                return x.LastName.CompareTo(y.LastName);
            if (Nullable.Compare(x.LastActivityDate, y.LastActivityDate) != 0)
                return Nullable.Compare(x.LastActivityDate, y.LastActivityDate);
            if (x.PasswordSalt.CompareTo(y.PasswordSalt) != 0)
                return x.PasswordSalt.CompareTo(y.PasswordSalt);

            return 0;
        }
    }
}