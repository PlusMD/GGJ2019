using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelicopterAI : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public Transform gattlingGun;
    public float range = 20.0f;
    public float health = 50.0f;
    public ParticleSystem effects;
    public GameObject damageSparks;
    public ParticleSystem explosion;

    public GameObject PowerUps;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        var em = effects.emission;
        em.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        gattlingGun.LookAt(player);

        float dist = Vector3.Distance(player.position, gattlingGun.position);
        if (dist <= range)
        {
            var em = effects.emission;
            em.enabled = true;
        }
        else {
            var em = effects.emission;
            em.enabled = false;
        }
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
            explosion.Play();
            GameObject.Destroy(this.gameObject);
            PowerUps.GetComponent<PowerUpScript>().RandomSpawnChance(transform.position);
        }
    }
}
