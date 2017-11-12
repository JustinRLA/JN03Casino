using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillController : MonoBehaviour {

    public Transform startHeight;
    public Transform endHeight;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startHeight.position, endHeight.position);
    }
    void FixedUpdate()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startHeight.position, endHeight.position, fracJourney);
    }
}
