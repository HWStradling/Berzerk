using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarController : MonoBehaviour
{
    [SerializeField] private PlayerCombatController playerCombat;
    [SerializeField] private EnemyCombatController enemyCombat;
    [SerializeField] private Image healthBar;
    // Update is called once per frame
    void Update()
    {
        if (playerCombat != null)
        {
            healthBar.fillAmount = playerCombat.playerCurrentHealth / playerCombat.playerMaxHealth;
        }else if (enemyCombat != null)
        {
            healthBar.fillAmount = enemyCombat.enemyCurrentHealth / enemyCombat.enemyMaxHealth;
        }
        
    }



}
