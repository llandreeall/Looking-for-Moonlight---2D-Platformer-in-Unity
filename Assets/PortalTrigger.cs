using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{

	public GameObject portal;
	public Transform _sp;

	private void OnTriggerEnter2D(Collider2D coll)
	{
		Instantiate(portal, _sp.position, _sp.rotation);
		
		GameObject.Destroy(gameObject);
	}
}
