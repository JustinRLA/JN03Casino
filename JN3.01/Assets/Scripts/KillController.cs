using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillController : MonoBehaviour {

    public Transform startingPosition;
    public Transform targetPosition;
    public float speed = 0.5f;

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, step);
    }

    public void BackToDefault()
    {
        transform.position = startingPosition.position;
    }
}
