using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Models;

namespace Vega.core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehichleId);
    }
}