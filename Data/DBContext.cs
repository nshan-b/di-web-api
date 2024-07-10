using Microsoft.EntityFrameworkCore;
using di_web_api.Models;

namespace di_web_api.Data {
    public class DBContext : DbContext {
        public DBContext(DbContextOptions<DBContext> options) : base(options) {
        }

        public DbSet<DocumentData> DocumentData { get; set; }
        public DbSet<DocumentStatus> DocumentStatus { get; set; }
    }
}
