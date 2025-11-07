using Project.Infrastructure.Services.SaveSystem.Data;

namespace Project.Infrastructure.Services.SaveSystem.PersistentProgressService
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public GameProgress Progress { get; set; }
    }
}