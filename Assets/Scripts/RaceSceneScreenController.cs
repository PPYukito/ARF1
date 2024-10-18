using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceSceneScreenController : MonoBehaviour
{
    [Header("Screen Object")]
    public GameObject CarPovScreen;

    [Header("SidePanelControl")]
    public SidePanelController sidePanelController;

    [Header("Car POV")]
    public RawImage car1POVImage;
    public RawImage car2POVImage;

    private void Start()
    {
        car1POVImage.enabled = true;
        car2POVImage.enabled = false;

        sidePanelController.ToggleButton(false);
    }

    public void SetActiveCarPovScreen(bool setActive)
    {
        CarPovScreen.SetActive(setActive);
        sidePanelController.ToggleButton(setActive);
    }

    public void ChangePOV()
    {
        car1POVImage.enabled = !car1POVImage.IsActive();
        car2POVImage.enabled = !car2POVImage.IsActive();
    }
}
