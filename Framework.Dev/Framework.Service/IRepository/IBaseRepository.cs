using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Service.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        #region 添加数据
        Task<bool> AddAsync(T entity, bool autoSave = false);
        Task<bool> AddRangeAsync(List<T> entities, bool autoSave = false);
        #endregion

        #region 修改数据
        Task<bool> UpdateAsync(T entity, bool autoSave = false);
        #endregion

        #region 删除数据
        Task<bool> DeleteAsync(T entity, bool autoSave = false);

        Task<bool> BatchDeleteAsync(List<T> entities, bool autoSave = false);

        Task<bool> BatchDeleteAsync(bool autoSave = false, params int[] ids);
        #endregion

        #region 获取数据
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> QueryListAsync(Expression<Func<T, bool>> whereLambda);

        Task<T> QueryAsync(Expression<Func<T, bool>> whereLambda);


        Task<bool> ExistsAsync(Expression<Func<T, bool>> whereLambda);

        Task<IEnumerable<T>> QueryPageListAsync<S>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc);
        #endregion

        #region 保存
        Task<bool> SaveAsync();
        #endregion
    }
}
