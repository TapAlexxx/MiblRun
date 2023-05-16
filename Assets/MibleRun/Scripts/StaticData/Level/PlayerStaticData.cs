using UnityEngine;

namespace Scripts.StaticData.Level
{

    [CreateAssetMenu(menuName = "StaticData/Player", fileName = "PlayerStaticData", order = 0)]
    public class PlayerStaticData : ScriptableObject
    {
        public GameObject Prefab;
        public float Speed;
    }

}