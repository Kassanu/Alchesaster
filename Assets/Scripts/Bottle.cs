using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public BottleColor color;
    public Cauldron cauldron;
    public GameObject fill;
    public float cooldown = 1f;
    private float cooldownTime = 0f;
    private bool canUse = true;
    private bool stopRefresh = false;
    private bool useRemaining = true;

    private void Update() {
        if (!this.canUse && Time.time > this.cooldownTime) {
            this.resetBottle();
        }
    }

    public void useBottle()
    {
        if (this.useRemaining && this.canUse && Time.time > this.cooldownTime) {
            this.cauldron.addColor(this.color);
            this.canUse = false;
            this.fill.SetActive(false);
            this.cooldownTime = Time.time + this.cooldown;
            if (this.stopRefresh == true) {
                this.useRemaining = false;
            }
        }
    }

    public void resetBottle()
    {
        this.canUse = true;
        this.cooldownTime = 0f;
        this.fill.SetActive(true);
    }

    public void stopBottleRefresh()
    {
        this.stopRefresh = true;
        this.useRemaining = true;
    }

    public void startBottleRefresh()
    {
        this.stopRefresh = false;
        this.useRemaining = true;
    }
}
