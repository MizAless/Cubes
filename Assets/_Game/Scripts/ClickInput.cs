using System;
using UnityEngine;
using Zenject;

namespace _Game.Scripts
{
    public class ClickInput : MonoBehaviour
    {
        private Camera _mainCamera;

        public event Action<Cube> CubeClicked;

        private void Update()
        {
            TryClick();
        }

        [Inject]
        private void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        private void TryClick()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (TryGetCube(out Cube cube))
                    CubeClicked?.Invoke(cube);
            }
        }

        private bool TryGetCube(out Cube cube)
        {
            cube = null;

            var hits = Physics2D.RaycastAll(_mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            foreach (var hit in hits)
            {
                if (hit.collider == null)
                    continue;

                if (hit.collider.TryGetComponent(out cube))
                    break;
            }

            return cube != null;
        }
    }
}