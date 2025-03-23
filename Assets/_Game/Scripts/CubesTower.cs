using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts
{
    public class CubesTower
    {
        private readonly Transform _lowerPoint;
        private readonly BoxCollider2D _boxCollider2D;
        private readonly Stack<Cube> _cubesStack = new Stack<Cube>();
        private readonly CatchArea _layoutCatchArea;

        private Vector3 _widthOffset = Vector3.zero;
        private Vector3 _heightOffset;

        public CubesTower(Transform lowerPoint, BoxCollider2D boxCollider2D, CatchArea layoutCatchArea)
        {
            _lowerPoint = lowerPoint;
            _boxCollider2D = boxCollider2D;
            _layoutCatchArea = layoutCatchArea;
        }

        public void AddCube(Cube cube)
        {
            if (_cubesStack.Count == 0)
            {
                _lowerPoint.position = NewVectorWithX(_lowerPoint.position, cube.transform.position.x);
            }
            else
            {
                var upperCube = _cubesStack.Peek();

                var directionFromPivotToUpperCubeOnHorizontalAxis =
                    NewVectorWithY(upperCube.transform.position, 0) -
                    NewVectorWithY(_lowerPoint.transform.position, 0);

                _widthOffset = directionFromPivotToUpperCubeOnHorizontalAxis +
                               Vector3.right * Random.Range(-upperCube.Width / 2f, upperCube.Width / 2f);
            }

            _cubesStack.Push(cube);

            _heightOffset = -Vector3.up * cube.Height / 2f;

            var maxWidthOffset = (_layoutCatchArea.Width - cube.Width) / 2f / _layoutCatchArea.Width;

            cube.transform.position = _lowerPoint.position + _heightOffset + _widthOffset +
                                      _lowerPoint.up * (cube.Height * _cubesStack.Count);

            Vector3 localPositionInCatchArea =
                _layoutCatchArea.transform.InverseTransformPoint(cube.transform.position);
            localPositionInCatchArea.x = Mathf.Clamp(localPositionInCatchArea.x, -maxWidthOffset, maxWidthOffset);
            cube.transform.position = _layoutCatchArea.transform.TransformPoint(localPositionInCatchArea);
            cube.transform.parent = _lowerPoint;

            UpdateCollider();
        }

        private void UpdateCollider()
        {
            float height = _cubesStack.Count * _cubesStack.Peek().Height * 2;
            float width = _cubesStack.Peek().Width;

            float minWidthOffest = float.MaxValue;
            float maxWidthOffest = float.MinValue;

            foreach (var cube in _cubesStack)
            {
                if (cube.transform.localPosition.x < minWidthOffest)
                    minWidthOffest = cube.transform.localPosition.x;

                if (cube.transform.localPosition.x > maxWidthOffest)
                    maxWidthOffest = cube.transform.localPosition.x;
            }

            _boxCollider2D.offset = new Vector2((maxWidthOffest + minWidthOffest) / 2f, height / 2f);
            _boxCollider2D.size = new Vector2(maxWidthOffest - minWidthOffest + width, height);
        }

        private Vector3 NewVectorWithX(Vector3 origin, float x)
        {
            return new Vector3(x, origin.y, origin.z);
        }

        private Vector3 NewVectorWithY(Vector3 origin, float y)
        {
            return new Vector3(origin.x, y, origin.z);
        }
    }
}