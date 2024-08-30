using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]

public class GunData : ScriptableObject
{
	[Header("Gun Configs")]
	public int damage;
	public float range;
	public int magSize;
	public float reloadTime;
	public float rpm;
	public float spread;
	public bool allowWeaponHold;

	[Header("Offset Configs")]
	public Vector3 hipfirePosOffset;
	public Vector3 adsPosOffset;
	public Vector3 reloadMagPos;

	[Header("Weapon Graphics")]
	public ParticleSystem muzzleFlash;
	public GameObject bulletHole;

	[Header("Audio Configs")]
	public AudioClip weaponAudioClip;


}
