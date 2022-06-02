using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCaptureController : MonoBehaviour
{
    public float catchMoveSpeed;
    public float idleMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(Vector3.forward * catchMoveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.forward * idleMoveSpeed * Time.deltaTime);
        }
    }
}
