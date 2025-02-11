using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DomageDiamond : MonoBehaviour
{
    public TextMeshProUGUI domageText;
    public float lifetime ;
    private float lifeCounter;
    // Start is called before the first frame update
    void Start()
    {
        lifeCounter = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime;
            if(lifeCounter <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void setup(int damageAmount){
        lifeCounter = lifetime;
        domageText.text = damageAmount.ToString();
    }
}
