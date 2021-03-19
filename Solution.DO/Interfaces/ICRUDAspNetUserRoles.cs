
using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.DO.Interfaces
    {
        public interface ICRUDAspNetUserRoles<T>
        {
            void Insert(T t);
            void Delete(T t);
            void Update(T t);
            IEnumerable<T> GetAll();
            T GetOneById(string id);
        }
    }
