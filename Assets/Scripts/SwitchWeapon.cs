using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    public bool isUsingBlueGun = true;
    public Animator anim;

    public 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isUsingBlueGun = !isUsingBlueGun;
            if (isUsingBlueGun)
            {
                changeGunToBlue();
            }
            else
            {
                changeGunToRed();
            }
        }
    }

    void changeGunToBlue()
    {
        GameManager.instance.currentGun = "blue";
        anim.Play("switchGunToBlue");
    }

    void changeGunToRed()
    {
        GameManager.instance.currentGun = "red";
        anim.Play("switchGunToRed");
    }
}
