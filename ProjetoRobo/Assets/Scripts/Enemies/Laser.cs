using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 5f;

    void Awake() {
        //transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);;
    }

    void Update() {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}