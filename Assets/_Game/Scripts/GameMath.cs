using UnityEngine;

namespace _Game.Scripts
{
    public static class GameMath
    {
        public static void ClampPositionRegardingObject(Transform self, float widthOffset, Transform obj)
        {
            var width = obj.localScale.x;

            var maxWidthOffset = (width - widthOffset) / 2f / width;

            Vector3 localPositionInCatchArea =
                obj.InverseTransformPoint(self.position);
            localPositionInCatchArea.x = Mathf.Clamp(localPositionInCatchArea.x, -maxWidthOffset, maxWidthOffset);
            self.position = obj.TransformPoint(localPositionInCatchArea);
        }
    }
}