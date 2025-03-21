using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using Zenject;

namespace _Game.Scripts
{
    public class ScrolledCubesPool : MonoBehaviour
    {
        [SerializeField] private Transform _cubesContainer;

        public event Action<ClickedElementData> ElementClicked;

        private List<UICubeView> _cubes = new List<UICubeView>();
        private ColorService _colorService;
        private UICubeView _uiCubeViewPrefab;

        private void OnEnable()
        {
            foreach (var uiCubeView in _cubes)
                uiCubeView.Clickled += UiCubeViewOnClickled;
        }

        private void OnDisable()
        {
            foreach (var uiCubeView in _cubes)
                uiCubeView.Clickled -= UiCubeViewOnClickled;
        }

        [Inject]
        private void Construct(List<Color> _availableColors)
        {
            var uiCubeViewPrefab = Prefabs.Load<UICubeView>();

            for (int i = 0; i < _availableColors.Count; i++)
            {
                var cube = Instantiate(uiCubeViewPrefab, _cubesContainer);
                cube.Init(_availableColors[i]);
                _cubes.Add(cube);
            }
        }

        private void UiCubeViewOnClickled(ClickedElementData clickedElementData)
        {
            ElementClicked?.Invoke(clickedElementData);
        }
    }
}