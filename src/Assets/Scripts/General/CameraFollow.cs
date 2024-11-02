using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 BLPosition;

    private void Update() {
        float x = target.position.x>BLPosition.x ? target.position.x : BLPosition.x;
        float y = target.position.y>BLPosition.y ? target.position.y : BLPosition.y;
        transform.position = new Vector3(x, y+2, BLPosition.z);
    }
}
