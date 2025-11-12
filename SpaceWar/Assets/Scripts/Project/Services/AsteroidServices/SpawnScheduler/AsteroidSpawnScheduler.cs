using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Extensions;
using Project.Infrastructure.BootStateMachine;
using Project.Infrastructure.Logger;
using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Logic.Asteroid;
using Project.Logic.Asteroid.Data;
using Project.Services.AsteroidServices.Factory;
using Project.Services.LevelServices.LevelChanger.Model;
using Project.Services.LevelServices.LevelSettingsProvider;
using Project.Services.LevelServices.LevelSettingsProvider.Data;
using UniRx;
using Zenject;

namespace Project.Services.AsteroidServices.SpawnScheduler
{
    public class AsteroidSpawnScheduler : IInitializable, ILogger
    {
        public bool IsActiveLogger => true;
        public Color DefaultColor => Color.CadetBlue;
        
        private static readonly AsteroidType[] PossibleValuesArray = (AsteroidType[])Enum.GetValues(typeof(AsteroidType));
        
        private readonly LevelConfig _levelConfig;
        private readonly IAsteroidFactory _factory;
        private readonly LevelSettings _levelSettings;
        private readonly CancellationToken _stopSpawnToken;
        private readonly IGameStateMachine _gameStateMachine;

        private int _spawnedAsteroidsCount = 0;

        public AsteroidSpawnScheduler
        (
            IAsteroidFactory factory,
            ILevelSetupModel levelSetupModel,
            CancellationToken stopSpawnToken,
            IGameStateMachine gameStateMachine,
            IReadAssetContainer assetContainer,
            ILevelSettingsProvider levelSettingsProvider
        )
        {
            _factory = factory;
            _stopSpawnToken = stopSpawnToken;
            _gameStateMachine = gameStateMachine;
            _levelSettings = levelSettingsProvider.GetLevelSettings(levelSetupModel.LevelIndex);
            _levelConfig = assetContainer.GetConfig<LevelsConfig>().GetLevelConfig(levelSetupModel.LevelIndex);
        }

        public void Initialize()
        {
            Observable.EveryUpdate()
                .ThrottleFirst(TimeSpan.FromSeconds(_levelSettings.AsteroidSpawnInterval))
                .Subscribe(SpawnAsteroid)
                .AddTo(_stopSpawnToken);
            
            this.Log($"Initialized asteroid spawn scheduler.");
        }

        private void SpawnAsteroid(long _)
        {
            if (_spawnedAsteroidsCount++ < _levelSettings.AsteroidSpawnCount)
            {
                AsteroidType mask = _levelConfig.AsteroidTypes;
                AsteroidType rndAsteroid = GetRandomAsteroidType(mask, PossibleValuesArray);

                _factory.SpawnAsteroid(rndAsteroid);
            }
            else if (IsAllAsteroidsReleased()) 
                GoToNextGameState();
        }

        private void GoToNextGameState() => _gameStateMachine.CurrentState.Value.Next();
        private bool IsAllAsteroidsReleased() => _factory.IsAllAsteroidsInPool;

        /// <summary>
        /// Get random asteroid type from level config mask if enum has [Flags]
        /// </summary>
        private static AsteroidType GetRandomAsteroidType(AsteroidType mask, AsteroidType[] valuesArray)
        {
            int raw = (int)mask;
            
            // If nothing was selected
            if (raw == 0)
                return mask;
            
            // If single option selected
            if ((raw & (raw - 1)) == 0)
                return mask;

            // Build candidate list from bits present in mask (avoid Enum reflection entirely)
            var candidates = new List<AsteroidType>(valuesArray.Length);
            foreach (var candidate in valuesArray)
            {
                if ((mask & candidate) == candidate) 
                    candidates.Add(candidate);
            }

            if (!candidates.Any())
                return mask;

            int index = UnityEngine.Random.Range(0, candidates.Count);
            return candidates[index];
        }
    }
}