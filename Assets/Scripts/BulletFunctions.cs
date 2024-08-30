using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFunctions : MonoBehaviour
{
	public enum State {Track, StraightShot}

	public State bulletState {get; set;}

	public float bulletSpeed {get; set;}
	public float damage {get; set;}
	public float lifetime {get; set;}
	
	public RaycastHit rayHit {get; set;}

	private float elapsedLifeTime = 0f;


	private void Update()
	{
		if (bulletState == State.Track)
		{
			transform.position = Vector3.MoveTowards(transform.position, rayHit.collider.transform.position, bulletSpeed * Time.deltaTime);

			if (Vector3.Distance(transform.position, rayHit.collider.transform.position) <= 1 && Vector3.Distance(transform.position, rayHit.collider.transform.position) >= -1)
			{
				if (rayHit.collider.gameObject.TryGetComponent(out IDamageable damageableObj))
				{
					damageableObj.Damage(damage);
				}

				Destroy(gameObject);
			}
		}

		if (bulletState == State.StraightShot)
		{
			transform.position = Vector3.MoveTowards(transform.position, rayHit.point, bulletSpeed * Time.deltaTime);

			elapsedLifeTime += Time.deltaTime;

			if (elapsedLifeTime >= lifetime) {Destroy(gameObject);}
		}
	}
	
}
