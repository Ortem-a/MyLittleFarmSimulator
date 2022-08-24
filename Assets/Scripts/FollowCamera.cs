using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject player; // тут объект игрока

    [Range(0f, 40f)]
    public float offsetY;
    [Range(-40f, 0f)]
    public float offsetZ;
    [Range(0f, 90f)]
    public float rotationX;

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, offsetY, offsetZ);

        transform.position = player.transform.position + offset;
        transform.Rotate(new Vector3(rotationX, 0, 0));
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        //transform.rotation = new Quaternion(rotationX, 0, 0);
    }
}
