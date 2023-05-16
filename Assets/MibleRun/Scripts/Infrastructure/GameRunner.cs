using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField, NotNull] private SceneContext _defaultSceneContext;

        private void Awake()
        {
            Application.targetFrameRate = 1000;
            if (!FindObjectOfType<SceneContext>())
                Instantiate(_defaultSceneContext);

            Destroy(gameObject);
        }
    }
}