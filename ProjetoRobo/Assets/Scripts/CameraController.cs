using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private GameObject player;
    private Vector3 offset;
    public Transform shakeOffset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
