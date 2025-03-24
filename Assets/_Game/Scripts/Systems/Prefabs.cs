using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public static class Prefabs
    {
        private static readonly Dictionary<Type, string> _prefabs = new Dictionary<Type, string>()
        {
            { typeof(Cube), nameof(Cube) },
            { typeof(UICubeView), nameof(UICubeView) },
        };
        
        public static T Load<T>()
            where T : MonoBehaviour
        {
            return Resources.Load<T>($"Prefabs/{_prefabs[typeof(T)]}");
        }
    }
}