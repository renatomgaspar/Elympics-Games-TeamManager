﻿using Elympics_Games.API.Data.Entities;

namespace Elympics_Games.API.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();


        Task<T> GetByIdAsync(int id);


        Task CreateAsync(T entity);


        Task UpdateAsync(T entity);


        Task DeleteAsync(T entity);


        Task<bool> ExistAsync(int id);
    }
}