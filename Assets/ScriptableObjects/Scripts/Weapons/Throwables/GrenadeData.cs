using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grenade", menuName = "Weapon/ThrowableItem/Grenade")]

public class GrenadeData : ThrowableItemData
{
	[Header("Grenade Configs")]
	public float explosionSize;
	public float timeUntilExplosion;
	public float explosionForce;

	[Header("Graphics")]
	public ParticleSystem explosionEffect;
	public float screenShakeDuration;

}
