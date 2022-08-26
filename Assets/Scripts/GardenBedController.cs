using UnityEngine;

public class GardenBedController : Player
{
    // заросшее травой место -> место без травы ->    грядка сделана -> растение посажено -> урожай
    // (можно косить)          (можно сделать грядку) (можно засадить)                       (можно собрать)
    // (вылетает стог сена)

    [SerializeField] private GameObject OverGrownGardenBedPrefab; // префаб заросшей грядки
    [SerializeField] private GameObject CleanedGardenBedPrefab; // префаб не заросшей грядки
    [SerializeField] private GameObject FreeGardenBedPrefab; // префаб не засаженной грядки
    [SerializeField] private GameObject StackPrefab; // префаб снопа сена

    private GameObject _currentPrefab; // объект, необходимый для корректного удаления префабов со сцены
    protected GameObject _stack;

    private bool _isOverGrown; // заросла травой
    private bool _isHaveNoGardenBed; // грядки на месте нет
    private bool _isReady; // урожай готов к сборке
    private bool _canPlant; // можно ли сажать на грядку

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
                // если грядка заросшая и у игрока в руках серп, то очистить от травы
                if (_isOverGrown && (instance.ItemInHands.tag == "Sickle"))
                {
                    Debug.Log($"CLEAN {this.name} FROM OVERGROWN");

                    UpdatePrefab(CleanedGardenBedPrefab);
                    _isOverGrown = false;

                    DropOutStack(); // вылетает стог сена
                }
                // если грядки нет, а трава почищена и в руках у игрока молоток, то можно сделать грядку
                else if (_isHaveNoGardenBed && (instance.ItemInHands.tag == "Hammer"))
                {
                    Debug.Log($"BUILD GARDEN BED IN {this.name}");

                    UpdatePrefab(FreeGardenBedPrefab);
                    _isHaveNoGardenBed = false;
                    _canPlant = true;
                }
                // грядка есть, она свободна, в руках у игрока лопата, то можно сажать
                else if (_canPlant && (instance.ItemInHands.tag == "Shovel"))
                {
                    Debug.Log($"PLANT smth IN {this.name}");

                    GetComponentInChildren<PlantGrownController>().ChangePlantState(); // посадить

                    _canPlant = false; // уже что-то растет
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
        // вылетает стог сена
        _stack = Instantiate(StackPrefab, transform);
        _stack.transform.position = transform.position + new Vector3(2, 0, -4);
        _stack.transform.rotation = Quaternion.Euler(-90, 0, 0);
    }

    public void TakeStackToPlayer()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.LogWarning($"PLAYER TAKE {this.name}");

            if (instance.ItemInHands != null) // в руках что-то есть
            {
                Debug.LogWarning("HANDS ARE BUSY");
            }
            else // руки свободны
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
