using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Cube : MonoBehaviour
    {
        private Color _color;
        private SpriteRenderer _spriteRenderer;

        public event Action<Cube> Destroyed;

        public float Height { get; private set; }
        
        private void Awake()
        {
            Height = transform.localScale.y;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(Color color)
        {
            _color = color;
            _spriteRenderer.color = color;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Destroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}