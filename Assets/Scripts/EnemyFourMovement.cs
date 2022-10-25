using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFourMovement : MonoBehaviour
{
    private float speed = .3f;
    Vector2 pointA;
    Vector2 pointB;
    // Start is called before the first frame update
    void Start()
    {
        pointA = new Vector2(-2.79f, 8.64f);
        pointB = new Vector2(-4.32f, 8.64f);
    }

    // Update is called once per frame
    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector2.Lerp(pointA, pointB, time);
    }
}
