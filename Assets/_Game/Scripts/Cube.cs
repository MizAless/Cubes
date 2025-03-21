using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Cube : MonoBehaviour , IPointerDownHandler
    {
        private Color _color;
        private SpriteRenderer _spriteRenderer;

        public event Action<Cube> Clicked;
        public event Action<Cube> Destroyed;

        private void Awake()
        {
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

        public void OnPointerDown(PointerEventData eventData)
        {
            print("Clicked");
            Clicked?.Invoke(this);
        }

        private void OnMouseDown()
        {
            print("Clicked");
            Clicked?.Invoke(this);
        }
    }
}