using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using Valve.VR;

public class GunShoot : MonoBehaviour {

	public float fireRate = 0.25f;										// Number in seconds which controls how often the player can fire
	public float weaponRange = 20f;										// Distance in Unity units over which the player can fire
    //public float damage = 10f;

	//VR Setup
	public SteamVR_Input_Sources handtype;
	public SteamVR_Action_Boolean shoot;
	//////////////////////

	public Transform gunEnd;
	public ParticleSystem muzzleFlash;
	public ParticleSystem cartridgeEjection;

	public GameObject metalHitEffect;
	

	public GameObject Particle;


	private float nextFire;												// Float to store the time the player will be allowed to fire again, after firing
	private Animator anim;
	//private GunAim gunAim;
    private AudioSource Audio;
    private float damage = 10f;

    void Start () 
	{
		anim = GetComponent<Animator> ();
		//gunAim = GetComponentInParent<GunAim>();
        Audio = GetComponent<AudioSource>();

		
		
	}

	void Update () 
	{
		if (shoot.GetStateDown(handtype)) 
		{
            
			nextFire = Time.time + fireRate;
			muzzleFlash.Play();
			cartridgeEjection.Play();
			anim.SetTrigger ("Fire");
			Debug.Log("FIRED");


			Vector3 rayOrigin = gunEnd.position;
			RaycastHit hit;
			if (Physics.Raycast(rayOrigin, gunEnd.forward, out hit, weaponRange))
			{
                Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
				HandleHit(hit);
			}
		}
	}

	void HandleHit(RaycastHit hit)
	{
		if(hit.collider.sharedMaterial != null)
		{
			string materialName = hit.collider.sharedMaterial.name;

			switch(materialName)
			{
				case "Metal":
					SpawnDecal(hit, metalHitEffect);
					break;
				
            }
		}
		SpawnDecal(hit,Particle);
        if(Audio != null)
        {
            Audio.Play();
        }
        
	}

	void SpawnDecal(RaycastHit hit, GameObject prefab)
	{
		GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
		spawnedDecal.transform.SetParent(hit.collider.transform);
	}
}