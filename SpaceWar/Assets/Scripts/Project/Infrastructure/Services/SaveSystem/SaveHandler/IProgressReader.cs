using Project.Infrastructure.Services.SaveSystem.Data;

namespace Project.Infrastructure.Services.SaveSystem.SaveHandler
{
    public interface IProgressReader
    {
        void LoadProgress(GameProgress progress);
    }
}