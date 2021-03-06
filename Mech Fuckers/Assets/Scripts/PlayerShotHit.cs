﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotHit : MonoBehaviour
{
    public int Damage;
    public ParticleSystem ParticleGun;
    public ParticleSystem WoodSpray;

    List<ParticleCollisionEvent> Collisions = new List<ParticleCollisionEvent>();

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Helicopter")
        {
            other.GetComponent<HelicopterAI>().TakeDamage(Damage);
        }

        if (other.tag == "Bomb Tank")
        {
            other.GetComponent<TankBombAI>().TakeDamage(Damage);
        }

        if (other.tag == "Main Tank")
        {
            other.GetComponent<MainTankAI>().TakeDamage(Damage);
        }

        if (other.tag == "Drone")
        {
            other.GetComponent<DroneAI>().TakeDamage(Damage);
        }

        if (other.tag == "Superheavy")
        {
            other.GetComponent<SuperTankAI>().TakeDamage(Damage);
        }

        /*if (other.tag == "Tree")
        {
            ParticlePhysicsExtensions.GetCollisionEvents(ParticleGun, other, Collisions);
            for(int i = 0; i < Collisions.Count; i++)
            {
                EmitWoodSpray(Collisions[i]);
            }
            other.GetComponent<TreeHealth>().LoseHealth(Damage);
        }*/
    }

    /*void EmitWoodSpray(ParticleCollisionEvent CollisionEvent)
    {
        WoodSpray.transform.position = CollisionEvent.intersection;
        WoodSpray.transform.rotation = Quaternion.LookRotation(CollisionEvent.normal);
        WoodSpray.Emit(1);
    }*/

}
