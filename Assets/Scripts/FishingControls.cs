using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingControls : MonoBehaviour
{
    public GameObject catchCanvas;
    public AnimationState holdState;
    public AnimationState reelState;
    public AnimationState castState;
    public Bobber bobber;
    public Vector3 force;
    public Text announcementText;

    private Vector3 bobberRestPos;
    private States state;
    private AnimationState currentAnimationState;
    private float castMultiplier;
    private float fishEscapeAngleTimer;
    private Vector3 fishEscapeDirection;

    [System.Serializable]
    public class AnimationState
    {
        public Vector3 position;
        public Vector3 rotation;
        public float moveSpeed;
        public float rotateSpeed;
    }

    public enum States
    {
        Hold,
        Reel,
        Cast,
    }

    // Start is called before the first frame update
    void Start()
    {
        state = States.Hold;
        currentAnimationState = holdState;
        bobberRestPos = bobber.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        announcementText.color = Color.Lerp(announcementText.color, Color.clear, 0.02f * Time.deltaTime);

        if (state == States.Hold)
        {
            if (Input.GetMouseButtonDown(0))
            {
                castMultiplier = 0.2f;
                state = States.Reel;
                currentAnimationState = reelState;
            }
        }
        else if (state == States.Reel)
        {
            castMultiplier = Mathf.Clamp(castMultiplier + Time.deltaTime, 0, 1);
            if (Input.GetMouseButtonUp(0))
            {
                Invoke("CastBobber", 0.2f);
                state = States.Cast;
                currentAnimationState = castState;
            }
        }
        else if (state == States.Cast)
        {
            if (Input.GetMouseButton(0))
            {
                bobber.ReelIn();
            }
        }

        Animate();
    }

    void Animate()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition,
            currentAnimationState.position, currentAnimationState.moveSpeed * Time.deltaTime);

        Vector3 euler = transform.localEulerAngles;
        transform.localRotation = Quaternion.Lerp(Quaternion.Euler(euler),
            Quaternion.Euler(currentAnimationState.rotation), currentAnimationState.rotateSpeed * Time.deltaTime);
    }

    void CastBobber()
    {
        Rigidbody bobberRb = bobber.GetComponent<Rigidbody>();
        bobberRb.isKinematic = false;
        bobberRb.transform.parent = null;
        Vector3 forwardForce = Camera.main.transform.forward * force.z;
        Vector3 upForce = Vector3.up * force.y;
        bobberRb.AddForce((forwardForce + upForce * castMultiplier), ForceMode.Impulse);
    }

    public void ReturnBobber()
    {
        Rigidbody bobberRb = bobber.GetComponent<Rigidbody>();
        bobberRb.isKinematic = true;
        bobber.transform.parent = transform;
        bobber.transform.localPosition = bobberRestPos;
        state = States.Hold;
        currentAnimationState = holdState;

        catchCanvas.SetActive(false);
        Invoke("CaughtFish", 0.6f);
    }

    //Fish "Escaping", move bobber around randomly
    public void EscapeBobber()
    {
        fishEscapeAngleTimer -= Time.deltaTime;
        if (fishEscapeAngleTimer <= 0)
        {
            fishEscapeDirection = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)).normalized;
            fishEscapeAngleTimer = 0.6f;
        }

        bobber.transform.position += fishEscapeDirection * 3 * Time.deltaTime;
        
    }

    public void CaughtFish()
    {
        announcementText.text = "Caught: Herring (" + string.Format("{0:F1}", Random.Range(12f, 22f)) + " inches)";
        announcementText.color = Color.white;
    }
}
