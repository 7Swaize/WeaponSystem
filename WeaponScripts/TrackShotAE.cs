using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackShotAE : WeaponActionExecuter
{
	[Header("InheretedMemberReferences")]
	[SerializeField] private TrackShotData trackShotData;

	private RaycastHit rayHit;


	protected override void Awake()
	{
		gunData = trackShotData;

		base.Awake();
	}

	protected override void Shoot()
	{
		if (!AbleToShoot())
		{
			return;
		}

		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit, trackShotData.range))
		{
			if (rayHit.collider.gameObject.TryGetComponent(out IDamageable damageableObj))
			{
				InstantiateBullet(true);
			}
			else
			{
				InstantiateBullet(false);
			}
		}
		
		else 
		{ 
			InstantiateBullet(false);
		}

		PlayMuzzleFlash();

		timeSinceLastShot = 0f;
		bulletsLeft--;
	}

	private void InstantiateBullet(bool track)
	{
		GameObject trackShot = Instantiate(trackShotData.trackShotPrefab, attackPoint.position, cam.transform.rotation);
		BulletFunctions trackShotBulletFunction = trackShot.GetComponent<BulletFunctions>();

		trackShotBulletFunction.damage = trackShotData.damage;
		trackShotBulletFunction.bulletSpeed = trackShotData.bulletSpeed;
		trackShotBulletFunction.rayHit = rayHit;
		trackShotBulletFunction.lifetime = trackShotData.bulletLifetime;
		

		trackShotBulletFunction.bulletState = (track) ? BulletFunctions.State.Track : BulletFunctions.State.StraightShot;
	}
}
