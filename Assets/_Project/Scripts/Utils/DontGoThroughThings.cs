using UnityEngine;
using System.Collections;

public class DontGoThroughThings : MonoBehaviour
{
    // Careful when setting this to true - it might cause double
    // events to be fired - but it won't pass through the trigger
    public bool sendTriggerMessage = false;

    public LayerMask layerMask = -1; //make sure we aren't in this layer
    public float skinWidth = 0.1f; //probably doesn't need to be changed

    private float minimumExtent;
    private Collider myCollider;
    private Rigidbody myRigidbody;
    private float partialExtent;
    private Vector3 previousPosition;
    private float sqrMinimumExtent;

    //initialize values
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        previousPosition = myRigidbody.position;
        minimumExtent = Mathf.Min(Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y),
            myCollider.bounds.extents.z);
        partialExtent = minimumExtent * (1.0f - skinWidth);
        sqrMinimumExtent = minimumExtent * minimumExtent;
    }

    void FixedUpdate()
    {
        //have we moved more than our minimum extent?
        Vector3 movementThisStep = myRigidbody.position - previousPosition;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;

        if (movementSqrMagnitude > sqrMinimumExtent)
        {
            float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
            RaycastHit hitInfo;

            //check for obstructions we might have missed
            if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
            {
                if (!hitInfo.collider)
                    return;

                if (hitInfo.collider.isTrigger)
                    hitInfo.collider.SendMessage("OnCollisionEnter", myCollider);

                if (!hitInfo.collider.isTrigger)
                    myRigidbody.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent;
            }
        }

        previousPosition = myRigidbody.position;
    }

    private void OnCollisionEnter (Collision collision)
    {
        Debug.Log($"Collided with: {collision.transform.name}");

        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Health>().TakeDamage(10);
            Destroy(gameObject);

        }
        else if(collision.transform.CompareTag("Enemy"))
        {
            Destroy(collision.transform);
        }

        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        // Instantiate(hitParticlePrefab, position, rotation);
        Destroy(gameObject);
    }
}