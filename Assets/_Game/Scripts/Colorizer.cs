using UnityEngine;

namespace _Game.Scripts
{
    public class ColorService
    {
        private readonly Color[] _availableColors;

        public ColorService(Color[] availableColors)
        {
            _availableColors = availableColors;
        }

        public int GetColorCount()
        {
            return _availableColors.Length;
        }
        
        public Color GetColor(int index)
        {
            return _availableColors[index];
        }
        
        // public Color GetRandomColor()
        // {
            // return _availableColors[Random.Range(0, _availableColors.Length - 1)];
        // }
    }
}