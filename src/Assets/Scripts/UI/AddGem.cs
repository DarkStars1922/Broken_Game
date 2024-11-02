using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddGem : MonoBehaviour
{
    public int gem;
    public GemController target;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        target.gem += gem;
        target.Check();
    }
}
