using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.core;
using Vega.Models;

namespace Vega.Persistence
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly VegaDbContext dbContext;
        public PhotoRepository(VegaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehichleId)
        {
          return  await dbContext.Photos.Where(s => s.VehichleId == vehichleId).ToListAsync();
        }
    }
}