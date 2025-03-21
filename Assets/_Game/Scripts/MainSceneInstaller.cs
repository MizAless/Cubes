using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Game.Scripts
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] ScrolledCubesPool _scrolledCubesPool;
        [SerializeField] private List<Color> _availableColors = new List<Color>();
        [SerializeField] private Camera _mainCamera;

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

            var pool = Container.Resolve<ScrolledCubesPool>();
            var spawner = Container.Resolve<CubeSpawner>();

            var mediator = new CubeMediator(spawner, pool, _mainCamera);
        }

        private CubeSpawner CreateCubeSpawner()
        {
            CubesFabric cubesFabric = new CubesFabric(Prefabs.Load<Cube>());
            CubesPool cubesPool = new CubesPool(cubesFabric);

            return new CubeSpawner(cubesPool);
        }
    }
}