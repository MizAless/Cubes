using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Game.Scripts
{
    [RequireComponent(typeof(Image))]
    public class UICubeView : MonoBehaviour, IPointerDownHandler
    {
        private Image _image;
        private RectTransform _rectTransform;
        
        private Color _color;
        private ClickedElementData _clickedElementData;

        public event Action<ClickedElementData> Clickled;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _rectTransform  = GetComponent<RectTransform>();
        }
        
        public void Init(Color color)
        {
            _color = color;
            _image.color = color;
            _clickedElementData.Color = _color;
            _clickedElementData.RectTransform = _rectTransform;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _clickedElementData.EventData = eventData;
            Clickled?.Invoke(_clickedElementData);
        }
    }
}