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
    public ParticleSystem effects;

    // Start is called before the first frame update
    void Start()
    {
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
}
