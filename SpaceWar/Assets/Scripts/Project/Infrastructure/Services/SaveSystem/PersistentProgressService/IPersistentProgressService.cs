using Project.Infrastructure.Services.SaveSystem.Data;

namespace Project.Infrastructure.Services.SaveSystem.PersistentProgressService
{
    public interface IPersistentProgressService
    {
        GameProgress Progress { get; set; }
    }
}