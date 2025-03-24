using System;

namespace _Game.Scripts
{
    public class CubePlacementManager : IDisposable
    {
        private readonly CubeDragger _cubeDragger;
        private readonly CatchArea _towerLayoutCatchArea;
        private readonly CatchArea _towerCatchArea;
        private readonly CatchArea _holeLayoutCatchArea;
        private readonly CubesTower _cubesTower;
        private readonly CubesHole _cubesHole;

        public CubePlacementManager(
            CubeDragger cubeDragger,
            CatchArea towerLayoutCatchArea,
            CatchArea towerCatchArea,
            CatchArea holeLayoutCatchArea,
            CubesTower cubesTower,
            CubesHole cubesHole)
        {
            _cubeDragger = cubeDragger;
            _towerLayoutCatchArea = towerLayoutCatchArea;
            _towerCatchArea = towerCatchArea;
            _holeLayoutCatchArea = holeLayoutCatchArea;
            _cubesTower = cubesTower;
            _cubesHole = cubesHole;

            _cubeDragger.CubeDragged += OnCubeDragged;
        }
        
        public event Action CubeMissed;

        public void Dispose()
        {
            _cubeDragger.CubeDragged -= OnCubeDragged;
        }

        private void OnCubeDragged(Cube cube)
        {
            if (_towerLayoutCatchArea.Intersects(cube))
                ChooseTowerAction(cube);
            else if (_holeLayoutCatchArea.Intersects(cube))
                ChooseHoleAction(cube);
            else
                cube.Destroy();
        }

        private void ChooseTowerAction(Cube cube)
        {
            if (_cubesTower.CubesList.Count == 0)
            {
                _cubesTower.AddCube(cube);
                return;
            }

            if (_cubesTower.Contains(cube))
            {
                _cubeDragger.CancelDragging();
                return;
            }

            if (!_cubesTower.CanAddCube())
            {
                cube.Destroy();
                return;
            }

            if (_towerCatchArea.Intersects(cube))
            {
                _cubesTower.AddCube(cube);
                return;
            }

            cube.DestroyWithAnimation();
            CubeMissed?.Invoke();
        }

        private void ChooseHoleAction(Cube cube)
        {
            if (_cubesTower.Contains(cube))
                _cubesTower.RemoveCube(cube);

            _cubesHole.DropCube(cube);
        }
    }
}