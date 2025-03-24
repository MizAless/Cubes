using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Game.Scripts
{
    public class ScrolledCubesPool : MonoBehaviour
    {
        [SerializeField] private Transform _cubesContainer;
        [SerializeField] private float _clickThreshold = 0.1f;
        [SerializeField] private ScrollRect _scrollRect;

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

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
                _scrollRect.enabled = true;
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
            StartCoroutine(HandleClick(clickedElementData));
        }
        
        private IEnumerator HandleClick(ClickedElementData clickedElementData)
        {
            var startPosition = Input.mousePosition;
            
            yield return new WaitForSeconds(_clickThreshold);
            
            var delta = Input.mousePosition - startPosition;
            delta.Normalize();
            
            if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
            {
                _scrollRect.enabled = false;
                ElementClicked?.Invoke(clickedElementData);
            }
        }
    }
}