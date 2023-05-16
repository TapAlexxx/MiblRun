using UnityEngine;

namespace Scripts.Logic
{
    public class Raycaster : MonoBehaviour
    {
        private Camera _camera;
        private bool _active;

        private void Awake()
        {
            _camera = Camera.main;
            Activate();
        }

        public bool Raycast(int layer, out RaycastHit hit)
        {
            hit = new RaycastHit();
            return _active && 
                   Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100, layer);
        }

        public void Disable()
        {
            _active = false;
        }
        
        public void Activate()
        {
            _active = true;
        }
    }
}