// using System;
// using UnityEngine;
//
// namespace _Game.Scripts
// {
//     public class EntryPoint : MonoBehaviour
//     {
//         [SerializeField] private ScrolledCubesPool _scrolledCubesPool;
//
//         private CubeMediator _mediator;
//         private CubeSpawner _cubeSpawner;
//         private CubesPool _cubesPool;
//         private CubesFabric _cubesFabric;
//         
//         private void Awake()
//         {
//             _cubesFabric = new CubesFabric(Prefabs.Load<Cube>());
//             _cubesPool = new CubesPool(_cubesFabric);
//             _cubeSpawner = new CubeSpawner(_cubesPool);
//             
//             _mediator = new CubeMediator(_cubeSpawner, _scrolledCubesPool);
//         }
//     }
// }