using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableItemAE : MonoBehaviour
{
	[Header("References")]
	[SerializeField] protected ThrowableItemData throwableItemData;
	[SerializeField] protected Camera cam;
	[SerializeField] protected Transform hipfirePos;
	[SerializeField] protected Transform weaponHolder;
	
	[Header("ItemInfo")]
	[SerializeField] protected InventoryItemData inventoryItemData;


	public InventoryItemData InventoryItemData { get; set; }

	protected bool readyToThrow = true;
	protected bool thrown = false;

	protected int throwsLeft;

	protected Rigidbody rb;


	// Other
	protected bool AbleToThrow() => readyToThrow && throwsLeft > 0 && gameObject.activeInHierarchy && this != null;


	// Main Method Sets
	protected virtual void Awake()
	{
		// subscribe to actions
		PlayerActions.shootInput += Throw;

		// set references
		cam = GameObject.Find("MainCamera").GetComponent<Camera>();
		hipfirePos = GameObject.Find("HipFirePosition").transform;
		weaponHolder = GameObject.Find("WeaponHolder").transform;
		rb = GetComponent<Rigidbody>();


		rb.isKinematic = true;
		throwsLeft = throwableItemData.totalThrows;

	}

	protected void Update()
	{
		if (!thrown) transform.localPosition = hipfirePos.localPosition - throwableItemData.hipfirePosOffset;
	}

	protected virtual void Throw()
	{
		readyToThrow = false;
		thrown = true;
		rb.isKinematic = false;

		transform.parent = null;

		Vector3 forceToAdd = CalcThrowDirection() * throwableItemData.throwForce + cam.transform.up * throwableItemData.throwUpwardsForce;

		rb.AddForce(forceToAdd, ForceMode.Impulse);
	}

	protected Vector3 CalcThrowDirection()
	{
		Vector3 forceDirection = cam.transform.forward;

		RaycastHit rayHit;

		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit, 500f))
		{
			forceDirection = (rayHit.point - (transform.position)).normalized;
		}

		return forceDirection;
	}

	void OnDestroy()
	{
		PlayerActions.shootInput -= Throw;
	}
}
