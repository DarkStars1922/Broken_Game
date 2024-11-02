using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class GemController : MonoBehaviour
{
    public Text gemNum;
    public int gem;
    public GameObject newWorld;

    public void Awake()
    {
        gem = 0;
    }
    private void Update()
    {
        gemNum.text = gem.ToString();
    }

    public void Check()
    {
        if (gem >= 3) newWorld?.SetActive(true);
    }

}
