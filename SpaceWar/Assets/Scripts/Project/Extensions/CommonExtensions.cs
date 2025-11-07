using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ILogger = Project.Infrastructure.Logger.ILogger;

namespace Project.Extensions
{
    public static class CommonExtensions
    {
        public static void Log(this ILogger logger, string message, Color color = default)
        {
            if (!logger.IsActiveLogger)
                return;

            color = color == default ? logger.DefaultColor : color;
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{logger.GetName()}:</color> {message}");
        }
        
        public static T With<T>(this T source, Action<T> action)
        {
            action.Invoke(source);
            return source;
        }

        public static string ToJson<TClass>(this TClass source) where TClass : class => JsonUtility.ToJson(source);
        public static TClass FromJson<TClass>(this string source) => JsonUtility.FromJson<TClass>(source);

        public static T GetRandomElement<T>(this List<T> list) => list.ElementAt(UnityEngine.Random.Range(0, list.Count));
    }
}