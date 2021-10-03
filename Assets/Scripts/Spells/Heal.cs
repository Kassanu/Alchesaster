using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Spell
{
   public int healAmount = 20;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        this.spellType = SpellType.SELF;
    }

    protected override void castSpell()
    {
        this.owner.heal(this.healAmount);
        Destroy(this.gameObject);
    }
}
