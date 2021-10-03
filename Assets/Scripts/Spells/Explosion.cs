using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 1f;
    public int damage = 0;

    public void init()
    {
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
        Destroy(this.gameObject, .5f);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, this.radius);
    }
}
