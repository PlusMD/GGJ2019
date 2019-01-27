using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeRegen : MonoBehaviour
{
    public PlayerHealth health;
    public ParticleSystem effects;
    bool healing = false;

    // Start is called before the first frame update
    void Start()
    {
        var em = effects.emission;
        em.enabled = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            healing = true;
            var em = effects.emission;
            em.enabled = true;
        }
    }

    void Update()
    {
        if (healing == true) {
            effects.transform.position = health.transform.position;

            if (health.currentHealth < health.maxHealth)
            {
                health.currentHealth += Time.deltaTime * 2;
                health.healthBar.fillAmount = health.currentHealth / health.maxHealth;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            healing = false;
            var em = effects.emission;
            em.enabled = false;
        }
    }
}
