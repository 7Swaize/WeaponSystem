using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour, IExplosionForce
{
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void AddExplosionForce(Transform explodingObj, float force)
	{
		Vector3 direction = explodingObj.position - gameObject.transform.position;

		rb.AddForce(-direction * force, ForceMode.Impulse);
	}
}
