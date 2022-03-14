using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    enum WeaponType
    {
        None,// default value for no weapon or non weapon item equiped,
        BasicGun,
        Rifle
    }

    // state variables
    private WeaponType selectedWeapon = WeaponType.None;
    private int directionFacingState;
    private bool attackDelayed =  false;

    //bullet prefabs to instantiate and weapon container to activate to display selected weapon,
    [SerializeField] private GameObject[] bulletPrefabArray;
    [SerializeField] private GameObject WeaponContainer;

    //basic gun constants
    [SerializeField] private float bulletDelay;
    [SerializeField] private float basicGunBulletSpeed;
    [SerializeField] private float basicGunDamage;
    [SerializeField] private Transform[] basicGunFiringPoints;


    // Start is called before the first frame update
    void Start()
    {
        directionFacingState = gameObject.GetComponent<PlayerController>().DirectionFacingState;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && WeaponContainer.activeSelf)
        {
            directionFacingState = gameObject.GetComponent<PlayerController>().DirectionFacingState;
           
            AttackSequence();
           
        }
    }
    private void AttackSequence()
    {
        SetSelectedWeapon();
        if (selectedWeapon != WeaponType.None && attackDelayed == false)
        {
            switch (selectedWeapon)
            {
                case WeaponType.BasicGun:
                    StartCoroutine(BasicGunAttack());
                    break;
                case WeaponType.Rifle:
                    RifleAttack();
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator BasicGunAttack()
    {
        Transform firepoint;
        GameObject bullet;
        BulletController bC;
        switch (directionFacingState)
        {
            case 0:
                firepoint = basicGunFiringPoints[0];
                bullet = Instantiate(bulletPrefabArray[0], firepoint.position, firepoint.rotation);
                bC = bullet.GetComponent<BulletController>();
                bC.Direction = 0; bC.BulletSpeed = basicGunBulletSpeed; bC.Damage = basicGunDamage; bC.Owner = gameObject;
                break;
            case 1:
                firepoint = basicGunFiringPoints[1];
                bullet = Instantiate(bulletPrefabArray[1], firepoint.position, firepoint.rotation);
                bC = bullet.GetComponent<BulletController>();
                bC.Direction = 1; bC.BulletSpeed = basicGunBulletSpeed; bC.Damage = basicGunDamage; bC.Owner = gameObject;
                break;
            case 2:
            case 3:
                firepoint = basicGunFiringPoints[2];
                bullet = Instantiate(bulletPrefabArray[2], firepoint.position, firepoint.rotation);
                bC = bullet.GetComponent<BulletController>();
                bC.Direction = 3; bC.BulletSpeed = basicGunBulletSpeed; bC.Damage = basicGunDamage; bC.Owner = gameObject;
                break;
            default:
                break;
        }
        attackDelayed = true;
        yield return new WaitForSeconds(bulletDelay);
        attackDelayed = false;
    }
    private void RifleAttack()
    {
        // to implement !!!
    }
    private void SetSelectedWeapon()
    {
        
        GameObject selectedItem = gameObject.GetComponent<PlayerInventoryController>().GetSelectedItem();
        if (selectedItem.CompareTag("Weapon"))
        {
            switch (selectedItem.name)
            {
                case "basic_gun":
                    this.selectedWeapon = WeaponType.BasicGun;
                    return;
                case "rifle":
                    this.selectedWeapon = WeaponType.Rifle;
                    return;
                default:
                    Debug.Log("weapon name unrecognised");
                    return;
            }
        }
        Debug.Log("non weapon selected");
        this.selectedWeapon = WeaponType.None;
    }
}
