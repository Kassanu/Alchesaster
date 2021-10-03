using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Entity target;
    private Rigidbody2D rigidBody2d;
    private Vector3 zero = Vector3.zero;
    public Vector3 movement = Vector3.zero;
    public float speed = 10f;
    public int damage = 10;

    protected virtual void Start()
    {
        this.rigidBody2d = GetComponent<Rigidbody2D>();
        this.target = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.movement = this.target.transform.position - this.transform.position;
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
        this.movement = this.movement.normalized;
		this.rigidBody2d.velocity = Vector3.SmoothDamp(this.rigidBody2d.velocity, this.movement * this.speed * Time.fixedDeltaTime, ref this.zero, .05f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            target.damage(this.damage);
        }
        if (col.gameObject.tag != "Enemy" && col.gameObject.tag != "PlayerExit" && col.gameObject.tag != "Bullet") {
            Destroy(this.gameObject);
        }
    }
}
