using System.Collections.Generic;
using Project.Infrastructure.Services.SaveSystem.Data;
using Project.Infrastructure.Services.SaveSystem.PersistentProgressService;
using Project.Infrastructure.Services.SaveSystem.SaveHandler;
using UnityEngine;
using ILogger = Project.Infrastructure.Logger.ILogger;
using System.IO;
using Project.Extensions;
using Project.Infrastructure.Services.SaveSystem.StaticData;

namespace Project.Infrastructure.Services.SaveSystem
{
    public class SaveLoadService : ISaveLoadService, ILogger
    {
        

        private readonly List<IProgressWriter> _progressWriters = new();
        private readonly List<IProgressReader> _progressReaders = new();
        
        private readonly ProgressFileReaderWriter _progressFile;
        private readonly IPersistentProgressService _progressService;

        public SaveLoadService(IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _progressFile = new ProgressFileReaderWriter(SaveStaticData.ProgressFilePath);
        }

        public void Register(ISaveHandler saveHandler)
        {
            _progressReaders.Add(saveHandler);
            _progressWriters.Add(saveHandler);
        }

        public void Save()
        {
            ProgressWriterUpdates();
            
            _progressFile.Write(_progressService.Progress);
        }

        public void Load()
        {
            GameProgress loaded = _progressFile.Read();

            if (loaded == null)
            {
                _progressService.Progress = new GameProgress();
                this.Log("Initialized new progress data");
            }
            else
            {
                _progressService.Progress = loaded;
            }
        }


        public void Cleanup()
        {
            _progressReaders.Clear();
            _progressWriters.Clear();
        }

        public void ResetProgress()
        {
            _progressService.Progress = new GameProgress();
            _progressFile.Delete();
        }

        public void InformProgressReader()
        {
            foreach (IProgressReader progressReader in _progressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private void ProgressWriterUpdates()
        {
            foreach (var progressWriter in _progressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);
        }
    }
}