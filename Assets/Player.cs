using System.Collections;
using System.Collections.Generic;
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

    public void UpdateHands(GameObject tool)
    {
        ItemInHands = Instantiate(tool, transform);
    }
}
