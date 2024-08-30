using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenadeAE : ThrowableItemAE
{
	[Header("InheretedMemberReferences")]
	[SerializeField] private GrenadeData grenadeData;


	protected override void Awake()
	{
		throwableItemData = grenadeData;

		base.Awake();
	}

	protected override void Throw()
	{
		if (!AbleToThrow())
		{
			return;
		}

		base.Throw();

		PlayerActions.shootInput -= Throw;

		StartCoroutine(ExplosionCoroutine());
	}

	private IEnumerator ExplosionCoroutine()
	{
		yield return new WaitForSeconds(grenadeData.timeUntilExplosion);


		Collider[] objects = Physics.OverlapSphere(transform.position, grenadeData.explosionSize);

		foreach (Collider obj in objects)
		{
			if (obj.gameObject.TryGetComponent(out IDamageable damageableObj))
			{
				damageableObj.Damage(grenadeData.damage);
			}

			if (obj.gameObject.TryGetComponent(out IExplosionForce expForce))

			{
				expForce.AddExplosionForce(gameObject.transform, grenadeData.explosionForce);
			}
		}

		ParticleSystem effectParticleSystem = Instantiate(grenadeData.explosionEffect, transform.position, Quaternion.Euler(Vector3.up));
		effectParticleSystem.Play();

		SFXManager.instance.PlaySFXClip(grenadeData.throwableItemAudioClip, gameObject.transform, .05f);

		HandleRest();

	}

	private void HandleRest()
	{
		gameObject.transform.SetParent(weaponHolder.transform);

		GameObject clone = Instantiate(gameObject);
		clone.transform.SetParent(weaponHolder.transform);
		clone.SetActive(true);

		Destroy(gameObject);
	}
}
