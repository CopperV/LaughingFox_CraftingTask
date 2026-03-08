using CopGameDev.LaughingFoxTest.Items;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CopGameDev.LaughingFoxTest.Inventory
{
    public class DetailedItemView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private TMP_Text itemNameLabel;

        [SerializeField]
        private TMP_Text itemDescriptionLabel;

        [SerializeField]
        private Image itemIcon;

        [SerializeField]
        private RectTransform itemStatsContainer;

        [SerializeField]
        private ItemStatView itemStatViewPrefab;

        private ItemData itemData;

        private void Awake()
        {
            Hide();
        }

        public void Show(ItemData itemData)
        {
            ClearView();

            this.itemData = itemData;

            itemNameLabel.text = itemData.ItemName;
            itemDescriptionLabel.text = itemData.Description;
            itemIcon.sprite = itemData.Icon;

            var stats = itemData.Stats;
            List<(string, int)> statsToDisplay = new();

            if (stats.Damage != 0) statsToDisplay.Add(("Obra¿enia", stats.Damage));
            if (stats.Defense != 0) statsToDisplay.Add(("Obrona", stats.Defense));
            if (stats.Strength != 0) statsToDisplay.Add(("Si³a", stats.Strength));
            if (stats.Dexterity != 0) statsToDisplay.Add(("Zrêcznoœæ", stats.Dexterity));
            if (stats.Intelligence != 0) statsToDisplay.Add(("Inteligencja", stats.Intelligence));

            statsToDisplay.ForEach(stat =>
            {
                var statView = Instantiate(itemStatViewPrefab, itemStatsContainer);
                statView.Setup(stat.Item1, stat.Item2.ToString());
            });

            StartCoroutine(LayoutRebuildCoroutine());
        }

        private IEnumerator LayoutRebuildCoroutine()
        {
            var rect = transform as RectTransform;

            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            StopAllCoroutines();
            ClearView();
            canvasGroup.alpha = 0;
            itemData = null;
        }

        private void ClearView()
        {
            foreach (Transform child in itemStatsContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
