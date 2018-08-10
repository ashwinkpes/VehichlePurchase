using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.core.Models;
using Vega.Models;

namespace Vega.core
{
    public interface IVehicleRepository
    {
         Task<Vehicle> GetVehicle(int id,bool includeRelated = true);
         void Add (Vehicle vehichle);

         void Remove (Vehicle vehichle);

         Task<Model> FindModelById(int id);

         Task<QueryResult<Vehicle>> GetVehicles(VehichleQuery queryObj);
    }
}