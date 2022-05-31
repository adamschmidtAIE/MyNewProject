using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public float flowSpeed;
    public float tideSpeed;
    public float tideMax;
    float offset;
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset = Time.time * flowSpeed;
        mat.SetTextureOffset("_MainTex", new Vector2(0, offset));

        transform.position += Vector3.up * Mathf.Sin(Time.timeSinceLevelLoad * tideSpeed) * tideMax;
    }
}
