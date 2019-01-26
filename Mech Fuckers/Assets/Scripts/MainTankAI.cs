using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainTankAI : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public Transform turret;
    public Transform barrel;
    public float range = 20.0f;
    public float health = 50.0f;
    public ParticleSystem effects;
    public GameObject damageSparks;
    public ParticleSystem explosion;
    //private Quaternion smoothTilt;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        var em = effects.emission;
        em.enabled = false;
        //smoothTilt = new Quaternion();
    }

    // Update is called once per frame
    void Update()
    {
        /*RaycastHit rcHit;
        Vector3 theRay = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, theRay, out rcHit, 50f)) {
            float GroundDis = rcHit.distance;
            Quaternion grndTilt = Quaternion.FromToRotation(Vector3.up, rcHit.normal);
            smoothTilt = Quaternion.Slerp(smoothTilt, grndTilt, Time.deltaTime * 2.0f);

            transform.rotation = smoothTilt * transform.rotation;

            Vector3 locPos = transform.localPosition;
            locPos.y = (transform.localPosition.y - GroundDis);
            transform.localPosition = locPos;
        }*/


        agent.SetDestination(player.position);

        turret.LookAt(player);
        //turret.SetPositionAndRotation(turret.position, new Quaternion(0, turret.rotation.y, 0, 0));

        float dist = Vector3.Distance(player.position, barrel.position);
        if (dist <= range)
        {
            var em = effects.emission;
            em.enabled = true;
        }
        else
        {
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
        }
    }
}
