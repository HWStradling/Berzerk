using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    enum WeaponType
    {
        None,// default value for no weapon or non weapon item equiped,
        BasicGun,
        Rifle
    }

    private WeaponType selectedWeapon = WeaponType.None;
    private int directionFacing = 0;
    [SerializeField] GameObject[] bulletPrefabArray;
    [SerializeField] GameObject WeaponContainer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && WeaponContainer.activeSelf)
        {
            directionFacing = gameObject.GetComponent<CharacterController>().GetDirectionFacing();
           
            AttackSequence();
           
        }
    }

    private void AttackSequence()
    {
        GetSelectedWeapon();
        if (selectedWeapon != WeaponType.None)
        {
            switch (selectedWeapon)
            {
                case WeaponType.BasicGun:
                    BasicGunAttack();
                    break;
                case WeaponType.Rifle:
                    RifleAttack();
                    break;
                default:
                    break;
            }
        }
    }
    private void BasicGunAttack()
    {
        
        Transform firepoint;
        switch (directionFacing)
        {
            
            case 0:
                firepoint = GameObject.Find("bg_firepoint_down").transform;
                Instantiate(bulletPrefabArray[0], firepoint.position, firepoint.rotation);
                break;
            case 1:
                firepoint = GameObject.Find("bg_firepoint_up").transform;
                //GameObject bullet = bulletPrefabArray[0]; // flips prefab for shooting up
                //bullet.GetComponent<SpriteRenderer>().flipY = true;
                Instantiate(bulletPrefabArray[1], firepoint.position, firepoint.rotation);
                break;
            case 2:
            case 3:
                firepoint = GameObject.Find("bg_firepoint_side").transform;
                Instantiate(bulletPrefabArray[2], firepoint.position, firepoint.rotation);
                break;
            default:
                break;
        }


    }
    private void RifleAttack()
    {

    }
    private void GetSelectedWeapon()
    {
        
        GameObject selectedItem = gameObject.GetComponent<InventoryController>().GetSelectedItem();
        if (selectedItem.CompareTag("Weapon"))
        {
            Debug.Log("isWeapon true");
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
                    break;
            }
        }
        this.selectedWeapon = WeaponType.None;
        

    }
}
