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

    float TimeLeft = 20f;

    public GameObject ParticleGun1;
    public GameObject ParticleGun2;
    bool DoubleDamage = false;

    bool DoubleSpeed = false;

    public void Update()
    {
        if(DoubleDamage)
        {
            TimeLeft -= Time.deltaTime;
            if(TimeLeft <= 0)
            {
                DoubleDamage = false;
                ParticleGun1.GetComponent<PlayerShotHit>().Damage /= 2;
                ParticleGun2.GetComponent<PlayerShotHit>().Damage /= 2;
                TimeLeft = 20f;
            }
        }
        else if(DoubleSpeed)
        {
            TimeLeft -= Time.deltaTime;
            if(TimeLeft <= 0)
            {
                DoubleSpeed = false;
                this.gameObject.GetComponent<PlayerMovement>().speed /= 2;
            }
        }
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

    public void FoundRepairKit()
    {
        currentHealth += 50;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void FoundBattery()
    {
        DoubleDamage = true;
        ParticleGun1.GetComponent<PlayerShotHit>().Damage *= 2;
        ParticleGun2.GetComponent<PlayerShotHit>().Damage *= 2;
    }

    public void FoundNitro()
    {
        DoubleSpeed = true;
        this.gameObject.GetComponent<PlayerMovement>().speed *= 2;
    }

}
