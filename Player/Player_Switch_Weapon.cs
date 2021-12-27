using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Switch_Weapon : MonoBehaviour
{
    GameObject child;
    public GameObject[] weaponList = new GameObject[2];
    void Start()
    {
        child = GameObject.Find("Square");
        ClearWeapon();
    }

    
    void Update()
    {
        //SwitchWeapon();
    }

    /*void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ClearWeapon();
            weaponList[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ClearWeapon();
            weaponList[1].SetActive(true);
        }
    }*/

    public void SwitchOnCollide(int i)
    {
        ClearWeapon();
        weaponList[i].SetActive(true);
    }
    public void ClearWeapon()
    {
        for (int i = 0; i < weaponList.Length; i++)
        {
            weaponList[i].SetActive(false);
        }
    }
}
