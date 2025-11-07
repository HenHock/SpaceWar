using Project.Infrastructure.Services.SaveSystem.Data;

namespace Project.Infrastructure.Services.SaveSystem.SaveHandler
{
    public interface IProgressWriter : IProgressReader
    {
        void UpdateProgress(GameProgress progress);
    }
}