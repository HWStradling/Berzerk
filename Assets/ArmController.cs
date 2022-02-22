using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    SpriteRenderer sR;
    // Start is called before the first frame update
    void Start()
    {
        sR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate()
    {
        sR.enabled = true;
    }
    public void DeActivate()
    {
        sR.enabled = false;
    }
    public void Flip(bool toFlip)
    {
        sR.flipX = true;
    }
}
