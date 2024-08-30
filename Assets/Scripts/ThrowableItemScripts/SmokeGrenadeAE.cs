using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGrenadeAE : ThrowableItemAE
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

        StartCoroutine(SmokeExplosionCoroutine());
    }

    private IEnumerator SmokeExplosionCoroutine()
    {
        yield return new WaitForSeconds(grenadeData.timeUntilExplosion);

        ParticleSystem effectParticleSystem = Instantiate(grenadeData.explosionEffect, transform.position, Quaternion.Euler(Vector3.up));

        effectParticleSystem.Play();

        Destroy(this.gameObject);

    }
}

