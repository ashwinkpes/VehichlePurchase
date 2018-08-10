using System.Threading.Tasks;
using Vega.core;

namespace Vega.Persistence
{
    public class UnitOfwork : IunitofWork
    {
        private readonly VegaDbContext dbContext;
        public UnitOfwork(VegaDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public async Task CompleteAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}