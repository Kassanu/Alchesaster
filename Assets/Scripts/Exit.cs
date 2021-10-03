using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public bool bossExit = false;
    public bool isActive = false;
    public bool playerIsOn = false;
    public SpriteRenderer light;
    public Color32[] colors = new Color32[2];

    void Update()
    {
        if (this.isActive) {
            this.light.color = this.colors[1];
        } else {
            this.light.color = this.colors[0];
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.isActive) {
           if (col.gameObject.tag == "Player") {
               this.playerIsOn = true;
           }
        }
    }
}
