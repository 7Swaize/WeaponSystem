using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private float xInput;
	private float zInput;

	Vector3 moveDir;
	Vector3 gravVelocity;

	private float sprintLerpDuration = 0.5f;
	private float sprintLerpElapsedTime;
	private float lerpedSpeed;

	private CharacterController controller;
	
	[Header("Movement Info")]
	[SerializeField] private float moveSpeed;
	[SerializeField] private float jumpHeight;
	[SerializeField] private float sprintSpeed;
	[SerializeField] private float crouchHeight;

	[Header("Key Binds")]
	[SerializeField] private KeyCode sprint;
	[SerializeField] private KeyCode jump;
	[SerializeField] private KeyCode crouch;

	[Header("Physics Simulation Info")]
	[SerializeField] private float gravity = -9.81f;

	[Header("GroundDetection")]
	[SerializeField] LayerMask groundMask;
	float groundDistance = 0.4f;
	bool isGrounded;

	void Start()
	{
		controller = GetComponent<CharacterController>();	
	}

	void Update()
	{
		xInput = Input.GetAxisRaw("Horizontal");
		zInput = Input.GetAxisRaw("Vertical");

		CheckIfGrounded();
		HandleJumps();
		HandleMovement();
		HandleCrouching();
	}

	void HandleCrouching()
	{
		if (Input.GetKey(crouch))
		{
			controller.height = Mathf.Lerp(controller.height, crouchHeight, Time.deltaTime * 6f);
		}
		else
		{
			// two is the original height. this must be changed if you change the height of the character controller in the inspector.
			controller.height = Mathf.Lerp(controller.height, 2f, Time.deltaTime * 6f);
		}
	}

	void HandleJumps()
	{
		if (Input.GetKeyDown(jump) && isGrounded)
		{
			gravVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}
	}

	// Ik its gross clean up later
	void HandleMovement()
	{
		moveDir = transform.right * xInput + transform.forward * zInput;

		// handles lerping of sprint speed and then moving character
		if (Input.GetKey(sprint))
		{
			if (sprintLerpElapsedTime < sprintLerpDuration && lerpedSpeed != sprintSpeed)
			{
				float t = sprintLerpElapsedTime / sprintLerpDuration;
				lerpedSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, t);

				controller.Move(moveDir * lerpedSpeed * Time.deltaTime);
				sprintLerpElapsedTime += Time.deltaTime;
			}
			else
			{
				lerpedSpeed = sprintSpeed;
				controller.Move(moveDir * lerpedSpeed * Time.deltaTime);

				sprintLerpElapsedTime = 0f;
			}
		}
		else
		{
			controller.Move(moveDir * moveSpeed * Time.deltaTime);
			lerpedSpeed  = moveSpeed;
		}
		
		// handles gravity
		gravVelocity.y += gravity * (Time.deltaTime);
		controller.Move(gravVelocity * Time.deltaTime);
	}

	void CheckIfGrounded()
	{
		//  calculates transform.position of the bottom of the controller collider
		Vector3 groundCheck = new Vector3(transform.position.x, transform.position. y - (controller.height / 2), transform.position.z);

		isGrounded = Physics.CheckSphere(groundCheck, groundDistance, groundMask.value);

        if(isGrounded && gravVelocity.y < 0)
        {
            gravVelocity.y = -2f;
        }
	}
}


