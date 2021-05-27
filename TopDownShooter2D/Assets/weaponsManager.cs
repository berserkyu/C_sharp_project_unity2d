using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponsManager : MonoBehaviour
{
    

    [SerializeField] private GameObject handGun, shotGun, rifle;
    private int[] noOfMegazines;
    private int[] bulletsOfSingleMegazine;
    private int curBulletLeft, curMegazineIndex;
    private static bool[] canUseWeapon;

    public static void setWeaponAvailability(int type,bool val)
    {
        if (type < -1 || type > 3) return;
        canUseWeapon[type] = val;
    }
    // Start is called before the first frame update
    void Start()
    {
        canUseWeapon = new bool[] { true, false, true};
        Physics.IgnoreLayerCollision(6, 6,true);
        curMegazineIndex = 0;
        shotGun.SetActive(false);
        rifle.SetActive(false);
    }
    private void onWeapoonsGet(Item.ItemType type)
    {
        canUseWeapon[(type == Item.ItemType.Shotgun?0:1)] = true;
    }
    private void manageWeaponSwitch()
    {
        if (Input.GetButtonDown("handGunSwitch"))
        {
            handGun.SetActive(true);
            handGun.GetComponent<gunBehaviour>()?.canShoot(false);
            shotGun.SetActive(false);
            rifle.SetActive(false);
        }
        else if (Input.GetButtonDown("shotGunSwitch") && canUseWeapon[1])
        {
            shotGun.SetActive(true);
            shotGun.GetComponent<gunBehaviour>()?.canShoot(false);
            handGun.SetActive(false);
            rifle.SetActive(false);
        }
        else if (Input.GetButtonDown("rifleSwitch") && canUseWeapon[2])
        {
            rifle.SetActive(true);
            rifle.GetComponent<gunBehaviour>()?.canShoot(false);
            shotGun.SetActive(false);
            handGun.SetActive(false);

        }
    }
    // Update is called once per frame
    void Update()
    {
        manageWeaponSwitch();
    }
}
