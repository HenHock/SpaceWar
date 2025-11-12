using Project.Infrastructure.Services.Windows.Data;

namespace Project.Infrastructure.Services.Windows
{
    public interface IWindowService
    {
        void Open(WindowType window);
    }
}