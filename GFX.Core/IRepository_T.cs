using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GFX.Core {
        public interface IRepository<T> : IRepository, IDisposable where T : class {

            IQueryable<T> Query(Func<T, bool> predicate);

            T Add(T item);

            T Remove(T item);

            int SaveChanges();

        }
}
