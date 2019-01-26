using System.Collections;
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
        if (other.tag == "Enemy")
        {

        }
        if(other.tag == "Tree")
        {
            ParticlePhysicsExtensions.GetCollisionEvents(ParticleGun, other, Collisions);
            for(int i = 0; i < Collisions.Count; i++)
            {
                EmitWoodSpray(Collisions[i]);
            }
            other.GetComponent<TreeHealth>().LoseHealth(Damage);
        }
    }

    void EmitWoodSpray(ParticleCollisionEvent CollisionEvent)
    {
        WoodSpray.transform.position = CollisionEvent.intersection;
        WoodSpray.transform.rotation = Quaternion.LookRotation(CollisionEvent.normal);
        WoodSpray.Emit(1);
    }

}
