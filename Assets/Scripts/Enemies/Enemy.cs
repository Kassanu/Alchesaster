using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    Player player;
    public float attackRadius = 1f;
    public int damage = 0;
    public float attackCooldown = 0f;
    public float nextAttack = 0f;

    protected override void Start()
    {
        base.Start();
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    protected override void Update()
    {
        base.Update();
        this.movement = this.player.transform.position - this.transform.position;
        if (this.playerInRange()) {
            if (Time.time > this.nextAttack) {
                this.Attack();
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if (!this.playerInRange()) {
            this.Move();
        } else {
            this.Stop();
        }
    }

    public bool playerInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, this.attackRadius, 1 << LayerMask.NameToLayer("Entities"));
        foreach (Collider2D collider in colliders) {
            if (collider.gameObject.tag == "Player") {
                return true;
            }
        }

        return false;
    }

    protected virtual void Attack()
    {
        this.player.damage(this.damage);
        this.nextAttack = Time.time + this.attackCooldown;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, this.attackRadius);
    }
}
