using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public int SpawnChance;
    public GameObject RepairKit;
    public GameObject Battery;
    public GameObject Nitro;
    public GameObject Jetpack;

    public void RandomSpawnChance(Vector3 Location)
    {
        int Chance = Random.Range(0, SpawnChance);
        if(Chance == 1)
        {
            WhichPowerUp(Location);
        }
    }

    void WhichPowerUp(Vector3 Location)
    {
        int Power = Random.Range(0, 10);
        if(Power <= 3)
        {
            Instantiate(RepairKit, Location, Quaternion.identity);
        }
        else if(Power == 4 || Power == 5)
        {
            Instantiate(Battery, Location, Quaternion.identity);
        }
        else if(Power == 6 || Power == 7)
        {
            Instantiate(Nitro, Location, Quaternion.identity);
        }
        else if(Power == 8 || Power == 9)
        {
            Instantiate(Jetpack, Location, Quaternion.identity);
        }
    }
}
