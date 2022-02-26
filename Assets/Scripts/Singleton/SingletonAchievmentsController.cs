using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAchievmentsController : MonoBehaviour
{
    public static SingletonAchievmentsController instance { get; private set;}
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
