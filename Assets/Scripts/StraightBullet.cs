using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : MonoBehaviour
{
    private Rigidbody2D rigidBody2d;
    private Vector3 zero = Vector3.zero;
    public float speed = 10f;
    public int damage = 10;

    protected virtual void Start()
    {
        this.rigidBody2d = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update() {
        // nothing
    }

    protected virtual void FixedUpdate()
    {
        this.Move();
    }

    protected void Move()
    {
		this.rigidBody2d.velocity = Vector3.SmoothDamp(this.rigidBody2d.velocity, this.transform.up * this.speed * Time.fixedDeltaTime, ref this.zero, .05f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            player.damage(this.damage);
        }
        if (col.gameObject.tag != "Boss" && col.gameObject.tag != "BossHead" && col.gameObject.tag != "BossArm" && col.gameObject.tag != "Enemy" && col.gameObject.tag != "PlayerExit" && col.gameObject.tag != "Bullet") {
            Destroy(this.gameObject);
        }
    }
}
