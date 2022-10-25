using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySixMovement : MonoBehaviour
{
    private float speed = .3f;
    Vector2 pointA;
    Vector2 pointB;
    // Start is called before the first frame update
    void Start()
    {
        pointA = new Vector2(49.33f, -0.37f);
        pointB = new Vector2(47.65f, -0.37f);
    }

    // Update is called once per frame
    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector2.Lerp(pointA, pointB, time);
    }
}
