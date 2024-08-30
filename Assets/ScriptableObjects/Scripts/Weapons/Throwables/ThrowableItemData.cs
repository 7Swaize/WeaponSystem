using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableItemData : ScriptableObject
{
	[Header("Throwable Configs")]
	public float damage;
	public float throwForce;
	public float throwUpwardsForce;
	public int totalThrows;

	[Header("ThrowableItemPositionOffsets")]
	public Vector3 hipfirePosOffset;

	[Header("Audio Configs")]
	public AudioClip throwableItemAudioClip;
}
