using UnityEngine;

public class GardenBedController : Player
{
    // �������� ������ ����� -> ����� ��� ����� ->    ������ ������� -> �������� �������� -> ������
    // (����� ������)          (����� ������� ������) (����� ��������)                       (����� �������)
    // (�������� ���� ����)

    [SerializeField] private GameObject OverGrownGardenBedPrefab; // ������ �������� ������
    [SerializeField] private GameObject CleanedGardenBedPrefab; // ������ �� �������� ������
    [SerializeField] private GameObject FreeGardenBedPrefab; // ������ �� ���������� ������
    [SerializeField] private GameObject StackPrefab; // ������ ����� ����

    private GameObject _currentPrefab; // ������, ����������� ��� ����������� �������� �������� �� �����
    protected GameObject _stack;

    private bool _isOverGrown; // ������� ������
    private bool _isHaveNoGardenBed; // ������ �� ����� ���
    private bool _isReady; // ������ ����� � ������
    private bool _canPlant; // ����� �� ������ �� ������

    private void Awake()
    {
        _isOverGrown = true;
        _isHaveNoGardenBed = true;
        _canPlant = false;
        _isReady = false;
        
        _currentPrefab = Instantiate(OverGrownGardenBedPrefab, transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            if (instance.ItemInHands != null)
            {
                // ���� ������ �������� � � ������ � ����� ����, �� �������� �� �����
                if (_isOverGrown && (instance.ItemInHands.tag == "Sickle"))
                {
                    Debug.Log($"CLEAN {this.name} FROM OVERGROWN");

                    UpdatePrefab(CleanedGardenBedPrefab);
                    _isOverGrown = false;

                    DropOutStack(); // �������� ���� ����
                }
                // ���� ������ ���, � ����� �������� � � ����� � ������ �������, �� ����� ������� ������
                else if (_isHaveNoGardenBed && (instance.ItemInHands.tag == "Hammer"))
                {
                    Debug.Log($"BUILD GARDEN BED IN {this.name}");

                    UpdatePrefab(FreeGardenBedPrefab);
                    _isHaveNoGardenBed = false;
                    _canPlant = true;
                }
                // ������ ����, ��� ��������, � ����� � ������ ������, �� ����� ������
                else if (_canPlant && (instance.ItemInHands.tag == "Shovel"))
                {
                    Debug.Log($"PLANT smth IN {this.name}");

                    GetComponentInChildren<PlantGrownController>().ChangePlantState(); // ��������

                    _canPlant = false; // ��� ���-�� ������
                }
            }
        }
    }

    private void UpdatePrefab(GameObject newPrefab)
    {
        Destroy(_currentPrefab);
        _currentPrefab = Instantiate(newPrefab, transform);
        newPrefab.transform.position = Vector3.zero;
    }

    private void DropOutStack()
    {
        // �������� ���� ����
        _stack = Instantiate(StackPrefab, transform);
        _stack.transform.position = transform.position + new Vector3(2, 0, -4);
        _stack.transform.rotation = Quaternion.Euler(-90, 0, 0);
    }

    public void TakeStackToPlayer()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.LogWarning($"PLAYER TAKE {this.name}");

            if (instance.ItemInHands != null) // � ����� ���-�� ����
            {
                Debug.LogWarning("HANDS ARE BUSY");
            }
            else // ���� ��������
            {
                Debug.LogWarning("TAKE STACK TO PLAYER");
                instance.ItemInHands = _stack;
                Destroy(_stack);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
    }
}
