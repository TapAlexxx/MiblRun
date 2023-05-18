using System;
using Scripts.Infrastructure.Services.StaticData;
using Scripts.Logic.EnemyControl;
using Scripts.Logic.LevelControl;
using Scripts.Logic.PlayerControl;
using Scripts.Logic.PlayerControl.PlayerInput;
using Scripts.Logic.Unit;
using Scripts.StaticData.Level;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.Services.Factories.Game
{
    public class GameFactory : Factory, IGameFactory
    {
        private readonly IStaticDataService _staticDataService;

        public GameObject Player { get; private set; }
        public GameObject GameHud { get; private set; }
        public EnemySpawner EnemySpawner { get; private set; }


        public GameFactory(IInstantiator instantiator, IStaticDataService staticDataService) : base(instantiator)
        {
            _staticDataService = staticDataService;
        }

        public void CreatePlayer(Vector3 position, Quaternion rotation)
        {
            PlayerStaticData playerStaticData = _staticDataService.PlayerStaticData();
            Player = InstantiatePrefabOnActiveScene(playerStaticData.Prefab);
            Player.transform.position = position;
            Player.transform.rotation = rotation;
            Player.GetComponent<PlayerHealth>().Revive();
            
            InitUnit(Player, playerStaticData.MoveSpeed, playerStaticData.RotationSpeed);
        }

        public void CreateBombSpawner()
        {
            LevelStaticData levelStaticData = _staticDataService.GetLevelStaticData();
            GameObject bombSpawner = InstantiatePrefabOnActiveScene(levelStaticData.BombSpawner);
            bombSpawner.GetComponent<BombSpawner>().Initialize(levelStaticData);
        }

        public void CreateEnemySpawner()
        {
            LevelStaticData levelStaticData = _staticDataService.GetLevelStaticData();
            GameObject enemySpawnerObject = InstantiatePrefabOnActiveScene(levelStaticData.EnemySpawner);
            EnemySpawner enemySpawner = enemySpawnerObject.GetComponent<EnemySpawner>();
            enemySpawner.Initialize(levelStaticData, Player.transform);
            EnemySpawner = enemySpawner;
        }

        public Enemy CreateEnemy(Transform parent)
        {
            LevelStaticData levelStaticData = _staticDataService.GetLevelStaticData();
            GameObject enemyObject = InstantiatePrefabOnActiveScene(levelStaticData.EnemyPrefab.gameObject);
            InitUnit(enemyObject, levelStaticData.EnemyMoveSpeed, levelStaticData.EnemyRotationSpeed);
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            enemy.Initialize(Player.transform);
            
            return enemy;
        }

        private void InitUnit(GameObject unit, float moveSpeed, float rotationSpeed)
        {
            UnitMovement unitMovement = unit.GetComponent<UnitMovement>();
            unitMovement.Initialize(moveSpeed);
            
            UnitRotation unitRotation = unit.GetComponent<UnitRotation>();
            unitRotation.Initialize(rotationSpeed);
        }

        public GameObject CreateHud()
        {
            GameHud = InstantiateOnActiveScene("Hud/Hud");
            return GameHud;
        }

        public void Clear()
        {
            Player = null;
            GameHud = null;
        }

    }
}