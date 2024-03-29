﻿using System.Linq.Expressions;
using Movie.Membership.Database.Entities;

namespace Movie.Membership.Database.Services
{
    public interface IDbService
    {
        Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
            where TEntity : class, IEntity
            where TDto : class;
        Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity;
        Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity;
        Task<List<TDto>> GetAsync<TEntity, TDto>()
            where TEntity : class, IEntity
            where TDto : class;
        Task<List<TDto>> GetAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity
            where TDto : class;
        string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Include<TEntity>() where TEntity : class, IEntity;
        Task<bool> SaveChangesAsync();
        Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity
            where TDto : class;
        void Update<TEntity, TDto>(int id, TDto dto)
            where TEntity : class, IEntity
            where TDto : class;
    }
}