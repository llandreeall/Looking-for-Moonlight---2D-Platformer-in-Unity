using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformInteraction : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "MovingPlatform")
		{
			this.transform.parent = other.transform;
		}
	}

	// if the player exits a collision with a moving platform, then unchild it
	void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag == "MovingPlatform")
		{
			this.transform.parent = null;
		}
	}
}
