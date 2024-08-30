using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Weapon/Shotgun")]

public class ShotgunData : GunData
{
	[Header("Shotgun Configs")]
	public float spreadConeRadius;
	public float adsSpreadConeRadius;
	public float numPellets;
}
