using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroTankFound : MonoBehaviour
{
    public Material materialDeath;
    public GameObject item; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().FoundNitro();
          
        }
        //start death
        StartCoroutine(Die());


     

        //Destroy(gameObject);
    }

    IEnumerator Die()
    {
        item.GetComponent<MeshRenderer>().material = materialDeath;
        yield return new WaitForSeconds(0.2f); 
        Destroy(gameObject); 
    }
}
