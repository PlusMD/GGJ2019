using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryFoundScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().FoundBattery();
        }
        Destroy(gameObject);
    }
}
