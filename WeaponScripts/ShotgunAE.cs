using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAE : WeaponActionExecuter 
{
	[Header("InheretedMemberReferences")]
	[SerializeField] private ShotgunData shotgunData;

	private Vector3 randPoint;

	protected override void Awake()
	{
		gunData = shotgunData;

		base.Awake();
	}

	protected override void Shoot()
	{
		if (!AbleToShoot())
		{
			return;
			
		}

		for (int lp = 0; lp < shotgunData.numPellets; lp++)
		{
			randPoint = IsAds ? Random.insideUnitSphere.normalized * shotgunData.adsSpreadConeRadius : Random.insideUnitSphere.normalized * shotgunData.spreadConeRadius;


			if (Physics.Raycast(cam.transform.position, cam.transform.forward + randPoint, out rayHit, shotgunData.range))
			{
				InteractWithHitObject(rayHit.collider.gameObject);

				InstantiateBulletHole(rayHit);
			}

			RenderBulletTracers(cam.transform.forward + randPoint);

		}

		PlayMuzzleFlash();

		timeSinceLastShot = 0f;
		bulletsLeft--;
	}
	
}