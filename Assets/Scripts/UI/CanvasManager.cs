using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private GameObject crosshair;
	[SerializeField] private TMP_Text currentWeaponText;
	
	private static CanvasManager canvasManagerInstance;
	public static CanvasManager CanvasManagerInstance { get { return canvasManagerInstance; } }


	void Start()
	{
		PlayerActions.isAds += IsAds;
		PlayerActions.isNotAds += IsNotAds;

        if (canvasManagerInstance != null && canvasManagerInstance != this)
        {
            Destroy(this.gameObject);
        } 
		else
		{
            canvasManagerInstance = this;
        }
    }

	void IsAds()
	{
		// functionality removed because I was told to remove weapon models
		// crosshair.SetActive(false);
	}

	void IsNotAds()
	{
		crosshair.SetActive(true);
	}

	public void DisplayCurrentWeapon(GameObject currentWeapon)
	{
		string displayName = (currentWeapon.TryGetComponent(out WeaponActionExecuter weaponActionExecuter)) ? weaponActionExecuter.InventoryItemData.displayName : currentWeapon.GetComponent<ThrowableItemAE>().InventoryItemData.displayName;
		
		currentWeaponText.text = displayName;

	}
}
