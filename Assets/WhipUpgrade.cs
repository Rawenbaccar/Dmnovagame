using UnityEngine;

public class WhipUpgrade : MonoBehaviour
{
    public GameObject upgradedWhipPrefab; // Version am�lior�e du Whip
    private bool isUpgraded = false;

    public void UpgradeWhip()
    {
        if (!isUpgraded)
        {
            isUpgraded = true;
            GameObject newWhip = Instantiate(upgradedWhipPrefab, transform.position, Quaternion.identity);
            newWhip.transform.parent = transform.parent; // Attache au m�me parent
            Destroy(gameObject); // D�truit l'ancien fouet
        }
    }
}
