using System;
using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using Scripts.Logic.BombControl;
using Scripts.Logic.Pool;
using Scripts.StaticClasses;
using Scripts.StaticData.Level;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Logic.LevelControl
{

    public class BombSpawner : MonoBehaviour
    {
        [SerializeField] private BombPool bombPool;
        
        private float _spawnRadius;
        private int _bombCount;
        private Coroutine _spawnBombsCoroutine;

        private void OnValidate()
        {
            if (!bombPool) TryGetComponent(out bombPool);
        }
        
        public void Initialize(LevelStaticData levelStaticData)
        {
            _bombCount = levelStaticData.BombCount;
            _spawnRadius = levelStaticData.SpawnRadius;
            bombPool.Initialize(levelStaticData.BombPrefab, levelStaticData.BombPoolSize);
            SpawnBombs();
        }

        public void SpawnBombs()
        {
            if (_spawnBombsCoroutine != null)
            {
                StopCoroutine(_spawnBombsCoroutine);
                _spawnBombsCoroutine = null;
            }
            
            _spawnBombsCoroutine = StartCoroutine(StartSpawnBombs());
        }

        private IEnumerator StartSpawnBombs()
        {
            bombPool.ResetPool();
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < _bombCount; i++)
            {
                if (bombPool.TryGetBomb(out Bomb bomb))
                {
                    Vector2 randomPos = GetRandomPosition();
                    bomb.transform.localPosition = new Vector3(randomPos.x, Constants.BombDefaultY,randomPos.y);
                    bomb.transform.eulerAngles = GetRandomRotation(bomb);
                    bomb.gameObject.SetActive(true);
                    bomb.transform.DOScale(Constants.BombDefaultSize, 0.3f);
                }
            }
            yield return new WaitForSeconds(0.3f);
        }

        private Vector2 GetRandomPosition()
        {
            Vector2 randomPos = Random.insideUnitCircle * _spawnRadius;
            while (randomPos.sqrMagnitude < 5)
            {
                randomPos = Random.insideUnitCircle * _spawnRadius;
            }

            return randomPos;
        }

        private Vector3 GetRandomRotation(Bomb bomb)
        {
            Vector3 eulerAngles = bomb.transform.eulerAngles;
            return new Vector3(eulerAngles.x, Random.Range(0, 360f), eulerAngles.z);
        }
    }

}