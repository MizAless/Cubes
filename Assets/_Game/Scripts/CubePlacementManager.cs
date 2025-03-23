using System;

namespace _Game.Scripts
{
    public class CubePlacementManager : IDisposable
    {
        private readonly CubeDragger _cubeDragger;
        private readonly CatchArea _towerLayoutCatchArea;
        private readonly CatchArea _towerCatchArea;
        private readonly CubesTower _cubesTower;

        public CubePlacementManager(CubeDragger cubeDragger, CatchArea towerLayoutCatchArea, CatchArea towerCatchArea, CubesTower cubesTower)
        {
            _cubeDragger = cubeDragger;
            _towerLayoutCatchArea = towerLayoutCatchArea;
            _towerCatchArea = towerCatchArea;
            _cubesTower = cubesTower;

            _cubeDragger.CubeDragged += OnCubeDragged;
        }

        public void Dispose()
        {
            _cubeDragger.CubeDragged -= OnCubeDragged;
        }

        private void OnCubeDragged(Cube cube)
        {
            if (_towerLayoutCatchArea.Intersects(cube))
                _cubesTower.Push(cube);
            else
                cube.Destroy();
        }
    }
}