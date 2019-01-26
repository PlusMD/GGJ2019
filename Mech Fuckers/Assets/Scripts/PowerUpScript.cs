using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{

    public GameObject RepairKit;

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
        //int Power = Random.Range(0, 1);
        int Power = 0;
        switch (Power)
        {
            case 0:
                Instantiate(RepairKit, Location, Quaternion.identity);
                break;
            case 1:
                break;
            default:
                Debug.Log("Somethings fucked up lads");
                break;
        }
    }

}
