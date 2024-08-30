using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
	[Header("Sway Configs")]
	[SerializeField] private float intensity;
	[SerializeField] private float smoothing;
	//Smoothing is how fast you want the weapon to reach said location.

	private float mouseX;
	private float mouseY;

	private Quaternion originRotation;

	// has to be called before weapon swap or else you get lerp back to original position (double lerp visually)
	void Awake()
	{
		originRotation = transform.localRotation;
	}

	void Update()
	{
		UpdateSway();
	}

	private void UpdateSway()
	{
		mouseX = Input.GetAxisRaw("Mouse X");
		mouseY = Input.GetAxisRaw("Mouse Y");

		// calcs target rotation
		Quaternion xAdjustment = Quaternion.AngleAxis(-intensity * mouseX, Vector3.up);
		Quaternion yAdjustment = Quaternion.AngleAxis(intensity * mouseY, Vector3.right);
		Quaternion targetRotation = originRotation * xAdjustment * yAdjustment;
		
		// rotate towards target rotation
		transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smoothing);
	}

}
