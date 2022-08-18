using UnityEngine;

public class ObjectiveScript : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private Vector3 positionOffset;
    public bool IsBeingHeld;

    void Update()
    {
        if(IsBeingHeld)
        {
            transform.position = Player.transform.position - positionOffset;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == Player.name)
        {
            IsBeingHeld = true;
        }
    }
}
