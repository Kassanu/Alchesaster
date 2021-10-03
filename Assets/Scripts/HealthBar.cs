using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBar;
    public Player player;

    void Update() {
        healthBar.transform.localScale = new Vector3(this.getLocalScale(), healthBar.transform.localScale.y, 0);
    }

    private float getHealthPercent() {
        return this.player.Health / 100f * 100f;
    }

    private float getLocalScale() {
        return 3 * (this.getHealthPercent ()) / 100f;
    }
}