using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidGroundEffect : MonoBehaviour
{
    public float radius = 1f;
    public float duration = 1f;
    public float tick = 0.25f;
    public float nextTick = 0f;
    public int damage = 0;

    public void init()
    {
        this.nextTick = Time.time + this.tick;
        Destroy(this.gameObject, this.duration);
    }

    public void Update()
    {
        if (Time.time > this.nextTick) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, this.radius, 1 << LayerMask.NameToLayer("Entities"));

            foreach (Collider2D collider in colliders) {
                if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Enemy") {
                    Entity entity = collider.gameObject.GetComponent<Entity>();
                    entity.damage(this.damage);
                }
                if (collider.gameObject.tag == "BossHead") {
                    Entity entity = GameObject.FindWithTag("Boss").GetComponent<Entity>();
                    entity.damage(this.damage);
                }
            }
            this.nextTick = Time.time + this.tick;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, this.radius);
    }
}
