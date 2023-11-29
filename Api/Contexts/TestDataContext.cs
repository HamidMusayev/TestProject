using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Contexts
{
    public class TestDataContext : DbContext
    {
        public TestDataContext(DbContextOptions<TestDataContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
    }
}
