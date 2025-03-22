using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class CubesTower
    {
        private Transform _lowerPoint;

        private Stack<Cube> _cubesStack = new Stack<Cube>();
        
        private Vector3 _offset;

        public CubesTower(Transform lowerPoint)
        {
            _lowerPoint = lowerPoint;
        }

        public void Push(Cube cube)
        {
            _cubesStack.Push(cube);
            PlaceNewCube();
        }

        private void PlaceNewCube()
        {
            var upperCube = _cubesStack.Peek();

            var stackCount = _cubesStack.Count;

            _offset = -Vector3.up * upperCube.Height / 2f;
            upperCube.transform.position = _lowerPoint.position + _offset + _lowerPoint.up * (upperCube.Height * stackCount);
        }
    }
}