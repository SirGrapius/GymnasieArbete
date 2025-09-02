using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float cameraSpeed;
    Vector3 zOffset;

    void Start()
    {
        zOffset = transform.position - playerTransform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + zOffset, Time.deltaTime * cameraSpeed);
    }
}
