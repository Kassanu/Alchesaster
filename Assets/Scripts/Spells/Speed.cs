using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : Spell
{
    public float increasePercent = 100f;
    public float duration = 2f;

    protected void Start()
    {
        base.Start();
        this.spellType = SpellType.SELF;
    }

    protected override void castSpell()
    {
        this.owner.addSpeedBuff(this.increasePercent, this.duration);
        Destroy(this.gameObject);
    }
}
