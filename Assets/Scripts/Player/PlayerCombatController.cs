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
    private bool allowAttackState =  true;

    // health variables
    public float playerCurrentHealth { get; private set; }
    public float playerMaxHealth { get; private set; } = 100;
    private bool allowGradualHeal = true;

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
        playerCurrentHealth = playerMaxHealth;
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
        if (selectedWeapon != WeaponType.None && allowAttackState == true)
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
        allowAttackState = false;
        yield return new WaitForSeconds(bulletDelay);
        allowAttackState = true;
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
    public void ApplyDamage(float damage)
    {
        if (playerCurrentHealth - damage <= 0)
        {
            allowGradualHeal = false;
            playerCurrentHealth = 0;
            Die();
        }
        else
        {
            playerCurrentHealth -= damage;
            // play damage animation
        }
    }
    private void GradualHeal()
    {
        // ienumerator tics heal increase on set duration.
    }
    public void SetPlayerMaxHealth(float newMaxHealth)
    {
        playerMaxHealth = newMaxHealth;
    }
    private void Die()
    {
        // play death animation,
        // load death scene;
        Destroy(gameObject);
    }
}
