using UnityEngine;
using UnityEngine.UI;

namespace DMNovaGame
{
    [ExecuteInEditMode]
    public class PortraitModeSetup : MonoBehaviour
    {
        private void Awake()
        {
            SetupCanvasForPortrait();
            AdjustCharacterSelectLayout();
        }

        private void SetupCanvasForPortrait()
        {
            var canvas = GetComponent<Canvas>();
            if (canvas == null) return;

            // Ensure the canvas is using Screen Space - Overlay
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = GetComponent<CanvasScaler>();
            if (scaler == null) return;

            // Set up the Canvas Scaler for portrait mode
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920); // Standard portrait resolution
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 1f; // Match height for portrait mode
        }

        private void AdjustCharacterSelectLayout()
        {
            // Find the CharacterSelectPanel
            var characterSelectPanel = transform.Find("CharacterSelectPanel");
            if (characterSelectPanel == null) return;

            var rectTransform = characterSelectPanel.GetComponent<RectTransform>();
            if (rectTransform == null) return;

            // Make the panel stretch to fill the screen with padding
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(20, 20); // Left, Bottom padding
            rectTransform.offsetMax = new Vector2(-20, -20); // Right, Top padding

            // Adjust the grid layout
            var characterGrid = characterSelectPanel.Find("CharacterGrid");
            if (characterGrid != null)
            {
                var gridRectTransform = characterGrid.GetComponent<RectTransform>();
                if (gridRectTransform != null)
                {
                    // Center the grid in the upper portion of the screen
                    gridRectTransform.anchorMin = new Vector2(0, 0.4f);
                    gridRectTransform.anchorMax = new Vector2(1, 0.9f);
                    gridRectTransform.anchoredPosition = Vector2.zero;
                    gridRectTransform.sizeDelta = Vector2.zero;
                }
            }

            // Adjust the confirm button
            var confirmButton = characterSelectPanel.Find("Confirm");
            if (confirmButton != null)
            {
                var confirmRectTransform = confirmButton.GetComponent<RectTransform>();
                if (confirmRectTransform != null)
                {
                    // Position the confirm button at the bottom center
                    confirmRectTransform.anchorMin = new Vector2(0.5f, 0.1f);
                    confirmRectTransform.anchorMax = new Vector2(0.5f, 0.1f);
                    confirmRectTransform.anchoredPosition = Vector2.zero;
                    confirmRectTransform.sizeDelta = new Vector2(200, 60);
                }
            }

            // Adjust character selection items
            AdjustCharacterSelectionItem("Character1 select");
            AdjustCharacterSelectionItem("Character2 select ");
        }

        private void AdjustCharacterSelectionItem(string itemName)
        {
            var characterSelect = transform.Find("CharacterSelectPanel/" + itemName);
            if (characterSelect == null) return;

            var rectTransform = characterSelect.GetComponent<RectTransform>();
            if (rectTransform == null) return;

            // Make character selection items responsive
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = new Vector2(300, 400); // Fixed size for character cards
        }
    }
}