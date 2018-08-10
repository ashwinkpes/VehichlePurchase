using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.core;
using Vega.core.Models;
using Vega.extensions;
using Vega.Models;

namespace Vega.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext dbContext;
        public VehicleRepository(VegaDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public async Task<QueryResult<Vehicle>> GetVehicles(VehichleQuery queryObj)
        {
            var result = new QueryResult<Vehicle>();
            
            var query =  dbContext.vehichles
            .Include( V=> V.Model)
            .ThenInclude(m=>m.Make)
            .Include(V=>V.Features)
            .ThenInclude(vf => vf.Feature).AsQueryable();

            if (queryObj.MakeId.HasValue)
            {
                query = query.Where(s => s.Model.MakeId == queryObj.MakeId);
            }

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName               
            };

           query = query.ApplyOrdering(queryObj, columnsMap);

           result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);
           
            result.Items = await query.ToListAsync();
            return result; 
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
           if (!includeRelated)
           {
               return await dbContext.vehichles.FindAsync(id);
           }
           return await dbContext.vehichles.
            Include(v => v.Features)
            .ThenInclude(vf => vf.Feature)
            .Include(v => v.Model)
            .ThenInclude(vm => vm.Make)
            .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Add (Vehicle vehichle)
        {
          dbContext.vehichles.Add(vehichle);
        }

         public void Remove (Vehicle vehichle)
        {
          dbContext.vehichles.Remove(vehichle);
        }

        public async Task<Model> FindModelById(int id)
        {
          return await dbContext.Models.FindAsync(id);
        }
    }
}