using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] weapons;

    private GameObject currentWeapon;

    private int selectedWeapon;
    private float timeSinceLastSwitch;
    private bool CannotSwap;


    void Start()
    {
        PlayerActions.weaponOneInput += SwapToWeaponOne;
        PlayerActions.weaponTwoInput += SwapToWeaponTwo;
        PlayerActions.weaponThreeInput += SwapToWeaponThree;
        PlayerActions.weaponFourInput += SwapToWeaponFour;
        PlayerActions.weaponFiveInput += SwapToWeaponFive;
        PlayerActions.weaponSixInput += SwapToWeaponSix;
        PlayerActions.weaponSevenInput += SwapToWeaponSeven;

        currentWeapon = Instantiate(weapons[0]);
        WeaponSwap(0);
    }


    void Update()
    {
        timeSinceLastSwitch += Time.deltaTime;
    }

    bool AbleToSwapWeapons() => (timeSinceLastSwitch > 0.25f) && !CannotSwap;



    private void WeaponSwap(int gunSlotValue)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
           Destroy(transform.GetChild(i).gameObject);
            
        }

        int lp = 0;
        timeSinceLastSwitch = 0f;
        

        foreach (GameObject weaponTransform in weapons)
        {
            lp++;

            if (lp == gunSlotValue)
            {
                GameObject previousWeapon = currentWeapon;

                currentWeapon = Instantiate(weaponTransform);
                currentWeapon.transform.SetParent(gameObject.transform);

                CanvasManager.CanvasManagerInstance.DisplayCurrentWeapon(weaponTransform);

                Destroy(previousWeapon);
            }
        }

        lp = 0;

    }


    void SwapToWeaponOne()
    {
        if (AbleToSwapWeapons())
        {
            WeaponSwap(1);
        }
    }

    void SwapToWeaponTwo()
    {
        if (AbleToSwapWeapons())
        {
            WeaponSwap(2);
        }
    }

    void SwapToWeaponThree()
    {
        if (AbleToSwapWeapons())
        {
            WeaponSwap(3);
        }
    }
        void SwapToWeaponFour()
    {
        if (AbleToSwapWeapons())
        {
            WeaponSwap(4);
        }
    }
        void SwapToWeaponFive()
    {
        if (AbleToSwapWeapons())
        {
            WeaponSwap(5);
        }
    }
        void SwapToWeaponSix()
    {
        if (AbleToSwapWeapons())
        {
            WeaponSwap(6);
        }
    }
        void SwapToWeaponSeven()
    {
        if (AbleToSwapWeapons())
        {
            WeaponSwap(7);
        }
    }
}
