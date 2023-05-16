using UnityEngine;

namespace Scripts.Logic.WindowControls
{

    public class SoundSettingsView : MonoBehaviour
    {
        [SerializeField] private GameObject soundOn;
        [SerializeField] private GameObject soundOff;

        public void RefreshView(bool isSoundOn)
        {
            soundOn.SetActive(isSoundOn);
            soundOff.SetActive(!isSoundOn);
        }
    }

}