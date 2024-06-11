using WebScrapping_C.Model;

namespace WebScrapping_C.Repository
{
    public interface IFoodsRepository
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task CreateItemAsync(Item item);
        Task<bool> SaveChangesAsync();
    }
}

