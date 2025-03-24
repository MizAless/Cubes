using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class CubeMediator : IDisposable
    {
        private readonly CubeSpawner _cubeSpawner;
        private readonly ScrolledCubesPool _scrolledCubesPool;
        private readonly CubeDragger _cubeDragger;
        private readonly Camera _mainCamera;

        public CubeMediator(CubeSpawner cubeSpawner, ScrolledCubesPool scrolledCubesPool, CubeDragger cubeDragger, Camera mainCamera)
        {
            _cubeSpawner = cubeSpawner;
            _scrolledCubesPool = scrolledCubesPool;
            _mainCamera = mainCamera;
            _cubeDragger = cubeDragger;

            _scrolledCubesPool.ElementClicked += OnClicked;
        }

        public void Dispose()
        {
            _scrolledCubesPool.ElementClicked -= OnClicked;
        }

        private void OnClicked(ClickedElementData clickedElementData)
        {
            Vector3 screenPosition = clickedElementData.EventData.position;
            screenPosition.z = _mainCamera.nearClipPlane;
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);

            var cube = _cubeSpawner.Spawn(worldPosition, clickedElementData.Color);
            
            _cubeDragger.StartDragging(cube);
        }
    }
}