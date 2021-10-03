using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cauldron : MonoBehaviour
{
    public GameObject fill;
    public List<BottleColor> bottles;
    public List<Spell> spells;
    public Player player;
    public Explosion explosionPrefab;

    public void addColor(BottleColor color)
    {
        if (this.bottles.Count == 2) {
            Debug.Log("Cauldron overflow explode");
            Explosion explosion = Instantiate(this.explosionPrefab, this.player.gameObject.transform.position, Quaternion.identity);
            explosion.damage = 10;
            explosion.init();
            this.resetCauldron();
        } else {
            Debug.Log("Adding: " + color);
            this.bottles.Add(color);
            this.fill.SetActive(true);
            this.fill.GetComponent<Image>().color = this.cauldronColor();
        }
    }

    public void useCauldron()
    {
        int toInstantiate = 0;
        if (this.bottles.Count == 2) {
            if ((this.bottles[0] == BottleColor.Green || this.bottles[1] == BottleColor.Green)) {
                if ((this.bottles[0] == BottleColor.Blue || this.bottles[1] == BottleColor.Blue)) {
                    toInstantiate = 0;
                } else if (this.bottles[0] == BottleColor.Yellow || this.bottles[1] == BottleColor.Yellow) {
                    toInstantiate = 4;
                } else if (this.bottles[0] == BottleColor.Red || this.bottles[1] == BottleColor.Red) {
                    toInstantiate = 3;
                }
            } else if ((this.bottles[0] == BottleColor.Red || this.bottles[1] == BottleColor.Red)) {
                if ((this.bottles[0] == BottleColor.Blue || this.bottles[1] == BottleColor.Blue)) {
                    toInstantiate = 5;
                } else if (this.bottles[0] == BottleColor.Yellow || this.bottles[1] == BottleColor.Yellow) {
                    toInstantiate = 2;
                }
            } else if ((this.bottles[0] == BottleColor.Blue || this.bottles[1] == BottleColor.Blue)) {
                 if (this.bottles[0] == BottleColor.Yellow || this.bottles[1] == BottleColor.Yellow) {
                    toInstantiate = 1;
                }
            }
            this.player.addSpell(Instantiate(this.spells[toInstantiate], new Vector3 (0,0,0), Quaternion.identity));
            this.resetCauldron();
        }
    }

    private void resetCauldron()
    {
        this.bottles.Clear();
        this.fill.SetActive(false);
    }

    private Color32 cauldronColor()
    {
        Color32 color = new Color32(255, 255, 255, 255);

        if (this.bottles.Count == 1) {
            switch (this.bottles[0])
            {
                case BottleColor.Green:
                color = new Color32(0, 152, 0, 255);
                break;
                case BottleColor.Blue:
                color = new Color32(26, 53, 198, 255);
                break;
                case BottleColor.Yellow:
                color = new Color32(255, 240, 0, 255);
                break;
                case BottleColor.Red:
                color = new Color32(255, 0, 0, 255);
                break;
            }
        } else {
            if ((this.bottles[0] == BottleColor.Green || this.bottles[1] == BottleColor.Green)) {
                if ((this.bottles[0] == BottleColor.Blue || this.bottles[1] == BottleColor.Blue)) {
                    color = new Color32(26, 205, 198, 255);
                } else if (this.bottles[0] == BottleColor.Yellow || this.bottles[1] == BottleColor.Yellow) {
                    color = new Color32(0, 243, 0, 255);
                } else if (this.bottles[0] == BottleColor.Red || this.bottles[1] == BottleColor.Red) {
                    color = new Color32(255, 152, 0, 255);
                }
            } else if ((this.bottles[0] == BottleColor.Red || this.bottles[1] == BottleColor.Red)) {
                if ((this.bottles[0] == BottleColor.Blue || this.bottles[1] == BottleColor.Blue)) {
                    color = new Color32(255, 53, 198, 255);
                } else if (this.bottles[0] == BottleColor.Yellow || this.bottles[1] == BottleColor.Yellow) {
                    color = new Color32(255, 255, 198, 255);
                }
            } else if ((this.bottles[0] == BottleColor.Blue || this.bottles[1] == BottleColor.Blue)) {
                 if (this.bottles[0] == BottleColor.Yellow || this.bottles[1] == BottleColor.Yellow) {
                    color = new Color32(77, 108, 154, 255);
                }
            }
        }

        return color;
    }
}
