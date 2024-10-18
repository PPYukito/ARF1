using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartPanelController : MonoBehaviour
{
    public Camera mainCam;

    // Update is called once per frame
    private void Update()
    {
        if (enabled)
            transform.LookAt(mainCam.transform);
    }
}
