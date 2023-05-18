using Scripts.Logic.EnemyControl;
using Scripts.Logic.LevelControl;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Factories.Game
{
    public interface IGameFactory
    {
        GameObject Player { get; }
        GameObject GameHud { get; }
        EnemySpawner EnemySpawner { get; }
        GameObject CreateHud();
        void Clear();
        void CreatePlayer(Vector3 position, Quaternion rotation);
        void CreateBombSpawner();
        void CreateEnemySpawner();
        Enemy CreateEnemy(Transform parent);
    }
}