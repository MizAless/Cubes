using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Scripts
{
    public struct ClickedElementData
    {
        public RectTransform RectTransform;
        public PointerEventData EventData; // попробовать использовать 
        public Color Color;
        
        // Вычисляем смещение от начала касания
        // Vector2 delta = eventData.position - _startDragPosition;
        //
        // Если движение в основном по диагонали (не строго горизонтальное)
        // if (Mathf.Abs(delta.y) > _dragThreshold)
    }
}