using Microsoft.EntityFrameworkCore;
using WebScrapping_C.Model;

namespace WebScrapping_C.Data
{
    public class FoodsContex : DbContext
    {

        public DbSet<Item> Items { get; set; }
        public DbSet<Properties> Details { get; set; }

        public FoodsContex(DbContextOptions<FoodsContex> options) : base(options)
        {
            Items = Set<Item>();
            Details = Set<Properties>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var item = modelBuilder.Entity<Item>();
            item.ToTable("items");
            item.HasKey(i => i.Id);
            item.Property(i => i.Id).HasColumnName("id").ValueGeneratedOnAdd();
            item.Property(i => i.Name).IsRequired(false).HasColumnName("name");
            item.Property(i => i.Group).IsRequired(false).HasColumnName("group");
            item.Property(i => i.Code).IsRequired(false).HasColumnName("code");
            item.Property(i => i.ScientificName).IsRequired(false).HasColumnName("scientific_name");
            item.Property(i => i.Brand).IsRequired(false).HasColumnName("brand");


            var properties = modelBuilder.Entity<Properties>();
            properties.ToTable("properties");
            properties.HasKey(i => i.Id);
            properties.Property(i => i.Id).HasColumnName("id").ValueGeneratedOnAdd();
            properties.Property(i => i.Component).HasColumnName("component");
            properties.Property(i => i.Units).IsRequired(false).HasColumnName("units");
            properties.Property(i => i.ValuePer100G).IsRequired(false).HasColumnName("value_per_100_g");
            properties.Property(i => i.DataType).IsRequired(false).HasColumnName("data_type");
            properties.Property(i => i.MinimumValue).IsRequired(false).HasColumnName("minimum_value");
            properties.Property(i => i.MaximumValue).IsRequired(false).HasColumnName("maximum_value");
            properties.Property(i => i.NumberOfDataUsed).IsRequired(false).HasColumnName("number_of_data_used");
            properties.Property(i => i.StandardDeviation).IsRequired(false).HasColumnName("standard_daviation");
            properties.Property(i => i.References).IsRequired(false).HasColumnName("references");

            modelBuilder.Entity<Item>()
                .HasMany(i => i.Details)
                .WithOne(p => p.Item)
                .HasForeignKey(p => p.ItemId);
        }
    }
}
