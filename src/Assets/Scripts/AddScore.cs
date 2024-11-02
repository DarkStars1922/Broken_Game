using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    public int score;
    public CherryController target;

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);
        target.score += score;
        target.Check();
    }
}
