using EZDoor;
using UnityEngine;

public class PickupKey : MonoBehaviour, IInteractable
{
    public Key key;
    public string playerTag;
    private KeyContainer keyContainer;

    private void Awake()
    {
        keyContainer = GameObject.FindWithTag(playerTag).GetComponent<KeyContainer>();
    }

    public void Pickup()
    {
        keyContainer.keys.Add(key);
        Destroy(gameObject);
    }

    public void Interact()
    {
        Pickup();
    }
}
