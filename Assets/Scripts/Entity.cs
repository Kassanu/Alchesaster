using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private Rigidbody2D rigidBody2d;
    private Vector3 zero = Vector3.zero;
    protected Vector3 movement = Vector3.zero;

    public float runSpeed = 10f;
    public bool hasRunSpeedBuff = false;
    public float runSpeedPercentIncrase = 0f;
    public float runSpeedBuffTime = 0f;
    public float wakeUp = 0f;

    [SerializeField]
    private int health = 100;
    public int Health {
        get => health;
        set {
            if (value > 100) {
                this.health = 100;
            } else if (value < 0) {
                this.health = 0;
            } else {
                this.health = value;
            }
        }
    }

    protected virtual void Start()
    {
        this.rigidBody2d = GetComponent<Rigidbody2D>();
        this.wakeUp = Time.time + 0.5f;
    }

    protected virtual void Update()
    {
        if (this.hasRunSpeedBuff && Time.time > this.runSpeedBuffTime) {
            this.runSpeedPercentIncrase = 0;
            this.hasRunSpeedBuff = false;
        }
    }

    protected virtual void FixedUpdate()
    {
        this.Move();
    }

    protected virtual void Move()
    {
        this.movement = this.movement.normalized;
		this.rigidBody2d.velocity = Vector3.SmoothDamp(this.rigidBody2d.velocity, this.movement * this.getSpeed() * Time.fixedDeltaTime, ref this.zero, .05f);
    }

    protected virtual void Stop()
    {
        this.movement = this.movement.normalized;
		this.rigidBody2d.velocity = Vector2.zero;
    }

    public void heal(int amount)
    {
        this.Health += amount;
    }

    public void damage(int amount)
    {
        this.Health -= amount;
        if (this.Health <= 0) {
            this.kill();
        }
    }

    public float getSpeed()
    {
        if (!this.hasRunSpeedBuff) {
            return this.runSpeed;
        }

        return this.runSpeed + ((this.runSpeedPercentIncrase/100f) * this.runSpeed);
    }

    public void addSpeedBuff(float increasePercent, float duration)
    {
        this.runSpeedPercentIncrase = increasePercent;
        this.runSpeedBuffTime = Time.time + duration;
        this.hasRunSpeedBuff = true;
    }

    public void kill()
    {
        Destroy(this.gameObject);
    }
}
