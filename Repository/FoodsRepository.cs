using WebScrapping_C.Data;
using WebScrapping_C.Model;
using Microsoft.EntityFrameworkCore;

namespace WebScrapping_C.Repository
{
    public class FoodsRepository : IFoodsRepository
    {
        private readonly FoodsContex context;

        public FoodsRepository(FoodsContex context)
        {
            this.context = context;
        }

        public async Task CreateItemAsync(Item item)
        {
            await this.context.AddAsync(item);
        }

        public async Task<bool> HasItemsAsync()
        {
            return await this.context.Items.AnyAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}