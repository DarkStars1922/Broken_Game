using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public int score;
    public GameObject hiddenMap;

    private void Awake() {
        score = 0;
    }

    public void Check() {
        if(score==3) hiddenMap.SetActive(true);
    }
}
