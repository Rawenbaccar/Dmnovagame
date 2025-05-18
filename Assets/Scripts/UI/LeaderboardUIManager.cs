using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DMNovaGame.UI
{
    public class LeaderboardUIManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private CanvasScaler canvasScaler;
        [SerializeField] private RectTransform leaderboardContainer;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private RectTransform entriesContainer;
        
        [Header("Configuration")]
        [SerializeField] private float minWidth = 800f;
        [SerializeField] private float maxWidth = 1920f;
        [SerializeField] private float minTitleSize = 42f;
        [SerializeField] private float maxTitleSize = 64f;
        [SerializeField] private float minEntryHeight = 60f;
        [SerializeField] private float maxEntryHeight = 80f;

        private void Start()
        {
            if (canvasScaler == null)
                canvasScaler = GetComponentInParent<CanvasScaler>();
                
            AdjustUIForCurrentResolution();
        }

        private void OnRectTransformDimensionsChange()
        {
            AdjustUIForCurrentResolution();
        }

        private void AdjustUIForCurrentResolution()
        {
            if (leaderboardContainer == null) return;

            // Get current screen dimensions
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            float aspectRatio = screenWidth / screenHeight;

            // Calculate scale factors
            float widthScale = Mathf.InverseLerp(minWidth, maxWidth, screenWidth);
            
            // Adjust title text size
            if (titleText != null)
            {
                float newTitleSize = Mathf.Lerp(minTitleSize, maxTitleSize, widthScale);
                titleText.fontSize = newTitleSize;
            }

            // Adjust entry heights
            if (entriesContainer != null)
            {
                float newEntryHeight = Mathf.Lerp(minEntryHeight, maxEntryHeight, widthScale);
                
                // Adjust all entry rect transforms
                foreach (RectTransform entry in entriesContainer)
                {
                    Vector2 sizeDelta = entry.sizeDelta;
                    sizeDelta.y = newEntryHeight;
                    entry.sizeDelta = sizeDelta;
                }
            }

            // Adjust container size based on aspect ratio
            if (aspectRatio < 1f) // Portrait
            {
                leaderboardContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 
                    screenWidth * 0.9f);
            }
            else // Landscape
            {
                leaderboardContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 
                    screenWidth * 0.7f);
            }

            // Force layout update
            LayoutRebuilder.ForceRebuildLayoutImmediate(leaderboardContainer);
        }
    }
}