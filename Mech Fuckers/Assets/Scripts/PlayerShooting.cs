﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public bool primary = true;
    public ParticleSystem effects;
    public ParticleSystem muzzle;

    // Start is called before the first frame update
    void Start()
    {
        var em = effects.emission;
        em.enabled = false;

        var emmuzzle = muzzle.emission;
        emmuzzle.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (primary == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var em = effects.emission;
                em.enabled = true;

                var emmuzzle = muzzle.emission;
                emmuzzle.enabled = true;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                var em = effects.emission;
                em.enabled = false;

                var emmuzzle = muzzle.emission;
                emmuzzle.enabled = false;
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                var em = effects.emission;
                em.enabled = true;

                var emmuzzle = muzzle.emission;
                emmuzzle.enabled = true;
            }

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                var em = effects.emission;
                em.enabled = false;

                var emmuzzle = muzzle.emission;
                emmuzzle.enabled = false;
            }
        }
    }
}