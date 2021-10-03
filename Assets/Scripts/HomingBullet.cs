using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    public float lockOnDuration = 0f;
    public float lockOnTime = 0f;
    public bool lockedOn = true;

    protected override void Start() {
        base.Start();
        this.lockOnTime = Time.time + this.lockOnDuration;
    }

    protected void Update()
    {
        base.Update();
        if (Time.time > this.lockOnTime) {
            this.lockedOn = false;
        }
        if (this.lockedOn) {
            this.movement = this.target.transform.position - this.transform.position;
        }
    }
}
