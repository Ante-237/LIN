using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObject : MonoBehaviour
{
    [SerializeField]
    [Range(0, 20)] private float RotationSpeed = 1.0f;
    [SerializeField] private Vector3 Direction = Vector3.up;

    void Update()
    {
        transform.Rotate(Direction * Time.deltaTime * RotationSpeed);
    }
}
