using System;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Services.SceneLoader
{
    public interface ISceneLoader
    {
        void Load(GameScene sceneID, Action onLoadedAction = null);
    }
}