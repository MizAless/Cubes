using System;

namespace _Game.Scripts
{
    public class CubePlacementManager : IDisposable
    {
        private readonly CubeDragger _cubeDragger;
        private readonly CatchArea _towerLayoutCatchArea;
        private readonly CatchArea _towerCatchArea;
        private readonly CatchArea _holeLayoutCatchArea;
        private readonly CatchArea _holeCatchArea;
        private readonly CubesTower _cubesTower;
        private readonly CubesHole _cubesHole;

        public CubePlacementManager(
            CubeDragger cubeDragger,
            CatchArea towerLayoutCatchArea, 
            CatchArea towerCatchArea, 
            CatchArea holeLayoutCatchArea, 
            CatchArea holeCatchArea, 
            CubesTower cubesTower,
            CubesHole cubesHole)
        {
            _cubeDragger = cubeDragger;
            _towerLayoutCatchArea = towerLayoutCatchArea;
            _towerCatchArea = towerCatchArea;
            _holeLayoutCatchArea = holeLayoutCatchArea;
            _holeCatchArea = holeCatchArea;
            _cubesTower = cubesTower;
            _cubesHole = cubesHole;

            _cubeDragger.CubeDragged += OnCubeDragged;
        }

        public void Dispose()
        {
            _cubeDragger.CubeDragged -= OnCubeDragged;
        }

        private void OnCubeDragged(Cube cube)
        {
            if (_towerLayoutCatchArea.Intersects(cube))
            {
                if (!_cubesTower.CanAddCube())
                    cube.Destroy();
                else if (!_cubesTower.Contains(cube))
                    _cubesTower.AddCube(cube);
                else
                    _cubeDragger.CancelDragging();
            }
            else if (_holeLayoutCatchArea.Intersects(cube))
            {
                if (_cubesTower.Contains(cube))
                    _cubesTower.PoolOutCube(cube);
                    
                _cubesHole.DropCube(cube);
            }
            else
                cube.Destroy();
        }
    }
}