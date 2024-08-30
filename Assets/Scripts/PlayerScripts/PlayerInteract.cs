using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
	[Header("InteractionSettings")]
	[SerializeField] private float interactionDistance;

	private Camera cam;

	RaycastHit rayHit;

	
	void Start()
	{
		// subscribe to events
		PlayerActions.interactInput += CheckInteraction;

		cam = GetComponentInChildren<Camera>();
	}

	void Update()
	{
		Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit, interactionDistance);

		if (rayHit.collider != null)
		{
			DisplayInteractGUI(rayHit.collider.gameObject);
		}
	}

	private void CheckInteraction()
	{
		if (rayHit.collider == null)
		{
			return;
		}

		if(rayHit.collider.gameObject.TryGetComponent(out IInteractable Interactable) && 
			rayHit.collider.gameObject.transform.parent != null && 
				!CheckIfObjectInInventory(rayHit.collider.gameObject))
		{
			Interactable.Interact("WeaponHolder");
		}
	}

	private void DisplayInteractGUI(GameObject interactObj)
	{
		if (interactObj.TryGetComponent(out IInteractable Interactable))
		{
			// display graphical stuff
			// just the "E to interact" prompt. No inventory bar cause thats to much work
		}
	}

	private bool CheckIfObjectInInventory(GameObject hitObj)
	{
		GameObject parentObj = GameObject.FindWithTag("WeaponHolder");

		if (parentObj.transform.childCount == 0)
		{
			return false;
		}

		for (int i = 0; i < parentObj.transform.childCount; i++)
		{
			if (parentObj.transform.GetChild(i).gameObject == hitObj)
			{
				return true;
			}
		}

		return false;
	}
}
