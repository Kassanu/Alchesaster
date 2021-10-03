using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEffect : MonoBehaviour
{
    public float duration = 10f;

    public void init()
    {
        Destroy(this.gameObject, this.duration);
    }
}
