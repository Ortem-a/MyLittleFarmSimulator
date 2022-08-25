using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public GameObject ItemInHands;

    private void Awake()
    {
        instance = this;
        ItemInHands = null;
    }
}
