using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Scripts
{
    public struct ClickedElementData
    {
        public RectTransform RectTransform;
        public PointerEventData EventData;
        public Color Color;
    }
}