using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackFound : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().FoundJetPack();
        }
        Destroy(gameObject);
    }
}
