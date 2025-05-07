using UnityEngine;

public class QualityManager : MonoBehaviour
{
    void Awake()
    {
        // Set the quality level to maximum
        QualitySettings.SetQualityLevel(QualitySettings.names.Length - 1, true);
        
        // Force high quality rendering
        QualitySettings.vSyncCount = 1;
        QualitySettings.antiAliasing = 4;
        
        // Set texture quality to maximum
        QualitySettings.masterTextureLimit = 0; // 0 = Full res, 1 = Half res, 2 = Quarter res
        
        // Enable anisotropic filtering for better texture quality at angles
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;

        // Set maximum resolution
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
    }
} 