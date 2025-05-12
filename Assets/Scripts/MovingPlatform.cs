using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform point1, point2, startingPoint;
    public float platformSpeed = 1f;

    private Vector2 nextPosition;

    void Start()
    {
        transform.position = startingPoint.position;
        nextPosition = (startingPoint == point1) ? point2.position : point1.position;
    }

    void Update()
    {
        float timeSteps = platformSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, nextPosition, timeSteps);

        if (Vector2.Distance(transform.position, nextPosition) < 0.05f)
        {
            // Cambiar destino al otro punto
            nextPosition = (nextPosition == (Vector2)point1.position) ? point2.position : point1.position;
        }
    }
}