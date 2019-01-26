using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public float maxHealth = 200;
    public float currentHealth = 200;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
