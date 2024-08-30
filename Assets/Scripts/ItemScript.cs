using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour, IInteractable
{
	[Header("References")]
	[SerializeField] private InventoryItemData inventoryItemData;

	GameObject parentObj;


	public void Interact(string tagOfDesiredObjectInHierarchy)
	{
		parentObj = GameObject.FindWithTag(tagOfDesiredObjectInHierarchy);

		if (parentObj.transform.childCount == 0)
		{
			SetNewWeapon(0, true);
		}

		else if (parentObj.transform.childCount == 1)
		{
			SetNewWeapon(1, false);
		}

		else
		{
			// find what weapon the player is currently holding
			for (int i = 0; i < parentObj.transform.childCount; i++)
			{
				if (parentObj.transform.GetChild(i).gameObject.activeInHierarchy)
				{
					GameObject currentWeapon = parentObj.transform.GetChild(i).gameObject;

					// replace the object you currently have with the one you want
					SetNewWeapon(i, true);

					// unchild and destroy the object you dropped
					currentWeapon.transform.SetParent(null);
					Destroy(currentWeapon);

					break;
				}
			}
		}
	}

	private void SetNewWeapon(int siblingIndex, bool setActive)
	{
		GameObject newWeapon = Instantiate(inventoryItemData.functionalWeaponPrefab);
		newWeapon.transform.SetParent(parentObj.transform);
		newWeapon.transform.SetSiblingIndex(siblingIndex);
		newWeapon.SetActive(setActive);
	}
}
