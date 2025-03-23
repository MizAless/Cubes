using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class CatchArea : MonoBehaviour
    {
        private BoxCollider2D _catchTrigger;

        private List<Cube> _cubes = new List<Cube>();

        private void Awake()
        {
            _catchTrigger = GetComponent<BoxCollider2D>();
        }

        public BoxCollider2D Collider { get; private set; }

        public bool Intersects(Cube cube)
        {
            return _cubes.Contains(cube);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Cube cube))
            {
                cube.Destroyed += Remove;
                _cubes.Add(cube);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Cube cube))
            {
                Remove(cube);
            }
        }

        private void Remove(Cube cube)
        {
            cube.Destroyed -= Remove;
            _cubes.Remove(cube);
        }
    }
}