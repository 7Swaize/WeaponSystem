using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerActions : MonoBehaviour
{
	[Header("WeaponEvents")]
	public static Action shootInput;
	public static Action reloadInput;
	public static Action isAds;
	public static Action isNotAds;
	public static Action weaponOneInput;
	public static Action weaponTwoInput;
	public static Action weaponThreeInput;
	public static Action weaponFourInput;
	public static Action weaponFiveInput;
	public static Action weaponSixInput;
	public static Action weaponSevenInput;


	[Header("MovementEvents")]
	public static Action playerSprint;

	[Header("InterationEvents")]
	public static Action interactInput;

	[Header("Key Bindings")]
	[SerializeField] private KeyCode reloadKey;
	[SerializeField] private KeyCode weaponOneKey;
	[SerializeField] private KeyCode weaponTwoKey;
	[SerializeField] private KeyCode weaponThreeKey;
	[SerializeField] private KeyCode weaponFourKey;
	[SerializeField] private KeyCode weaponFiveKey;
	[SerializeField] private KeyCode weaponSixKey;
	[SerializeField] private KeyCode weaponSevenKey;
 	[SerializeField] private KeyCode playerSprintKey;
	[SerializeField] private KeyCode interactKey;


	void Update()
	{
		CheckWeaponInputs();
		CheckMovementInputs();
		CheckInterationInputs();
	}

	void CheckWeaponInputs()
	{
		// handles Ads
		if (Input.GetMouseButton(1))
		{
			isAds?.Invoke();
		}
		else
		{
			isNotAds?.Invoke();
		}

		// handles shoot input
		if (Input.GetMouseButton(0))
		{
			shootInput?.Invoke();
		}

		// handles reload input
		if (Input.GetKeyDown(reloadKey))
		{
			reloadInput?.Invoke();
		}

		//handles weapon switching
		if (Input.GetKeyDown(weaponOneKey))
		{
			weaponOneInput?.Invoke();
		}

		if (Input.GetKeyDown(weaponTwoKey))
		{
			weaponTwoInput?.Invoke();
		}
		
		if (Input.GetKeyDown(weaponThreeKey))
		{
			weaponThreeInput?.Invoke();
		}
		
		if (Input.GetKeyDown(weaponFourKey))
		{
			weaponFourInput?.Invoke();
		}
		
		if (Input.GetKeyDown(weaponFiveKey))
		{
			weaponFiveInput?.Invoke();
		}

		if (Input.GetKeyDown(weaponSixKey))
		{
			weaponSixInput?.Invoke();
		}	

		if (Input.GetKeyDown(weaponSevenKey))
		{
			weaponSevenInput?.Invoke();
		}
	}

	void CheckMovementInputs()
	{
		if (Input.GetKey(playerSprintKey))
		{
			playerSprint?.Invoke();
		}
	}

	void CheckInterationInputs()
	{
		if (Input.GetKeyDown(interactKey))
		{
			interactInput?.Invoke();
		}
	}
}
