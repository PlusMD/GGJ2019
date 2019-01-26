using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHealth : MonoBehaviour
{

    public int Health;

    public void LoseHealth(int Damage)
    {
        Health -= Damage;
        if(Health < 0)
        {
            TreeDeath();
        }
    }

    public void TreeDeath()
    {
        Destroy(gameObject);
    }

}
