using UnityEngine;

namespace _Game.Scripts
{
    public class CubesFabric
    {
        private readonly Cube _cubePrefab;

        public CubesFabric(Cube cubePrefab)
        {
            _cubePrefab = cubePrefab;
        }

        public Cube Create()
        {
            return Object.Instantiate(_cubePrefab);
        }
    }
}