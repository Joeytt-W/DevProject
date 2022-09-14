using Framework.MainEntity.Data;
using Framework.Service.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Service.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T:class
    {
        private readonly FrameworkDBContext Db = new FrameworkDBContext();


        #region 添加数据
        public virtual async Task<bool> AddAsync(T entity, bool autoSave = false)
        {
            Db.Set<T>().Add(entity);

            if(autoSave)
                return await Db.SaveChangesAsync() > 0;

            return true;
        }
        public virtual async Task<bool> AddRangeAsync(List<T> entities, bool autoSave = false)
        {
            Db.Set<T>().AddRange(entities);

            if (autoSave)
                return await Db.SaveChangesAsync() > 0;

            return true;
        }
        #endregion

        #region 修改数据
        public virtual async Task<bool> UpdateAsync(T entity, bool autoSave = false)
        {
            Db.Entry(entity).State = EntityState.Modified;

            if (autoSave)
                return await Db.SaveChangesAsync() > 0;

            return true;
        }
        #endregion

        #region 删除数据
        public virtual async Task<bool> DeleteAsync(T entity, bool autoSave = false)
        {
            Db.Entry(entity).State = EntityState.Deleted;

            if (autoSave)
                return await Db.SaveChangesAsync() > 0;

            return true;
        }
        public virtual async Task<bool> DeleteAsync(int id, bool autoSave = false)
        {
            var entity = await Db.Set<T>().FindAsync(id);//如果实体已经在内存中，那么就直接从内存拿，如果内存中跟踪实体没有，那么才查询数据库。
            if (entity != null) Db.Entry(entity).State = EntityState.Deleted;

            if (autoSave)
                return await Db.SaveChangesAsync() > 0;

            return true;
        }

        public virtual async Task<bool> BatchDeleteAsync(List<T> entities, bool autoSave = false)
        {
            Db.Set<T>().RemoveRange(entities);

            if (autoSave)
                return await Db.SaveChangesAsync() > 0;

            return true;
        }
        public virtual async Task<bool> BatchDeleteAsync(bool autoSave = false,params int[] ids)
        {
            foreach (var item in ids)
            {
                var entity = Db.Set<T>().Find(item);//如果实体已经在内存中，那么就直接从内存拿，如果内存中跟踪实体没有，那么才查询数据库。
                if (entity != null) Db.Set<T>().Remove(entity);
            }

            if (autoSave)
                return await Db.SaveChangesAsync() > 0;

            return true;
        }
        #endregion

        #region 获取数据
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Db.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> QueryListAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await Db.Set<T>().Where(whereLambda).ToListAsync();
        }

        public virtual async Task<T> QueryAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await Db.Set<T>().Where(whereLambda).SingleOrDefaultAsync();
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await Db.Set<T>().Where(whereLambda).AnyAsync();
        }

        public virtual async Task<IEnumerable<T>> QueryPageListAsync<S>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc)
        {
            if (isAsc)
            {
                return
                await Db.Set<T>()
                  .Where(whereLambda)
                  .OrderBy(orderbyLambda)
                  .Skip(pageSize * (pageIndex - 1))
                  .Take(pageSize)
                  .ToListAsync();
            }
            else
            {
                return
                await Db.Set<T>()
                 .Where(whereLambda)
                 .OrderByDescending(orderbyLambda)
                 .Skip(pageSize * (pageIndex - 1))
                 .Take(pageSize)
                 .ToListAsync();
            }
        }
        #endregion

        #region 保存
        public virtual async Task<bool> SaveAsync()
        {
            return await Db.SaveChangesAsync() > 0;
        }
        #endregion
    }
}
