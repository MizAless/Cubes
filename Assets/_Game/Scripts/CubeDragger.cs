using UnityEngine;
using Zenject;

namespace _Game.Scripts
{
    public class CubeDragger : MonoBehaviour
    {
        private Camera _mainCamera;

        private bool _isDragging = false;

        private Cube _draggingCube = null;

        private void Update()
        {
            TryDrag();
            TryStopDragging();
        }

        [Inject]
        public void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
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