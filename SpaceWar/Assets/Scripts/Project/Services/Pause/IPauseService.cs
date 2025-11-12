namespace Project.Services.Pause
{
    public interface IPauseService
    {
        void SetPause(bool isPaused);
        bool IsPaused { get; }
    }
}