using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Spell
{
    public bool casted = false;
    public bool armed = false;
    public float armTime = 0f;
    public Explosion explosionPrefab;
    public SpriteRenderer light;
    public Color32[] colors = new Color32[2];
    public int damage = 20;

    protected void Start()
    {
        base.Start();
        this.spellType = SpellType.SELF;
    }

    protected void Update()
    {
        base.Update();
        if (this.casted) {
            if (Time.time > this.armTime) {
                this.armed = true;
            }
        }
        if (this.armed) {
            this.light.color = this.colors[1];
        } else {
            this.light.color = this.colors[0];
        }
    }

    protected override void castSpell()
    {
        if (!this.casted) {
            base.castSpell();
            this.casted = true;
            this.owner.spell = null;
            this.gameObject.transform.parent = null;
            this.targetPosition = this.owner.transform.position;
            this.transform.position = this.targetPosition;
            this.armTime = Time.time + 0.35f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.casted) {
            if (this.armed && col.gameObject.tag != "Player" && col.gameObject.tag != "PlayerExit") {
                Explosion explosion = Instantiate(this.explosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                explosion.damage = this.damage;
                explosion.init();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (this.casted) {
            if (this.armed && other.gameObject.tag != "Player" && other.gameObject.tag != "PlayerExit") {
                Explosion explosion = Instantiate(this.explosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                explosion.damage = this.damage;
                explosion.init();
                Destroy(this.gameObject);
            }
        }
    }
}
