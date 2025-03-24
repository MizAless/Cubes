using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Cube : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _overlaySpriteRenderer;
        
        private Color _color;
        private SpriteRenderer _spriteRenderer;

        public event Action<Cube> Destroyed;

        public float Height { get; private set; }
        public float Width { get; private set; }

        private void Awake()
        {
            Height = transform.localScale.y;
            Width = transform.localScale.x;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(Color color)
        {
            _color = color;
            _spriteRenderer.color = color;
            SetMaskable(false);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetMaskable(bool maskable)
        {
            var maskInteraction = 
                maskable ? SpriteMaskInteraction.VisibleInsideMask : SpriteMaskInteraction.None;

            _spriteRenderer.maskInteraction = maskInteraction;
            _overlaySpriteRenderer.maskInteraction = maskInteraction;
        }

        public void Destroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}