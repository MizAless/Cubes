using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class CubeSpawner
    {
        private readonly CubesPool _cubesPool;

        public CubeSpawner(CubesPool cubesPool)
        {
            _cubesPool = cubesPool;
        }

        public event Action<Cube> Spawned;

        public Cube Spawn(Vector3 position, Color color)
        {
            var cube = _cubesPool.Get();
            cube.Init(color);
            cube.SetPosition(position);

            cube.Destroyed += OnCubeDestroyed;

            Spawned?.Invoke(cube);
            
            return cube;
        }

        private void OnCubeDestroyed(Cube cube)
        {
            cube.Destroyed -= OnCubeDestroyed;
            _cubesPool.Release(cube);
        }
    }
}