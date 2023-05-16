using UnityEngine;

namespace Scripts.Logic
{

    public class CameraPlacer : MonoBehaviour
    {
        [SerializeField] private Camera sceneCamera;

        private void OnValidate()
        {
            if (!sceneCamera) TryGetComponent(out sceneCamera);
        }

        public void PlaceCamera(Vector2Int gridSize)
        {
            transform.position = new Vector3((gridSize.x - 1) / 2f, 10, gridSize.y / 2f - 4.5f);
            sceneCamera.orthographicSize = gridSize.x + 1;
        }
    }

}