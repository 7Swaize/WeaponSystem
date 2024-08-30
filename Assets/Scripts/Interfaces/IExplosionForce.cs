using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExplosionForce
{
	public void AddExplosionForce(Transform explodingObj, float force);
}
