using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class CubeMediator : IDisposable
    {
        private CubeSpawner _cubeSpawner;
        private readonly ScrolledCubesPool _scrolledCubesPool;
        private Camera _mainCamera;

        public CubeMediator(CubeSpawner cubeSpawner, ScrolledCubesPool scrolledCubesPool, Camera mainCamera)
        {
            _cubeSpawner = cubeSpawner;
            _scrolledCubesPool = scrolledCubesPool;
            _mainCamera = mainCamera;

            _scrolledCubesPool.ElementClicked += OnClickled;
        }

        public void Dispose()
        {
            _scrolledCubesPool.ElementClicked -= OnClickled;
        }

        private void OnClickled(ClickedElementData clickedElementData)
        {
            Vector3 screenPosition = clickedElementData.EventData.position;
            screenPosition.z = _mainCamera.nearClipPlane;
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);

            _cubeSpawner.Spawn(worldPosition, clickedElementData.Color);
        }
    }
}