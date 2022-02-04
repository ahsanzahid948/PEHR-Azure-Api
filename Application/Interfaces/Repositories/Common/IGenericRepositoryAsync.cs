using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<T> where T : class
    {

        Task<T> GetByIdAsync(long id);
        Task<T> GetByFilterAsync(string filterCriteria);
        Task<IReadOnlyList<T>> GetAllAsync(int pageNumber, int pageSize);
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(long id);
    }
}