using System.Threading.Tasks;

namespace Vega.core
{
    public interface IunitofWork
    {
         Task CompleteAsync();
    }
}