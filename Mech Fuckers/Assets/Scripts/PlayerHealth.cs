﻿using System.Collections;
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
    public CharacterController controller;

    bool PowerUp = false;
    public float TimeLeft = 20f;
    public Text powerupUI;

    public GameObject playerDeath;
    bool isDead = false;

    public GameObject ParticleGun1;
    public GameObject ParticleGun2;
    bool DoubleDamage = false;
    bool DoubleSpeed = false;
    bool JumpBoost = false;

    public void Update()
    {
        if(PowerUp)
        {
            TimeLeft -= Time.deltaTime;
            if(TimeLeft <= 0)
            {
                if(DoubleDamage)
                {
                    DoubleDamage = false;
                    ParticleGun1.GetComponent<PlayerShotHit>().Damage /= 2;
                    ParticleGun2.GetComponent<PlayerShotHit>().Damage /= 2;
                    powerupUI.text = "";
                }
                if(DoubleSpeed)
                {
                    DoubleSpeed = false;
                    this.gameObject.GetComponent<PlayerMovement>().speed /= 2;
                    powerupUI.text = "";
                }
                if(JumpBoost)
                {
                    JumpBoost = false;
                    this.gameObject.GetComponent<PlayerMovement>().jumpSpeed /= 2;
                    powerupUI.text = "";
                }
                CheckOtherPowerUps();
            }
        }

        if (isDead == true && controller.isGrounded) {
            playerDeath.transform.position = gameObject.transform.position;
            playerDeath.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void CheckOtherPowerUps()
    {
        if (!DoubleDamage && !DoubleSpeed && !JumpBoost)
        {
            PowerUp = false;
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

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public void FoundRepairKit()
    {
        currentHealth += 125;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void FoundBattery()
    {
        if(!DoubleDamage)
        {
            PowerUp = true;
            DoubleDamage = true;
            ParticleGun1.GetComponent<PlayerShotHit>().Damage *= 2;
            ParticleGun2.GetComponent<PlayerShotHit>().Damage *= 2;
            powerupUI.text = "DOUBLE DAMAGE";
        }
        TimeLeft = 20f;
    }

    public void FoundNitro()
    {
        if(!DoubleSpeed)
        {
            PowerUp = true;
            DoubleSpeed = true;
            this.gameObject.GetComponent<PlayerMovement>().speed *= 2;
            powerupUI.text = "NITRO RUN";
        }
        TimeLeft = 20f;
    }

    public void FoundJetPack()
    {
        if(!JumpBoost)
        {
            PowerUp = true;
            JumpBoost = true;
            this.gameObject.GetComponent<PlayerMovement>().jumpSpeed *= 2;
            powerupUI.text = "NITRO JUMP";
        }
        TimeLeft = 20f;
    }

}
