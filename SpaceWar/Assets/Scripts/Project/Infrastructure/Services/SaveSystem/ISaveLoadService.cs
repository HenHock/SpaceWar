using Project.Infrastructure.Services.SaveSystem.SaveHandler;

namespace Project.Infrastructure.Services.SaveSystem
{
    public interface ISaveLoadService
    {
        void Save();
        void Load();
        void Cleanup();
        void Register(ISaveHandler saveHandler);
        void InformProgressReader();
        void ResetProgress();
    }
}