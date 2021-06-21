using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponsManager : MonoBehaviour
{


    [SerializeField] private GameObject handGun, shotGun, rifle, weaponPics;
    
    private int[] noOfMegazines;
    private int[] bulletsOfSingleMegazine;
    private int curBulletLeft, curMegazineIndex;
    private static bool[] canUseWeapon;

   
    // Start is called before the first frame update
    void Start()
    {
        canUseWeapon = new bool[] { true, false, false};
        Physics.IgnoreLayerCollision(6, 6,true);
        curMegazineIndex = 0;
        shotGun.SetActive(false);
        rifle.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        manageWeaponSwitch();

    }

    private void manageWeaponSwitch()
    {
        if (Input.GetButtonDown("handGunSwitch"))
        {
            handGun.SetActive(true);
            weaponPics.transform.GetChild(0).gameObject.SetActive(true);
            handGun.GetComponent<gunBehaviour>()?.canShoot(false);
            handGun.GetComponent<gunBehaviour>()?.switchIn();
            shotGun.SetActive(false);
            rifle.SetActive(false);
            weaponPics.transform.GetChild(1).gameObject.SetActive(false);
            weaponPics.transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (Input.GetButtonDown("shotGunSwitch") && canUseWeapon[1])
        {
            weaponPics.transform.GetChild(1).gameObject.SetActive(true);
            shotGun.SetActive(true);
            shotGun.GetComponent<gunBehaviour>()?.canShoot(false);
            shotGun.GetComponent<gunBehaviour>()?.switchIn();
            handGun.SetActive(false);
            rifle.SetActive(false);
            weaponPics.transform.GetChild(2).gameObject.SetActive(false);
            weaponPics.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (Input.GetButtonDown("rifleSwitch") && canUseWeapon[2])
        {
            weaponPics.transform.GetChild(2).gameObject.SetActive(true);
            rifle.SetActive(true);
            rifle.GetComponent<gunBehaviour>()?.canShoot(false);
            rifle.GetComponent<gunBehaviour>()?.switchIn();
            shotGun.SetActive(false);
            handGun.SetActive(false);
            weaponPics.transform.GetChild(0).gameObject.SetActive(false);
            weaponPics.transform.GetChild(1).gameObject.SetActive(false);

        }
    }
    public void setWeaponAvailability(int type, bool val)
    {
        if (type < 0 || type > 2) return;
        canUseWeapon[type] = val;
    }
    public void addAmmo()
    {
        if(canUseWeapon[0]) handGun.GetComponent<gunBehaviour>()?.addAmmo();
        if(canUseWeapon[1]) shotGun.GetComponent<gunBehaviour>()?.addAmmo();
        if(canUseWeapon[2]) rifle.GetComponent<gunBehaviour>()?.addAmmo();
    }
}
