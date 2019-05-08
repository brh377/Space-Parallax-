using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TestGunShoot : MonoBehaviour
{
    //VR Setup
    public SteamVR_Input_Sources handtype;
    public SteamVR_Action_Boolean shoot;
    //////////////////////

    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;

    public Camera fpscamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public Transform gunEnd;

    public ParticleSystem cartridgeEjection;

    //public GameObject metalHitEffect;


    public GameObject Particle;


    private float nextFire;                                             // Float to store the time the player will be allowed to fire again, after firing
    private Animator anim;
    //private GunAim gunAim;
    private AudioSource Audio;


    void Start()
    {
        anim = GetComponent<Animator>();
        //gunAim = GetComponentInParent<GunAim>();
        Audio = GetComponent<AudioSource>();

        // Update is called once per frame
        void Update()
        {
            if (shoot.GetState(handtype))
            {
                Shoot();
                Debug.Log("FIRED");

            }

            
        }

        void Shoot()
        {
            muzzleFlash.Play();
            RaycastHit hit;

            if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);

                }

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                impactGO.GetComponent<ParticleSystem>().Play();
                Destroy(impactGO, 2f);
            }
        }
    }
}
