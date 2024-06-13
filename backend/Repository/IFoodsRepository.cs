using WebScrapping_C.Model;

namespace WebScrapping_C.Repository
{
    public interface IFoodsRepository
    {
        Task<bool> HasItemsAsync();
        Task CreateItemAsync(Item item);
        Task<bool> SaveChangesAsync();
    }
}