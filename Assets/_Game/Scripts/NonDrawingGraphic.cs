using UnityEngine.UI;

namespace _Game.Scripts
{
    public class NonDrawingGraphic : Graphic
    {
        public override void SetMaterialDirty()
        {
            return;
        }

        public override void SetVerticesDirty()
        {
            return;
        }
    }
}