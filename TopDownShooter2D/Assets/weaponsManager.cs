using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponsManager : MonoBehaviour
{
    enum WeaponType { handGun, shotGun, rifle };
    private WeaponType weaponType = WeaponType.handGun;
    [SerializeField] private GameObject handGun, shotGun, rifle;
    private int[] noOfMegazines;
    private int[] bulletsOfSingleMegazine;
    private int curBulletLeft, curMegazineIndex;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(6, 6);
        curMegazineIndex = 0;
        shotGun.SetActive(false);
        rifle.SetActive(false);
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
        else if (Input.GetButtonDown("shotGunSwitch"))
        {
            shotGun.SetActive(true);
            shotGun.GetComponent<gunBehaviour>()?.canShoot(false);
            handGun.SetActive(false);
            rifle.SetActive(false);
        }
        else if (Input.GetButtonDown("rifleSwitch"))
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
