using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    public GameObject ItemInHands;
    public Text PlantsCountText;
    public int PlantsCount;

    private void Awake()
    {
        instance = this;
        ItemInHands = null;
        PlantsCount = 3;
    }

    public void AddPlant()
    {
        PlantsCount++;
        PlantsCountText.text = $"Plants: {PlantsCount} / 4";
    }
}
