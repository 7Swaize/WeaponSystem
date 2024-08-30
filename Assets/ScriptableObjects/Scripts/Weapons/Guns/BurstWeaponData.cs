using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Burst", menuName = "Weapon/Burst")]


public class BurstWeaponData : GunData
{
	[Header("Burst Configs")]
	public float shotsInBurst;
	public float timeBetweenBurstShots;
	// rpm for burst weapons is essentally the number of bursts. mag size will also be the number of bursts. maybe change this later but for now its this
}
