using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Game.Scripts
{
    public class Saver
    {
        private readonly CubesTower _cubesTower;
        private readonly string _savePath;

        public Saver(CubesTower cubesTower, string savePath)
        {
            _cubesTower = cubesTower;
            // _savePath = Path.Combine(Application.persistentDataPath, "cubes_save.json");
            _savePath = savePath;

            _cubesTower.CubeAdded += SaveData;
            _cubesTower.CubeRemoved += SaveData;
        }

        private void SaveData()
        {
            var saveData = new List<CubeSaveData>();

            foreach (var cube in _cubesTower.CubesList)
            {
                saveData.Add(new CubeSaveData()
                {
                    Position = cube.transform.position,
                    Color = cube.Color
                });
            }

            string json = JsonUtility.ToJson(new CubeSaveDataWrapper { Cubes = saveData }, true);
            File.WriteAllText(_savePath, json);
        }
    }
}