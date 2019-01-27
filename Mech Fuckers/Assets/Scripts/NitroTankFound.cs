using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroTankFound : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().FoundNitro();
        }
        Destroy(gameObject);
    }
}
