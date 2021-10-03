using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Enemy
{
    public bool homing = false;
    public SpriteRenderer clothing;
    public Color32 clothingColor;
    public Bullet bulletPrefab;
    public float bulletSpeed = 10f;
    public int bulletDamage = 10;

    protected override void Start() {
        base.Start();
        this.clothing.color = this.clothingColor;
    }

    protected override void Attack()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.gameObject.transform.position, Quaternion.identity);
        bullet.speed = this.bulletSpeed;
        bullet.damage = this.bulletDamage;
        this.nextAttack = Time.time + this.attackCooldown;
    }
}
