using Strazacy.Models;

namespace Strazacy.Repository
{
    public interface IFirefightersRepo
    {
        public abstract Task<MAction> GetActionInfoAsync(int IdAction);
        public abstract Task<MAction> DeleteActionAsync(int IdAction);
    }
}
