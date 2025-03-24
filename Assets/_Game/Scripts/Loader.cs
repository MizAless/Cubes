using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Game.Scripts
{
    public class Loader
    {
        private readonly CubesTower _cubesTower;
        private readonly CubeSpawner _cubeSpawner;
        private readonly string _savePath;

        public Loader(CubesTower cubesTower, CubeSpawner cubeSpawner, string savePath)
        {
            _cubesTower = cubesTower;
            _cubeSpawner = cubeSpawner;
            _savePath = savePath;
        }

        public void LoadData()
        {
            if (!File.Exists(_savePath))
            {
                Debug.LogWarning("Файл сохранения не найден");
                return;
            }

            string json = File.ReadAllText(_savePath);
            var dataWrapper = JsonUtility.FromJson<CubeSaveDataWrapper>(json);

            if (dataWrapper == null || dataWrapper.Cubes == null)
            {
                Debug.LogError("Ошибка загрузки данных");
                return;
            }

            foreach (var cubeData in dataWrapper.Cubes)
            {
                var cube = _cubeSpawner.Spawn(cubeData.Position, cubeData.Color);
                _cubesTower.AddLoadedCube(cube);
            }
        }
    }
}