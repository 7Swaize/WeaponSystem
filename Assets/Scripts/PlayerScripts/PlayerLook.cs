using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
	private Camera cam;

	[Header("Sensitivity")]
	[SerializeField] private float sensX;
	[SerializeField] private float sensY;

	private float mouseX;
	private float mouseY;

	private float xRotation;
	private float yRotation;

	void Start()
	{
		cam = GetComponentInChildren<Camera>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update()
	{
		mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime;
		mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime;

		HandleRotation();
	}

	void HandleRotation()
	{
		yRotation += mouseX * sensX;
		xRotation -= mouseY * sensY;

		// xRotation clamped before applying rotation to make sure we cant look to far sown or up. Syntax => xRotation paramter is clamped between -90 and 90.
		xRotation = Mathf.Clamp(xRotation, -90, 90);

		gameObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);
		cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

	}
}

