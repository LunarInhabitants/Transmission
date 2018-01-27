using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public RawImage spinnyBoi;
    public RawImage redLightBoi;
    public RawImage greenLightBoi;
    public RawImage blueLightBoi;
    float spinnyAngle = 0.0f;

    void Update()
    {
        
        float targetSpinnyAngle = 0.0f;

        switch (Gun.Instance.GunMode)
        {
            default:
            case GunMode.Red:
                redLightBoi.color = Color.Lerp(redLightBoi.color, Color.white, 0.2f);
                greenLightBoi.color = Color.Lerp(greenLightBoi.color, Color.gray, 0.2f);
                blueLightBoi.color = Color.Lerp(blueLightBoi.color, Color.gray, 0.2f);
                targetSpinnyAngle = 0.0f;
                break;
            case GunMode.Green:
                redLightBoi.color = Color.Lerp(redLightBoi.color, Color.gray, 0.2f);
                greenLightBoi.color = Color.Lerp(greenLightBoi.color, Color.white, 0.2f);
                blueLightBoi.color = Color.Lerp(blueLightBoi.color, Color.gray, 0.2f);
                targetSpinnyAngle = 240.0f;
                break;
            case GunMode.Blue:
                redLightBoi.color = Color.Lerp(redLightBoi.color, Color.gray, 0.2f);
                greenLightBoi.color = Color.Lerp(greenLightBoi.color, Color.gray, 0.2f);
                blueLightBoi.color = Color.Lerp(blueLightBoi.color, Color.white, 0.2f);
                targetSpinnyAngle = 120.0f;
                break;
        }

        spinnyAngle = Mathf.LerpAngle(spinnyAngle, targetSpinnyAngle + Random.Range(-10.0f, 10.0f), 0.5f);
        spinnyBoi.transform.rotation = Quaternion.AngleAxis(spinnyAngle, Vector3.forward);
    }
}
