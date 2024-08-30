
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponActionExecuter : MonoBehaviour
{
	[Header("References")]
	[SerializeField] protected GunData gunData;
	[SerializeField] protected Camera cam;
	[SerializeField] protected Transform attackPoint;

	[SerializeField] protected Transform ADSPosition;
	[SerializeField] protected Transform hipFirePosition;

	[Header("ItemInfo")]
	[SerializeField] protected InventoryItemData inventoryItemData;

	[Header("Tracers")]
    [SerializeField] protected TrailRenderer bulletTrail;

    [Header("ADS")]
    [SerializeField] protected float aimAnimationSpeed;

    public InventoryItemData InventoryItemData { get; private set; }
	
	protected int bulletsLeft;
	protected int BulletsShot;

	protected float timeSinceLastShot;

	protected bool shooting;
	protected bool reloading;

	protected GameObject mag;

	protected Vector3 originalMagPos;

	protected RaycastHit rayHit;

	protected bool IsAds;

	

	// Abstracts
	protected abstract void Shoot();


	// Other
	protected bool AbleToShoot() => !reloading && bulletsLeft > 0 && (timeSinceLastShot > (1f / (gunData.rpm / 60f)));


	// Main Method Sets
	protected void isAds()
	{
		IsAds = true;

		if(gunData.adsPosOffset == new Vector3(0,0,0))
		{
			if (!(transform.localPosition == ADSPosition.localPosition))
			{
				Vector3 pos = Vector3.Lerp(transform.localPosition, ADSPosition.localPosition, aimAnimationSpeed * Time.deltaTime);
				transform.localPosition = pos;
			}
		}

		else if (!(transform.localPosition == ADSPosition.localPosition + (gunData.adsPosOffset - ADSPosition.localPosition)))
		{
			Vector3 pos = Vector3.Lerp(transform.localPosition, ADSPosition.localPosition + (gunData.adsPosOffset - ADSPosition.localPosition), aimAnimationSpeed * Time.deltaTime);
			transform.localPosition = pos;
		}
	}


	protected void isNotAds()
	{
		IsAds = false;

		if (!(transform.localPosition == hipFirePosition.localPosition + gunData.hipfirePosOffset))
		{
			Vector3 pos = Vector3.Lerp(transform.localPosition, hipFirePosition.localPosition + gunData.hipfirePosOffset, aimAnimationSpeed * Time.deltaTime);
			transform.localPosition = pos;
		}
	}


	protected virtual void Awake()
	{
		//subscribe to events
		PlayerActions.shootInput += Shoot;
		PlayerActions.reloadInput += StartReload;

		PlayerActions.isAds += isAds;
		PlayerActions.isNotAds += isNotAds;
		
		// set vars
		bulletsLeft = gunData.magSize;

		// set references
		cam = GameObject.Find("MainCamera").GetComponent<Camera>();

		ADSPosition = GameObject.Find("ADSPosition").transform;
		hipFirePosition = GameObject.Find("HipFirePosition").transform;

		originalMagPos = transform.Find("Mag").localPosition;
		mag = transform.Find("Mag").gameObject;

	}

	protected void Update()
	{
		timeSinceLastShot += Time.deltaTime;	
	}


	protected void OnDisable()
	{
		// stops reload coroutine on weapon swap
        if (Reload() != null)
        {
            StopCoroutine(Reload());
            reloading = false;
        }
	}

	protected void OnDestroy()
	{
		PlayerActions.shootInput -= Shoot;
		PlayerActions.reloadInput -= StartReload;

		PlayerActions.isAds -= isAds;
		PlayerActions.isNotAds -= isNotAds;
	}

	protected void InteractWithHitObject(GameObject hitObj)
	{
		if (hitObj.TryGetComponent(out IDamageable damageableObj))
		{
			damageableObj.Damage(gunData.damage);
		}
	}

	protected Vector3 CalcSpread()
	{
		if (!IsAds)
		{
			float xSpread = Random.Range(-gunData.spread, gunData.spread);
			float ySpread = Random.Range(-gunData.spread, gunData.spread);
			float zSpread = Random.Range(-gunData.spread, gunData.spread);

			Vector3 spreadDirection = cam.transform.forward + new Vector3(xSpread, ySpread, zSpread);

			return spreadDirection;
		}
		else
		{
			Vector3 spreadDirection = cam.transform.forward;

			return spreadDirection;	
		}
	}

	protected void StartReload()
	{
		if (bulletsLeft < gunData.magSize && !reloading && gameObject.activeInHierarchy)
		{
			StartCoroutine(Reload());
		}
	}

	protected void PlayMuzzleFlash()
	{
		gunData.muzzleFlash.transform.position = attackPoint.transform.position;

		if (gunData.muzzleFlash.isPlaying) gunData.muzzleFlash.Stop();
		gunData.muzzleFlash.Play();

		SFXManager.instance.PlaySFXClip(gunData.weaponAudioClip, attackPoint, 2f);
	}

	protected void InstantiateBulletHole(RaycastHit rayHit)
	{
		Vector3 distance = rayHit.point - transform.position;
		float angle = Mathf.Atan2(distance.x, distance.z) * Mathf.Rad2Deg;
				
		Instantiate(gunData.bulletHole, rayHit.point, Quaternion.Euler(0, angle + 180, 0));
	}

	protected void RenderBulletTracers(Vector3 spreadVar)
	{

        var bullet = Instantiate(bulletTrail, attackPoint.position, Quaternion.identity);

        bullet.AddPosition(attackPoint.position);
        {
            bullet.transform.position = transform.position + (spreadVar * 200);
        }

	}

	// Couroutines
	protected IEnumerator Reload()
	{
		reloading = true;

		float reloadElapsedTime = 0f;

		while (reloadElapsedTime < gunData.reloadTime / 2)
		{
			float t = reloadElapsedTime / (gunData.reloadTime / 2);
			
			Vector3 lerpedPos = Vector3.Lerp(originalMagPos, gunData.reloadMagPos, t);

			mag.transform.localPosition = lerpedPos;
			
			yield return 0;

			reloadElapsedTime += Time.deltaTime;
			
		}

		reloadElapsedTime = 0f;

		while (reloadElapsedTime < gunData.reloadTime / 2)
		{
			float t = reloadElapsedTime / (gunData.reloadTime / 2);
			
			Vector3 lerpedPos = Vector3.Lerp(gunData.reloadMagPos, originalMagPos, t);

			mag.transform.localPosition = lerpedPos;
			
			yield return 0;

			reloadElapsedTime += Time.deltaTime;
			
		}


		bulletsLeft = gunData.magSize;
		
		reloading = false;
	}

}