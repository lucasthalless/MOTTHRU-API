using Microsoft.EntityFrameworkCore;
using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Infrastructure.Data.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }
        public DbSet<MotoEntity> Moto { get; set; }
        public DbSet<PatioEntity> Patio { get; set; }
        public DbSet<RfidEntity> Rfid { get; set; }
    }    
}