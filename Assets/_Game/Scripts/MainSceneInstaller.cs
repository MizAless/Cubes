using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Game.Scripts
{
    public class MainSceneInstaller : MonoInstaller
    {
        [Header("UI")]
        [SerializeField] private ScrolledCubesPool _scrolledCubesPool;
        [SerializeField] private Camera _mainCamera;
        [Header("Colors")]
        [SerializeField] private List<Color> _availableColors = new List<Color>();
        [Header("Input")]
        [SerializeField] private CubeDragger _cubeDragger;
        [SerializeField] private ClickInput _clickInput;
        [Header("Tower")]
        [SerializeField] private Transform _towerLowerPoint;
        [SerializeField] private CatchArea _towerLayoutCatchArea;
        [SerializeField] private CatchArea _towerCatchArea;
        [SerializeField] private BoxCollider2D _towerCollider2D;

        public override void InstallBindings()
        {
            Container
                .BindInstance(_availableColors)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<ScrolledCubesPool>()
                .FromInstance(_scrolledCubesPool)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<CubeSpawner>()
                .FromMethod(CreateCubeSpawner)
                .AsSingle()
                .NonLazy();

            Container
                .BindInstance(_cubeDragger)
                .AsSingle()
                .NonLazy();

            Container
                .BindInstance(_mainCamera)
                .AsSingle()
                .NonLazy();

            Container
                .BindInstance(_clickInput)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<CubeMediator>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<CubePlacementManager>()
                .AsSingle()
                .WithArguments(_towerLayoutCatchArea, _towerCatchArea)
                .NonLazy();

            Container
                .Bind<CubesTower>()
                .AsSingle()
                .WithArguments(_towerLowerPoint, _towerCollider2D, _towerLayoutCatchArea)
                .NonLazy();

            // var pool = Container.Resolve<ScrolledCubesPool>();
            // var spawner = Container.Resolve<CubeSpawner>();
            // var cubeDragger = Container.Resolve<CubeDragger>();

            // var mediator = new CubeMediator(spawner, pool, cubeDragger, _mainCamera);
        }

        private CubeSpawner CreateCubeSpawner()
        {
            CubesFabric cubesFabric = new CubesFabric(Prefabs.Load<Cube>());
            CubesPool cubesPool = new CubesPool(cubesFabric);

            return new CubeSpawner(cubesPool);
        }
    }
}