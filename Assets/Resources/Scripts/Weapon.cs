﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    // Weapon Propeties
    public string Name;
    public float FireRate;
    public int Damage;
    public float BulletSpeed;

    // instance refrence
    public GameObject Bullet;

    // temporary values
    private float NextFire;
    private float spriteWidth;

    private void Start()
    {
        // calculate bullet spawn position offset from sprite pivot
        Sprite gunSprite = GetComponent<SpriteRenderer>().sprite;
        spriteWidth = gunSprite.bounds.size.x * transform.localScale.x;
    }
    public void Attack()
    {
        NextFire += Time.fixedDeltaTime;

        if (NextFire > FireRate)
        {
            // calculate bullet position and rotation
            Vector3 shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Quaternion bulletRotation = transform.rotation * Quaternion.Euler(0.0f, 0.0f, 90.0f);
            Vector3 spawnPos = transform.position + transform.right * spriteWidth * 0.75f;

            // spawn bullet and add velocity
            GameObject bullet = Instantiate(Bullet, spawnPos, bulletRotation);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * BulletSpeed;
            Debug.Log(bullet.GetComponent<Rigidbody2D>().velocity);

            // store the bullet in a parent object 
            bullet.transform.SetParent(GameObject.Find("Bullets").transform);

            // reset timer
            NextFire = 0;
        }
    }

    public void RotateToAimCursor()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.right = new Vector3(direction.x, direction.y, 0);
    }
}