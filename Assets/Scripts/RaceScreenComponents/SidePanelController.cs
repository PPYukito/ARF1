using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SidePanelController : MonoBehaviour
{
    [Header("POV")]
    public Button povBtn;
    public Sprite povOnSprite;
    public Sprite povOffSprite;

    public void ToggleButton(bool povOn)
    {
        if (povOn)
        {
            povBtn.image.sprite = povOnSprite;
        }
        else
        {
            povBtn.image.sprite = povOffSprite;
        }
    }
}
