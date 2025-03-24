using System;
using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public class Logger : IDisposable
    {
        private readonly CubesTower _cubesTower;
        private readonly CubesHole _cubesHole;
        private readonly ActionsTextView _actionsTextView;
        private readonly CubePlacementManager _cubePlacementManager;

        public Logger(CubesTower cubesTower, CubesHole cubesHole, ActionsTextView actionsTextView, CubePlacementManager cubePlacementManager)
        {
            _cubesTower = cubesTower;
            _cubesHole = cubesHole;
            _actionsTextView = actionsTextView;
            _cubePlacementManager = cubePlacementManager;

            _cubesHole.Dropped += OnCubeDropped;
            _cubesTower.CubeAdded += OnCubeAdded;
            _cubesTower.TowerFulled += OnTowerFulled;
            _cubePlacementManager.CubeMissed += OnCubeMissed;
        }

        public void Dispose()
        {
            _cubesHole.Dropped -= OnCubeDropped;
            _cubesTower.CubeAdded -= OnCubeAdded;
            _cubesTower.TowerFulled -= OnTowerFulled;
            _cubePlacementManager.CubeMissed -= OnCubeMissed;
        }

        private void OnTowerFulled()
        {
            Log("Tower is full");
        }

        private void OnCubeMissed()
        {
            Log("Cube missed tower");
        }

        private void OnCubeAdded()
        {
            Log("Cubes added to tower");
        }

        private void OnCubeDropped()
        {
            Log("Cubes dropped into hole");
        }

        private void Log(string message)
        {
            _actionsTextView.SetMessage(message);
        }

        // private IEnumerator LogDelayed(string message, float delay)
        // {
        //     yield return new WaitForSeconds(delay);
        //     Log(message);
        // }
    }
}