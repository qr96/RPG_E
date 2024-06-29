using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTaker : MonoBehaviour
{
    public float weight = -6f;
    public float pivot = 27.70215f;

    private void LateUpdate()
    {
        var cameraDis = CalculateCameraDis((float)Screen.width / Screen.height);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + cameraDis, transform.position.z - cameraDis);
    }

    float CalculateCameraDis(float screenRatio)
    {
        return weight * screenRatio + pivot;
    }
}
