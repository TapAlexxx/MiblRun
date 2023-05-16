using TMPro;
using UnityEngine;

namespace Scripts.Logic.WindowControls
{

    public class LevelTextView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private GameObject bossImage;

        public void ViewLevel(int level, bool isBossLevel)
        {
            bossImage.SetActive(isBossLevel);
            text.text = isBossLevel ? "BOSS" : level.ToString();
        }
    }

}