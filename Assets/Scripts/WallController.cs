using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour
{
	public AudioSource bumpSound;


	void OnCollisionEnter(Collision col)
	{
		if (bumpSound != null) 
		{
			bumpSound.volume = col.relativeVelocity.magnitude / 20;
			bumpSound.Play ();
		}
	}
}

