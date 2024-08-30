using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstAE : WeaponActionExecuter
{
	[Header("InheretedMemberReferences")]
	[SerializeField] private BurstWeaponData burstWeaponData;

	protected override void Awake()
	{
		gunData = burstWeaponData;

		base.Awake();
	}

	protected override void Shoot()
	{
		if (!AbleToShoot() || !gameObject.activeInHierarchy)
		{
			return;
		}

		StartCoroutine(HandleBurstFire());

		timeSinceLastShot = 0f;
		bulletsLeft--;
	}

	private IEnumerator HandleBurstFire()
	{
		for (int lp = 0; lp < burstWeaponData.shotsInBurst; lp++)
		{
			Vector3 spreadVar = CalcSpread();

			if (Physics.Raycast(cam.transform.position, spreadVar, out rayHit, burstWeaponData.range))
			{
				InteractWithHitObject(rayHit.collider.gameObject);

				InstantiateBulletHole(rayHit);
			}

			RenderBulletTracers(spreadVar);

			PlayMuzzleFlash();

			yield return new WaitForSeconds(burstWeaponData.timeBetweenBurstShots);
		}
	}
	
}
