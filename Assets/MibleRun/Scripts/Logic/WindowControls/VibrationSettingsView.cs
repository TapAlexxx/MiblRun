using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Scripts.Logic.WindowControls
{

    public class VibrationSettingsView : MonoBehaviour
    {
        [SerializeField] private GameObject noneIcon;
        [SerializeField] private GameObject lightIcon;
        [SerializeField] private GameObject softIcon;

        public void RefreshView(HapticTypes vibrationType)
        {
            noneIcon.SetActive(vibrationType == HapticTypes.None);
            lightIcon.SetActive(vibrationType == HapticTypes.LightImpact);
            softIcon.SetActive(vibrationType == HapticTypes.SoftImpact);
        }
    }

}