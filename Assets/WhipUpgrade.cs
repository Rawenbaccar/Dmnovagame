using UnityEngine;

public class WhipUpgrade : MonoBehaviour
{
    public GameObject upgradedWhipPrefab; // Version améliorée du Whip
    private bool isUpgraded = false;

    public void UpgradeWhip()
    {
        if (!isUpgraded)
        {
            isUpgraded = true;
            GameObject newWhip = Instantiate(upgradedWhipPrefab, transform.position, Quaternion.identity);
            newWhip.transform.parent = transform.parent; // Attache au même parent
            Destroy(gameObject); // Détruit l'ancien fouet
        }
    }
}
