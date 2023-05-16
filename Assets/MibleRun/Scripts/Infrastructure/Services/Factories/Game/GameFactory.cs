using Scripts.Infrastructure.Services.StaticData;
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

        public void CreatePlayer()
        {
            PlayerStaticData playerStaticData = _staticDataService.PlayerStaticData();
            Player = InstantiatePrefabOnActiveScene(playerStaticData.Prefab);
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