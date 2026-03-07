using TMPro;
using UnityEngine;


namespace CopGameDev.LaughingFoxTest.Inventory
{
    public class ItemStatView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text statNameLabel;

        [SerializeField]
        private TMP_Text statValueLabel;

        public void Setup(string statName, string statValue)
        {
            statNameLabel.text = statName;
            statValueLabel.text = statValue;
        }
    }
}
