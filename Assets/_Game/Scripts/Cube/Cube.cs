using System;
using DG.Tweening;
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

        public Color Color => _color;
        
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

        public void AnimateFall(float height, float duration)
        {
            var endPoint = transform.position;
            var startPoint = transform.position + Vector3.up * height;

            transform.position = startPoint;

            transform.DOMove(endPoint, duration)
                .SetEase(Ease.InSine);
        }

        public void Destroy()
        {
            Destroyed?.Invoke(this);
        }

        public void DestroyWithAnimation()
        {
            AnimateFall(2f,0.5f);
            
            _overlaySpriteRenderer.DOFade(1, 0.5f)
                .SetEase(Ease.InSine);
            
            _spriteRenderer.DOFade(0, 0.5f)
                .SetEase(Ease.InSine).OnComplete(Destroy);
        }
    }
}