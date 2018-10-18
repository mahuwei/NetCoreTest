using Microsoft.EntityFrameworkCore;

namespace Project.Infrastructure {
    public class KkdRepository<TEntity> : Repository<TEntity> where TEntity : class {
        public KkdRepository(DbContext dc) : base(dc) { }
    }
}