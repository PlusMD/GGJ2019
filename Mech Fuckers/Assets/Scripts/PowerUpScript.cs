using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{

    public GameObject RepairKit;
    public GameObject Battery;
    public GameObject Nitro;

    public void RandomSpawnChance(Vector3 Location)
    {
        int Chance = Random.Range(0, 50);
        if(Chance == 10)
        {
            WhichPowerUp(Location);
        }
    }

    void WhichPowerUp(Vector3 Location)
    {
        int Power = Random.Range(0, 2);
        //Power = 1;
        switch (Power)
        {
            case 0:
                Instantiate(RepairKit, Location, Quaternion.identity);
                break;
            case 1:
                Instantiate(Battery, Location, Quaternion.identity);
                break;
            case 2:
                Instantiate(Nitro, Location, Quaternion.identity);
                break;
            default:
                Debug.Log("Somethings fucked up lads");
                break;
        }
    }

}
