using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : Spell
{
    public int damage = 1;
    public bool casted = false;
    public AcidGroundEffect acidGroundEffectPrefab;
    private Rigidbody2D rigidBody2d;

    protected void Start()
    {
        base.Start();
        this.rigidBody2d = GetComponent<Rigidbody2D>();
        this.rigidBody2d.isKinematic = true;
        this.spellType = SpellType.LOCATION;
        this.crosshair.show();
    }

    protected override void castSpell()
    {
        if (!this.casted) {
            base.castSpell();
            this.casted = true;
            this.owner.spell = null;
            this.crosshair.hide();
            this.targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPositionNormalized = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position);
            targetPositionNormalized = targetPositionNormalized.normalized;
            this.rigidBody2d.isKinematic = false;
            this.rigidBody2d.velocity = new Vector2 (targetPositionNormalized.x, targetPositionNormalized.y).normalized * 10f;
            Destroy(this.gameObject, 10f);
        }
    }

    void FixedUpdate() {
        if (this.casted) {
            if(Vector2.Distance(this.rigidBody2d.position, this.targetPosition) <= 0.1) {
                AcidGroundEffect acidGroundEffectPrefab = Instantiate(this.acidGroundEffectPrefab, this.gameObject.transform.position, Quaternion.identity);
                acidGroundEffectPrefab.transform.parent = this.spellContainer.transform;
                acidGroundEffectPrefab.damage = 1;
                acidGroundEffectPrefab.init();
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.casted) {
            if (col.gameObject.tag != "Player" && col.gameObject.tag != "PlayerExit") {
                Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
                AcidGroundEffect acidGroundEffectPrefab = Instantiate(this.acidGroundEffectPrefab, this.gameObject.transform.position, Quaternion.identity);
                acidGroundEffectPrefab.transform.parent = this.spellContainer.transform;
                acidGroundEffectPrefab.damage = 1;
                acidGroundEffectPrefab.init();
                Destroy(this.gameObject);
            }
        }
    }
}
