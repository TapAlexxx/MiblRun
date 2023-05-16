using System;
using Scripts.Infrastructure.Services.StaticData;
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
        

        public GameFactory(IInstantiator instantiator, IStaticDataService staticDataService) : base(instantiator)
        {
            _staticDataService = staticDataService;
        }

        public void CreatePlayer(Transform spawnPoint)
        {
            PlayerStaticData playerStaticData = _staticDataService.PlayerStaticData();
            Player = InstantiatePrefabOnActiveScene(playerStaticData.Prefab);
            Player.transform.position = spawnPoint.position;
            Player.transform.rotation = spawnPoint.rotation;

            InitUnit(Player, playerStaticData);
        }

        private void InitUnit(GameObject unit, PlayerStaticData staticData)
        {
            UnitMovement unitMovement = unit.GetComponent<UnitMovement>();
            unitMovement.Initialize(staticData);
            
            UnitRotation unitRotation = unit.GetComponent<UnitRotation>();
            unitRotation.Initialize(staticData);
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