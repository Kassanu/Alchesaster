using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    int bottlePressed = -1;
    public List<Bottle> bottles;
    public Cauldron cauldron;
    public Transform spellTransform;
    public Spell spell;
    float horizontalMove = 0f;
    float verticalMove = 0f;

    protected override void Update()
    {
        base.Update();
        this.horizontalMove = Input.GetAxisRaw("Horizontal");
        this.verticalMove = Input.GetAxisRaw("Vertical");
        this.movement = new Vector3(this.horizontalMove, this.verticalMove, 0);
        if (Input.GetKeyUp(KeyCode.Q)) {
            this.cauldron.useCauldron();
        }

        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            this.bottlePressed = 0;
        } else if (Input.GetKeyUp(KeyCode.Alpha2)) {
            this.bottlePressed = 1;
        } else if (Input.GetKeyUp(KeyCode.Alpha3)) {
            this.bottlePressed = 2;
        } else if (Input.GetKeyUp(KeyCode.Alpha4)) {
            this.bottlePressed = 3;
        }

        if (this.bottlePressed > -1) {
            this.bottles[this.bottlePressed].useBottle();
            this.bottlePressed = -1;
        }


    }

    public void addSpell(Spell spell) {
        this.spell = spell;
        this.spell.owner = this;
        spell.gameObject.transform.parent = this.gameObject.transform;
        spell.gameObject.transform.position = new Vector2(this.spellTransform.position.x, this.spellTransform.position.y);
    }

    public void enterRoom()
    {
        if (this.spell != null) {
            this.spell.cleanUp();
        }
        this.spell = null;
        this.resetBottles();
        this.startBottlesRefreshing();
    }

    public void resetBottles()
    {
        foreach (Bottle bottle in this.bottles) {
            bottle.resetBottle();
        }
    }

    public void stopBottlesRefreshing()
    {
        foreach (Bottle bottle in this.bottles) {
            bottle.stopBottleRefresh();
        }
    }

    public void startBottlesRefreshing()
    {
        foreach (Bottle bottle in this.bottles) {
            bottle.startBottleRefresh();
        }
    }
}
