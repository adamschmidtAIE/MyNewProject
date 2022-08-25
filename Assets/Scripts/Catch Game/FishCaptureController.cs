using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishCaptureController : MonoBehaviour
{
    public float catchDistance;
    public float catchMoveSpeed;
    public float idleMoveSpeed;
    public FishingControls fishingControls;

    public Transform fish;
    public Transform capture;
    public Image indicator;

    private Color goodColor;
    public Color badColor;


    // Start is called before the first frame update
    void Start()
    {
        goodColor = indicator.color;
        
    }

    private void OnEnable()
    {
        fish.localEulerAngles = Vector3.zero;
        capture.localEulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float fishAngle = SetAngle(fish.localEulerAngles.z);
        float ringAngle = SetAngle(capture.localEulerAngles.z);

        float distance = Mathf.Abs(fishAngle - ringAngle);

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Distance: " + distance);
        }
        if (distance < catchDistance)
        {
            indicator.color = goodColor;
        }
        else
        {
            indicator.color = badColor;
            fishingControls.EscapeBobber();
        }

        if (Input.GetMouseButton(0))
        {
            transform.Rotate(Vector3.forward * catchMoveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.forward * idleMoveSpeed * Time.deltaTime);
        }
    }

    float SetAngle(float angle)
    {
        if (angle > 180)
        {
            angle -= 180;
        }
        else if (angle <= 180)
        {
            angle += 180;
        }

        return angle;
    }
}
