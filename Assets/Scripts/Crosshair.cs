using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public bool enabled = false;

    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position =  new Vector3(pos.x, pos.y, 0);
    }

    public void show()
    {
        this.enabled = true;
        this.gameObject.GetComponent<Renderer>().enabled = true;
    }

    public void hide()
    {
        this.enabled = false;
        this.gameObject.GetComponent<Renderer>().enabled = false;
    }
}
