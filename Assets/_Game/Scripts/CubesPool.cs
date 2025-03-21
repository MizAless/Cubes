using System.Collections.Generic;

namespace _Game.Scripts
{
    public class CubesPool
    {
        private readonly CubesFabric _cubesFabric;

        private Queue<Cube> _cubesQueue = new Queue<Cube>();

        public CubesPool(CubesFabric cubesFabric)
        {
            _cubesFabric = cubesFabric;
        }

        public Cube Get()
        {
            if (_cubesQueue.Count == 0) 
                return _cubesFabric.Create();
            
            var cube = _cubesQueue.Dequeue();
            cube.gameObject.SetActive(true);
            return cube;
        }

        public void Release(Cube cube)
        {
            cube.gameObject.SetActive(false);
            _cubesQueue.Enqueue(cube);
        }
    }
}