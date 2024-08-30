using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovAE : ThrowableItemAE
{
    [Header("InheretedMemberReferences")]
    [SerializeField] private MolotovData molotovData;
    
    [Header("Check Cylinder")]
    [SerializeField] private Vector3 cStart;
    [SerializeField] private Vector3 cEnd;
    [SerializeField] private float bHeight;
    [SerializeField] private LayerMask layer;

    private enum State { Held, Thrown, Exploded, }
    private State molotovState;


    protected override void Awake()
    {
        throwableItemData = molotovData;
        molotovState = State.Held;

        base.Awake  ();
    }

    protected override void Throw()
    {
        if (!AbleToThrow())
        {
            return;
        }

        base.Throw();

        PlayerActions.shootInput -= Throw;

        molotovState = State.Thrown;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (molotovState == State.Thrown)
        {
            molotovState = State.Exploded;
            StartCoroutine(ExplosionCoroutine());
        }
    }
    
    private void CheckCylinder(Vector3 cStart, Vector3 cEnd, float cRadius, float bHeight, LayerMask layer)
    {
        if (!Physics.CheckCapsule(transform.position - cStart, transform.position + cEnd, cRadius, layer))
        {
            return;
        }

        if (!Physics.CheckBox(transform.position, new Vector3(cRadius, bHeight, cRadius), Quaternion.LookRotation(Vector3.forward, Vector3.up), layer))
        {
            return;
        }

        Collider[] objects = Physics.OverlapCapsule(cStart - transform.position, cEnd + transform.position, cRadius, layer);

        foreach (Collider obj in objects)
		{
			if (obj.gameObject.TryGetComponent(out IDamageable damageableObj))
			{
				damageableObj.Damage(molotovData.damage);
			}
		}
    }

    // coroutines 
    IEnumerator ExplosionCoroutine()
    {   
        SFXManager.instance.PlaySFXClip(molotovData.throwableItemAudioClip, gameObject.transform, 0.01f);

        for (int i = 0; i < (molotovData.explosionBurnDuration / molotovData.damageTickRate); i++)
        {
            CheckCylinder(cStart, cEnd, molotovData.explosionSize, bHeight, layer);

            yield return new WaitForSeconds(molotovData.damageTickRate);

            ParticleSystem effectParticleSystem = Instantiate(molotovData.explosionEffect, transform.position, Quaternion.Euler(transform.right + new Vector3(-90, 0, 0)));

            effectParticleSystem.Play();
        }

        SFXManager.instance.StopSFXClip();

        Destroy(this.gameObject);
    }
}