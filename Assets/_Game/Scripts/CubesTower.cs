using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts
{
    public class CubesTower
    {
        private const float CubeFallDuration = 0.5f;
        private const float CubeFallHeight = 2f;

        private readonly Transform _lowerPoint;
        private readonly BoxCollider2D _boxCollider2D;
        private readonly CatchArea _layoutCatchArea;
        private readonly Camera _mainCamera;
        private readonly List<Cube> _cubesList = new List<Cube>();

        private Vector3 _widthOffset = Vector3.zero;
        private Vector3 _heightOffset;

        public CubesTower(Transform lowerPoint, BoxCollider2D boxCollider2D, CatchArea layoutCatchArea,
            Camera mainCamera)
        {
            _lowerPoint = lowerPoint;
            _boxCollider2D = boxCollider2D;
            _layoutCatchArea = layoutCatchArea;
            _mainCamera = mainCamera;
        }

        public IReadOnlyList<Cube> CubesList => _cubesList;

        public event Action CubeAdded;
        public event Action CubeRemoved;
        public event Action TowerFulled;

        public void AddCube(Cube cube)
        {
            if (_cubesList.Count == 0)
            {
                _lowerPoint.position = _lowerPoint.position.WithX(cube.transform.position.x);
            }
            else
            {
                var upperCube = _cubesList.Last();

                var directionFromPivotToUpperCubeOnHorizontalAxis =
                    upperCube.transform.position.WithY(0) -
                    _lowerPoint.transform.position.WithY(0);

                _widthOffset = directionFromPivotToUpperCubeOnHorizontalAxis +
                               Vector3.right * Random.Range(-upperCube.Width / 2f, upperCube.Width / 2f);
            }

            _cubesList.Add(cube);

            _heightOffset = -Vector3.up * cube.Height / 2f;

            cube.transform.position = _lowerPoint.position + _heightOffset + _widthOffset +
                                      _lowerPoint.up * (cube.Height * _cubesList.Count);

            GameMath.ClampPositionRegardingObject(cube.transform, cube.Width, _layoutCatchArea.transform);

            cube.transform.parent = _lowerPoint;
            CubeAdded?.Invoke();

            cube.AnimateFall(CubeFallHeight, CubeFallDuration);
            UpdateCollider();
        }

        public void RemoveCube(Cube cube)
        {
            if (!_cubesList.Contains(cube))
                throw new ArgumentException("The cube does not exist in the tower.");

            int cubeIndex = _cubesList.IndexOf(cube);

            _cubesList.RemoveAt(cubeIndex);
            CubeRemoved?.Invoke();

            for (int i = cubeIndex; i < _cubesList.Count; i++)
            {
                var fallingCube = _cubesList[i];

                fallingCube.transform.position =
                    fallingCube.transform.position.WithY(fallingCube.transform.position.y - fallingCube.Width);

                if (i > 0)
                {
                    var downCube = _cubesList[i - 1];
                    GameMath.ClampPositionRegardingObject(fallingCube.transform, 0, downCube.transform);
                }
            }

            CubeRemoved?.Invoke();
            UpdateCollider();

            for (int i = cubeIndex; i < _cubesList.Count; i++)
            {
                _cubesList[i].AnimateFall(CubeFallHeight, CubeFallDuration);
            }
        }

        public bool Contains(Cube cube)
        {
            return _cubesList.Contains(cube);
        }

        public bool CanAddCube()
        {
            if (_cubesList.Count == 0)
                return true;

            var upperCube = _cubesList.Last();

            var canAdd = _mainCamera.WorldToViewportPoint(upperCube.transform.position).y < 1;

            if (!canAdd)
                TowerFulled?.Invoke();

            return canAdd;
        }

        public void AddLoadedCube(Cube cube)
        {
            _cubesList.Add(cube);
            cube.transform.parent = _lowerPoint;
            UpdateCollider();
        }

        private static void AnimateFall(Cube cube)
        {
            var endPoint = cube.transform.position;
            var startPoint = cube.transform.position + Vector3.up * CubeFallHeight;

            cube.transform.position = startPoint;

            cube.transform.DOMove(endPoint, CubeFallDuration)
                .SetEase(Ease.InSine);
        }

        private void UpdateCollider()
        {
            if (_cubesList.Count == 0)
            {
                _boxCollider2D.size = Vector2.zero;
                return;
            }

            float height = _cubesList.Count * _cubesList[0].Height * 2;
            float width = _cubesList[0].Width;

            float minWidthOffset = float.MaxValue;
            float maxWidthOffset = float.MinValue;

            foreach (var cube in _cubesList)
            {
                if (cube.transform.localPosition.x < minWidthOffset)
                    minWidthOffset = cube.transform.localPosition.x;

                if (cube.transform.localPosition.x > maxWidthOffset)
                    maxWidthOffset = cube.transform.localPosition.x;
            }

            _boxCollider2D.offset = new Vector2((maxWidthOffset + minWidthOffset) / 2f, height / 2f);
            _boxCollider2D.size = new Vector2(maxWidthOffset - minWidthOffset + width, height);
        }
    }
}