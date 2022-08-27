using UnityEngine;

public class KeyboardBindsController : MonoBehaviour
{
    public GameObject KeyboardBindsUI;

    private void Awake()
    {
        KeyboardBindsUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        KeyboardBindsUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        KeyboardBindsUI.SetActive(false);
    }
}
