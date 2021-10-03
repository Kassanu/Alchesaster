using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Spell
{
    public int damage = 1;
    public bool casted = false;
    public Explosion explosionPrefab;
    private Rigidbody2D rigidBody2d;

    protected void Start()
    {
        base.Start();
        this.rigidBody2d = GetComponent<Rigidbody2D>();
        this.rigidBody2d.isKinematic = true;
        this.spellType = SpellType.PROJECTILE;
        this.crosshair.show();
    }

    protected override void castSpell()
    {
        if (!this.casted) {
            base.castSpell();
            this.casted = true;
            this.owner.spell = null;
            this.crosshair.hide();
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position);
            direction = direction.normalized;
            this.rigidBody2d.isKinematic = false;
            this.rigidBody2d.velocity = new Vector2 (direction.x, direction.y).normalized * 10f;
            Destroy(this.gameObject, 10f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.casted) {
            if (col.gameObject.tag != "Player" && col.gameObject.tag != "PlayerExit") {
                Explosion explosion = Instantiate(this.explosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                explosion.transform.parent = this.spellContainer.transform;
                explosion.damage = 10;
                explosion.init();
                Destroy(this.gameObject);
            }
        }
    }
}
