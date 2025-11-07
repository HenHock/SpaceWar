using System;
using UnityEngine;

namespace Project.Infrastructure.Logger
{
    public interface ILogger
    {
        public bool IsActiveLogger => true;
        public Color DefaultColor => Color.magenta;

        public string GetName() => GetType().Name;
    }
}