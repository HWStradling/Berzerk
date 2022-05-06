using UnityEngine;
public class HealthBarController : MonoBehaviour
{

    public void UpdateHealthBar(float fillAmount)
    {
        Debug.Log("update health");
        Debug.Log(fillAmount);
        transform.localScale = new Vector3(fillAmount, 1, 1);
    }



}
