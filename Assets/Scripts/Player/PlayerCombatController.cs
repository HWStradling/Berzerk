using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombatController : MonoBehaviour
{
    // state variables
    private string selectedWeapon;
    private int directionFacing;
    private bool attackDelayed = false;

    //gun rotation variables
    [SerializeField] private Transform arm;
    [SerializeField] private Transform weaponHandle;
    private new Camera camera;
    [SerializeField] private float aimingArc;

    //bullet prefabs to instantiate and weapon container to activate to display selected weapon,
    [SerializeField] private GameObject[] bulletPrefabArray;
    [SerializeField] private GameObject WeaponContainer;

    //basic gun firing points
    [SerializeField] private Transform[] basicGunFiringPoints;

    //rifle firing points
    [SerializeField] private Transform[] rifleFiringPoints;

    [SerializeField] UnityEvent OnShoot;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        camera = GameObject.FindGameObjectsWithTag("m_cam")[0].GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && selectedWeapon != "empty")
        {
            AttackSequence();
        }
    }
    private void FixedUpdate()
    {
        UpdateGunRotation();
    }
    private void AttackSequence()
    {

        if (attackDelayed == false)
        {
            switch (selectedWeapon)
            {
                case "basic_gun":
                    StartCoroutine(GunAttack(basicGunFiringPoints, new BasicGun()));
                    break;
                case "rifle":
                    StartCoroutine(GunAttack(rifleFiringPoints, new Rifle()));
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator GunAttack(Transform[] firepoints, IWeapon weapon)
    {
        if (!gameObject.activeInHierarchy)
        {
            yield break;
        }
        Transform firepoint;
        GameObject bullet;
        BulletController bC;
        OnShoot?.Invoke();
        switch (directionFacing)
        {
            case 0:
                firepoint = firepoints[0];
                bullet = Instantiate(bulletPrefabArray[0], firepoint.position, firepoint.rotation);
                bC = bullet.GetComponent<BulletController>();
                bC.Direction = 0; bC.BulletSpeed = weapon.BulletSpeed; bC.Damage = weapon.WeaponDamage; bC.Owner = gameObject;
                break;
            case 1:
                firepoint = firepoints[1];
                bullet = Instantiate(bulletPrefabArray[1], firepoint.position, firepoint.rotation);
                bC = bullet.GetComponent<BulletController>();
                bC.Direction = 1; bC.BulletSpeed = weapon.BulletSpeed; bC.Damage = weapon.WeaponDamage; bC.Owner = gameObject;
                break;
            case 2:
            case 3:
                firepoint = firepoints[2];
                bullet = Instantiate(bulletPrefabArray[2], firepoint.position, firepoint.rotation);
                bC = bullet.GetComponent<BulletController>();
                bC.Direction = 3; bC.BulletSpeed = weapon.BulletSpeed; bC.Damage = weapon.WeaponDamage; bC.Owner = gameObject;
                break;
            default:
                break;
        }
        attackDelayed = true;
        yield return new WaitForSeconds(weapon.BulletDelay);
        attackDelayed = false;
    }
    public void UpdateDirectionFacing(int newDirectionFacing)
    {
        directionFacing = newDirectionFacing;
    }
    public void UpdateSelectedWeapon(string newSelectedItem)
    {
        if (newSelectedItem == "empty")
        {
            selectedWeapon = newSelectedItem;
            return;
        }
        if (newSelectedItem == "basic_gun" || newSelectedItem == "rifle")
        {
            selectedWeapon = newSelectedItem;
        }
    }

    private void UpdateGunRotation()
    {
        if (arm.gameObject.activeSelf || weaponHandle.gameObject.activeSelf)
        {
            Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 directionToMouse = mousePos - (Vector2)arm.position;
            float angleToMouse = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
            switch (directionFacing)// TODO: make directionfacingstate an enum.
            {
                case 0:// up,
                    arm.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    if (Mathf.Clamp(angleToMouse, -90 - aimingArc, -90 + aimingArc) != angleToMouse)
                        return;

                    weaponHandle.localRotation = Quaternion.Euler(0f, 0f, angleToMouse + 90); // use different method !!!
                    return;
                case 1: //up
                    arm.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    if (Mathf.Clamp(angleToMouse, 90 - aimingArc, 90 + aimingArc) != angleToMouse)
                        return;
                    weaponHandle.localRotation = Quaternion.Euler(0f, 0f, angleToMouse - 90);
                    return;
                case 2:// right flip,
                    if (Mathf.Clamp(angleToMouse, -aimingArc, aimingArc) != angleToMouse)
                        return;
                    weaponHandle.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    arm.localRotation = Quaternion.Euler(0f, 0f, -angleToMouse);
                    break;
                case 3: // left,

                    if (angleToMouse > 0)
                    {
                        if (angleToMouse != Mathf.Clamp(angleToMouse, 180 - aimingArc, 180))
                            return;
                        weaponHandle.localRotation = Quaternion.Euler(0f, 0f, 0f);
                        arm.localRotation = Quaternion.Euler(0f, 0f, angleToMouse - 180);
                    }
                    if (angleToMouse < 0)
                    {
                        if (angleToMouse != Mathf.Clamp(angleToMouse, -180, -180 + aimingArc))
                            return;
                        weaponHandle.localRotation = Quaternion.Euler(0f, 0f, 0f);
                        arm.localRotation = Quaternion.Euler(0f, 0f, angleToMouse + 180);
                    }
                    break;
                default:
                    break;
            }
        }

    }
}
