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
            
            var hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider == null)
                return false;

            if (!hit.collider.TryGetComponent(out cube))
                return false;
            
            return true;
        }
    }
}