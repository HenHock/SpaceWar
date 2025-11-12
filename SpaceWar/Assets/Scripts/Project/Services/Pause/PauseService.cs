using System;
using Project.Extensions;
using UnityEngine;
using ILogger = Project.Infrastructure.Logger.ILogger;

namespace Project.Services.Pause
{
    public class PauseService : IPauseService, IDisposable, ILogger
    {
        public bool IsActiveLogger => true;
        public Color DefaultColor => Color.orangeRed;
        
        public bool IsPaused => Time.timeScale == 0f;
        
        public void SetPause(bool isPaused)
        {
            if (isPaused != IsPaused)
            {
                Time.timeScale = isPaused ? 0f : 1f;
                this.Log("Game set to " + (isPaused ? "PAUSED" : "RESUMED"));
            }
        }

        public void Dispose()
        {
            SetPause(false);
        }
    }
}