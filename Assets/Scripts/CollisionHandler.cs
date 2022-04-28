using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with Friendly Object!");
                break;
            case "Finish":
                Debug.Log("Made it to the Finish Line!");
                break;
            case "Fuel":
                Debug.Log("Grabed some Fuel!");
                break;
            default:
                Debug.Log("You hit debris!");
                break;
        }
    }
}
