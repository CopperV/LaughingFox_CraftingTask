using System;
using UnityEngine;

namespace CopGameDev.LaughingFoxTest.Items
{

    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField]
        public string ItemName { get; private set; }

        [field: SerializeField, TextArea]
        public string Description { get; private set; }

        [field: SerializeField]
        public ItemCategory Category { get; private set; }

        [field: SerializeField]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        [field: SerializeField]
        public ItemStats Stats { get; private set; }

        [Serializable]
        public class ItemStats
        {
            [field: SerializeField]
            public int Damage { get; private set; }

            [field: SerializeField]
            public int Defense { get; private set; }

            [field: SerializeField]
            public int Strength { get; private set; }

            [field: SerializeField]
            public int Dexterity { get; private set; }

            [field: SerializeField]
            public int Intelligence { get; private set; }
        }
    }
}
