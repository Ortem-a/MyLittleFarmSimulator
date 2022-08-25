using UnityEngine;
using UnityEngine.UI;

public class ShelterController : Player
{
    public GameObject ShelterUI;

    public GameObject Hammer;
    public GameObject Sickle;
    public GameObject Shovel;

    private Image _hammerButton;
    private Image _sickleButton;
    private Image _shovelButton;
    private Image _freeHandsButton;

    private void Awake()
    {
        ShelterUI.SetActive(false);

        _hammerButton = ShelterUI.GetComponentsInChildren<Image>()[0];
        _shovelButton = ShelterUI.GetComponentsInChildren<Image>()[1];
        _sickleButton = ShelterUI.GetComponentsInChildren<Image>()[2];
        _freeHandsButton = ShelterUI.GetComponentsInChildren<Image>()[3];

        _hammerButton.color = Color.green;
        _shovelButton.color = Color.green;
        _sickleButton.color = Color.green;
        _freeHandsButton.color = Color.yellow;
    }

    private void OnTriggerEnter(Collider other)
    {
        ShelterUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        ShelterUI.SetActive(false);
    }

    // ����� ����������
    public void TakeTool(GameObject tool)
    {
        if (instance.ItemInHands != null) // ���� � ����� ��� ���-�� ����
        {
            Debug.Log($"ALREADY HAVE { instance.ItemInHands.tag } IN HAND");
            if (instance.ItemInHands.tag == "Stack") // ���� ��� ����, �� ��� ���� ������� ������ �� �����
            {
                // ����� ������ ������
                return;
            }
            PutToolInPlace(instance.ItemInHands.tag);
        }
        Debug.Log($"TOOL TAKEN { tool.tag }");
        instance.ItemInHands = Instantiate(tool, instance.transform); // ����� ����������       
        UpdateToolButton(tool.tag); // ������ ���������� �� �����
    }

    private void UpdateToolButton(string toolTag)
    {
        Debug.Log($"Update tool button { toolTag }");
        // ����� �������
        if (toolTag == "Hammer")
        {
            _hammerButton.color = Color.red;
            Hammer = null;
        }
        // ����� ����
        else if (toolTag == "Sickle")
        {
            _sickleButton.color = Color.red;
            Sickle = null;
        }
        // ����� ������
        else if (toolTag == "Shovel")
        {
            _shovelButton.color = Color.red;
            Shovel = null;
        }
    }

    public void FreeHands()
    {
        Debug.Log("FREE HANDS");
        if (instance.ItemInHands != null)
        {
            PutToolInPlace(instance.ItemInHands.tag);
        }
    }

    private void PutToolInPlace(string tag)
    {
        Debug.Log($"PUT IN PLACE { tag }");
        // �������� �������
        if (Hammer == null && tag == "Hammer")
        {
            Hammer = instance.ItemInHands;
            _hammerButton.color = Color.green;
        }
        // �������� ����
        else if (Sickle == null && tag == "Sickle")
        {
            Sickle = instance.ItemInHands;
            _sickleButton.color = Color.green;
        }
        // �������� ������
        else if (Shovel == null && tag == "Shovel")
        {
            Shovel = instance.ItemInHands;
            _shovelButton.color = Color.green;
        }
        Destroy(instance.ItemInHands); // ������ �� ���, �� ��� ������� � �����
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(7.5f, 0, 0), GetComponent<BoxCollider>().size * 10);
    }
}
