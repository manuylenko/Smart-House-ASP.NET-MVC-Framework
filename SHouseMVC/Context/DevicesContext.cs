using System.Data.Entity;

namespace SHouseMVC
{
    public class DevicesContext : DbContext
    {
        public DbSet<Illuminator> Illuminators { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<Fridge> Fridges { get; set; }
        public DbSet<Tv> Tvs { get; set; }
        public DbSet<Microwave> Microwaves { get; set; }
        public DbSet<DeepFreeze> DeepFreezes { get; set; }
    }
}