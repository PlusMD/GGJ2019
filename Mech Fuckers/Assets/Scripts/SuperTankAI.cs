using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuperTankAI : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;

    public Transform turretFront;

    public Transform turretBack;

    public Transform turretBig;


    public float range = 20.0f;
    public float health = 50.0f;
    public ParticleSystem effectsTurretFront;
    public ParticleSystem effectsTurretBack;
    public ParticleSystem effectsTurretBig;
    public GameObject damageSparks;
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        var emF = effectsTurretFront.emission;
        emF.enabled = false;

        var emB = effectsTurretBack.emission;
        emB.enabled = false;

        var emBig = effectsTurretBig.emission;
        emBig.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);

        turretFront.LookAt(player);
        turretBack.LookAt(player);
        turretBig.LookAt(player);

        float dist = Vector3.Distance(player.position, transform.position);
        if (dist <= range)
        {
            var emF = effectsTurretFront.emission;
            emF.enabled = true;

            var emB = effectsTurretBack.emission;
            emB.enabled = true;

            var emBig = effectsTurretBig.emission;
            emBig.enabled = true;
        }
        else
        {
            var emF = effectsTurretFront.emission;
            emF.enabled = false;

            var emB = effectsTurretBack.emission;
            emB.enabled = false;

            var emBig = effectsTurretBig.emission;
            emBig.enabled = false;
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
        }
    }
}
