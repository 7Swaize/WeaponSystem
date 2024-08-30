using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrackShot", menuName = "Weapon/TrackShot")]

public class TrackShotData : GunData
{
	[Header("TrackShotReferences")]
	public GameObject trackShotPrefab;

	[Header("TrackShotConfigs")]
	public float bulletSpeed;
	public float bulletLifetime;
}
