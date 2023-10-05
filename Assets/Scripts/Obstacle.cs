using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 5;

    void Update()
    {
        if (Spawner.stop) Destroy(this);

        transform.position += Vector3.left * speed * Time.deltaTime;

        if(transform.position.x <= -15)
        {
            Destroy(gameObject);
        }
    }
}
