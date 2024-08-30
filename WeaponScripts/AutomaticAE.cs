using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticAE : WeaponActionExecuter
{
	protected override void Shoot()
	{
		if (!AbleToShoot())
		{
			return;
		}

		Vector3 spreadVar = CalcSpread();

		if (Physics.Raycast(cam.transform.position, spreadVar, out rayHit, gunData.range))
		{
			InteractWithHitObject(rayHit.collider.gameObject);

			InstantiateBulletHole(rayHit);
		}

		PlayMuzzleFlash();

		RenderBulletTracers(spreadVar);

		timeSinceLastShot = 0f;
		bulletsLeft--;
	}
}
