using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class DoorActivatorMecanim : MonoBehaviour 
{
    private Animator DoorAnimator;
    private AudioSource audio;

	void Start()
	{
        DoorAnimator = GetComponent<Animator> ();
        audio = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider col)
	{
        DoorAnimator.SetTrigger("open");
        audio.Play();

	}
}
