using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowContent : MonoBehaviour
{
    public string titleText;
    [TextArea(1,4)]
    public string mainText;
    public TextMeshProUGUI titleTarget;
    public TextMeshProUGUI mainTarget;
    public ButtonController buttonController;
    public void ShowIt() {
        titleTarget.text = titleText;
        mainTarget.text = mainText;
        buttonController.ToUI();
    }
}
