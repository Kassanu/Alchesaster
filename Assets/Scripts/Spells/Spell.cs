using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    protected SpellType spellType;
    public Player owner;
    public Entity targetEntity;
    public Vector3 targetPosition;
    protected Crosshair crosshair;
    protected GameObject spellContainer;

    protected void Start() {
        this.crosshair = GameObject.FindWithTag("crosshair").GetComponent<Crosshair>();
        this.spellContainer = GameObject.FindWithTag("Spells");
    }

    protected void Update()
    {
        if (Input.GetButtonUp("Fire1")) {
            this.castSpell();
        }
    }

    protected virtual void castSpell()
    {
        Debug.Log("cast spell base");
        this.gameObject.transform.parent = this.spellContainer.transform;
    }

    public void cleanUp()
    {
        this.crosshair.hide();
        DestroyImmediate(this.gameObject);
    }
}
