using UnityEngine;

public class Stack : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        GetComponentInParent<GardenBedController>().TakeStackToPlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
    }
}
