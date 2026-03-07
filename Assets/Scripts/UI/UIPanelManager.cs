using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CopGameDev.LaughingFoxTest.Inventory
{
    public interface IUIPanel
    {
        void ToggleView(bool toggle);
    }

    [Serializable]
    public abstract class UIPanelBase : MonoBehaviour, IUIPanel
    {
        public abstract void ToggleView(bool toggle);
    }

    public class UIPanelManager : MonoBehaviour
    {
        public static UIPanelManager Instance { get; private set; }

        [SerializeField]
        private InputActionReference escapeInput;

        [SerializeField]
        private List<UIPanel> panels = new();

        private IUIPanel activePanel;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            escapeInput.action.performed += _ => HidePanel(activePanel);
            escapeInput.action.Enable();

            foreach (var panel in panels)
            {
                panel.InputAction.action.performed += _ => TogglePanel(panel.Panel);
                panel.InputAction.action.Enable();
            }
        }

        private void OnDestroy()
        {
            escapeInput.action.Disable();
            escapeInput.action.performed -= _ => HidePanel(activePanel);

            foreach (var panel in panels)
            {
                panel.InputAction.action.Disable();
                panel.InputAction.action.performed -= _ => TogglePanel(panel.Panel);
            }
        }

        private void TogglePanel(IUIPanel panel)
        {
            if (activePanel == panel)
                HidePanel(activePanel);
            else
                ShowPanel(panel);
        }

        private void ShowPanel(IUIPanel panel)
        {
            HidePanel(activePanel);

            activePanel = panel;
            if (activePanel != null)
                activePanel.ToggleView(true);
        }

        private void HidePanel(IUIPanel panel)
        {
            if (panel == null)
                return;

            activePanel.ToggleView(false);
            activePanel = null;
        }

        [Serializable]
        private class UIPanel
        {
            [field: SerializeField]
            public UIPanelBase Panel { get; private set; }

            [field: SerializeField]
            public InputActionReference InputAction { get; private set; }
        }
    }
}
