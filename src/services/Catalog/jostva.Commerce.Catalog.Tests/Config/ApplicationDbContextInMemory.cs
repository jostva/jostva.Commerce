using jostva.Commerce.Catalog.Data;
using Microsoft.EntityFrameworkCore;

namespace jostva.Commerce.Catalog.Tests.Config
{
    public static class ApplicationDbContextInMemory
    {
        public static ApplicationDBContext Get()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                                .UseInMemoryDatabase(databaseName: "Catalog.Db")
                                .Options;

            return new ApplicationDBContext(options);
        }
    }
}