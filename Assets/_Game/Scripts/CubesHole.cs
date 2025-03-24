using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts
{
    public class CubesHole : MonoBehaviour
    {
        [SerializeField] private SpriteMask _downMask;

        public void DropCube(Cube cube) 
        {
            var startPosition = cube.transform.position.WithY(transform.position.y + 5f);
            cube.transform.position = startPosition;

            _downMask.enabled = false;
            cube.SetMaskable(true);

            GameMath.ClampPositionRegardingObject(cube.transform, cube.Width + 0.2f, transform);

            var endPosition = transform.position.WithX(cube.transform.position.x) + Vector3.down * 2f;
            
            cube.transform.DOMove(endPosition, 1f)
                .SetEase(Ease.InSine)
                .OnComplete(cube.Destroy);
        }
    }
}