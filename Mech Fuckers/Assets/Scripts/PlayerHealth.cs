using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 200;
    public float currentHealth = 200;
    public Image healthBar;
    public GameObject damageSparks;
    public GameObject damageHologram;
    public GameObject damageHologramRed;
    public GameObject damageAlarm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageTaken) {
        currentHealth -= damageTaken;

        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 110) {
            damageSparks.SetActive(true);
        }

        if (currentHealth <= 75)
        {
            damageHologram.SetActive(false);
            damageHologramRed.SetActive(true);
        }

        if (currentHealth <= 50)
        {
            damageAlarm.SetActive(true);
        }
    }
}
