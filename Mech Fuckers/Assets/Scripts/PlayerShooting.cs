using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public AudioClip sound1;
    private AudioSource source;

    public bool primary = true;
    public ParticleSystem effects;
    public ParticleSystem muzzle;
    float AudioTime1 = 0f;
    float AudioTime2 = 0f;

    // Start is called before the first frame update
    void Start()
    {
        var em = effects.emission;
        em.enabled = false;

        var emmuzzle = muzzle.emission;
        emmuzzle.enabled = false;
    }

    void Awake()
    {

        source = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        if (primary == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                source.Play(0);
                var em = effects.emission;
                em.enabled = true;

                var emmuzzle = muzzle.emission;
                emmuzzle.enabled = true;                          
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                source.Stop();
                AudioTime1 = 0f;
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
                source.Play(0);
                AudioTime2 -= Time.deltaTime;
                var em = effects.emission;
                em.enabled = true;

                var emmuzzle = muzzle.emission;
                emmuzzle.enabled = true;
            }

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                source.Stop();
                AudioTime2 = 0f;
                var em = effects.emission;
                em.enabled = false;

                var emmuzzle = muzzle.emission;
                emmuzzle.enabled = false;
            }
        }
        /*if(Input.GetKey(KeyCode.Mouse0))
        {        
            AudioTime1 -= Time.deltaTime;
            if(AudioTime1 <= 0)
            {
                source.Play(0);
                AudioTime1 = 0.5f;
            }
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (AudioTime2 <= 0)
            {
                source.Play(0);
                AudioTime2 = 0.5f;
            }
        }*/
    }
}
