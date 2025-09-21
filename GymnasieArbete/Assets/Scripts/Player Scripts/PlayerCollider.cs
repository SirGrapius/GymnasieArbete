using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    [SerializeField] ContactFilter2D[] contactFilters;

    void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            contactFilters[i].useNormalAngle = true;
            contactFilters[i].minNormalAngle = 90 * i - 45;
            contactFilters[i].maxNormalAngle = 90 * i + 45;
        }

        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool onWallD = myRigidbody.IsTouching(contactFilters[1]);
        bool onWallL = myRigidbody.IsTouching(contactFilters[0]);
        bool onWallR = myRigidbody.IsTouching(contactFilters[2]);
        bool onWallU = myRigidbody.IsTouching(contactFilters[3]);
    }
}
