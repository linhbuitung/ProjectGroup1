using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shoot : MonoBehaviour
{
    public Transform Player;
    public Transform Gun;
    public Transform ShootPoint;

    public GameObject Bullet;

    public float bulletSpeed;
    public float fireRate; 

    public int amountBullet;
    public int magazineFull;
    private int magazine;
    public int reloadBreak;

    float readyForNextShot;
    private bool shootBreak = false;

    Vector3 direction = new Vector3(0,0,0);
    void Start()
    {
        magazine = magazineFull;       
    }

    // Update is called once per frame
    void Update()
    {
        
        BulletWay();
        Shooting();
        Reload();
    }



    void BulletWay(){
        direction.x = Player.localScale.x;
    }
    void Shooting()
    {
        if(Time.time > readyForNextShot && shootBreak == false)
        {
            readyForNextShot = Time.time + 1 / fireRate;
            if (Input.GetMouseButton(0) && magazine != 0)
            {
                GameObject BulletIns = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
                magazine--;
                BulletIns.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
                Destroy(BulletIns, 2);
            } 
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && amountBullet + magazine >= magazineFull && magazine != magazineFull)
        {
            amountBullet = amountBullet + magazine - magazineFull;
            magazine = magazineFull;
            StartCoroutine(ReloadTime());
            StopCoroutine(ReloadTime());

        }
        else if (Input.GetKeyDown(KeyCode.R) && amountBullet + magazine < magazineFull && amountBullet > 0)
        {
            magazine = amountBullet + magazine;
            amountBullet = 0;
            StartCoroutine(ReloadTime());
            StopCoroutine(ReloadTime());
        }
    }

    IEnumerator ReloadTime()
    {
        shootBreak = true;
        yield return new WaitForSeconds(reloadBreak);
        shootBreak = false;
    }

}
