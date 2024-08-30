using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Molotov", menuName = "Weapon/ThrowableItem/Molotov")]

public class MolotovData : GrenadeData
{
    [Header("Molotov Configs")]
    public float explosionBurnDuration;
    public float damageTickRate;
}
