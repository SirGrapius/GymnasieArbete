using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float cameraSpeed;
    [SerializeField] public bool inCombat;
    Vector3 zOffset;

    void Start()
    {
        zOffset = transform.position - playerTransform.position;
        inCombat = false;
    }

    void Update()
    {
        if (!inCombat)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position + zOffset, Time.deltaTime * cameraSpeed);
        }
        if (inCombat)
        {

        }
    }
}
