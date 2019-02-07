using System.Threading.Tasks;

namespace VegaSPA.Data
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}