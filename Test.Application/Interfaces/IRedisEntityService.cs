﻿namespace Test.Application.Interfaces
{
    public interface IRedisEntityService<TEntity>
    {
        Task<TEntity> AddEntity(TEntity entity);

        Task<TEntity?> GetEntity(Guid id);

        Task<TEntity?> GetEntity(long tEntityMember);

        Task UpdateEntity(TEntity entity);

        Task RemoveEntity(Guid id);
    }
}
