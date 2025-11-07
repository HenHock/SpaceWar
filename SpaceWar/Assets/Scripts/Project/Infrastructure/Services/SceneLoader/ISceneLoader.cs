using System;

namespace Project.Infrastructure.Services.SceneLoader
{
    public interface ISceneLoader
    {
        void Load(int sceneID, Action onLoadedAction = null);
    }
}