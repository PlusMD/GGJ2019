using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneAI : MonoBehaviour
{
    public Transform objective;
    public NavMeshAgent agent;
    public float range = 20.0f;
    public float damage = 5f;
    public float health = 50.0f;
    public ParticleSystem effects;
    bool exploded = false;
    public GameObject damageSparks;
    public ParticleSystem explosion;

    public GameObject PowerUps;

    // Start is called before the first frame update
    void Start()
    {
        objective = GameObject.FindGameObjectWithTag("Player").transform;
        var em = effects.emission;
        em.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(objective.position);

        float dist = Vector3.Distance(objective.position, transform.position);
        if (dist <= range && exploded == false)
        {
            exploded = true;
            objective.GetComponent<PlayerHealth>().TakeDamage(damage);
            explosion.transform.parent = null;
            explosion.gameObject.SetActive(true);
            explosion.Play();

            var em = effects.emission;
            em.enabled = true;
            effects.Play();

            DamageObjective();
        }
    }

    void DamageObjective()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;

        if (health <= 30)
        {
            damageSparks.SetActive(true);
        }

        if (health <= 0)
        {
            explosion.transform.parent = null;
            explosion.gameObject.SetActive(true);
            var em = effects.emission;
            em.enabled = true;
            explosion.Play();
            GameObject.Destroy(this.gameObject);
            PowerUps.GetComponent<PowerUpScript>().RandomSpawnChance(transform.position);
        }
    }
}
