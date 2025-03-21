using System;
using UnityEngine;
using Zenject;

namespace _Game.Scripts
{
    public class CubeDragger : MonoBehaviour, IDisposable
    {
        private Camera _mainCamera;
        private CubeSpawner _cubeSpawner;

        private Cube _draggingCube;

        private bool _isDragging = false;
        
        private ClickInput _clickInput;

        private void Update()
        {
            TryDrag();
            TryStopDragging();
        }

        [Inject]
        public void Construct(ClickInput clickInput, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _clickInput = clickInput;
            
            _clickInput.CubeClicked += StartDragging;
        }

        public void Dispose()
        {
            _clickInput.CubeClicked -= StartDragging;
        }

        public void StartDragging(Cube cube)
        {
            if (_isDragging)
                return;

            _isDragging = true;
            _draggingCube = cube;
        }

        private void TryDrag()
        {
            if (!_isDragging || _draggingCube == null)
                return;

            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _draggingCube.transform.position = new Vector3(mousePosition.x, mousePosition.y, _draggingCube.transform.position.z);
        }

        private void TryStopDragging()
        {
            if (!_isDragging)
                return;

            if (!Input.GetMouseButtonUp(0))
                return;
            
            _isDragging = false;
            _draggingCube = null;
        }
    }
}