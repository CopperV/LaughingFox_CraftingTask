using CopGameDev.LaughingFoxTest.Items;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
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

        private ObjectPool<ItemStatView> statPool;
        private List<ItemStatView> activeStats = new();

        private ItemData itemData;

        private void Awake()
        {
            statPool = new(
                createFunc: () => Instantiate(itemStatViewPrefab, itemStatsContainer),
                actionOnGet: slot => slot.gameObject.SetActive(true),
                actionOnRelease: slot => slot.gameObject.SetActive(false),
                actionOnDestroy: slot => Destroy(slot.gameObject),
                collectionCheck: false,
                defaultCapacity: 0,
                maxSize: 5
            );

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

            TryAddStat("Obra¿enia", stats.Damage);
            TryAddStat("Obrona", stats.Defense);
            TryAddStat("Si³a", stats.Strength);
            TryAddStat("Zrêcznoœæ", stats.Dexterity);
            TryAddStat("Inteligencja", stats.Intelligence);

            statsToDisplay.ForEach(stat =>
            {
                var statView = Instantiate(itemStatViewPrefab, itemStatsContainer);
                statView.Setup(stat.Item1, stat.Item2.ToString());
            });

            StartCoroutine(LayoutRebuildCoroutine());
        }

        private void TryAddStat(string label, int value)
        {
            if (value == 0)
                return;

            var statView = statPool.Get();
            statView.transform.SetAsLastSibling();
            statView.Setup(label, value.ToString());
            activeStats.Add(statView);
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
            foreach (var slot in activeStats)
            {
                statPool.Release(slot);
            }
            activeStats.Clear();
        }
    }
}
