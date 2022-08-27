using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GardenBedController : Player
{
    // заросшее травой место -> место без травы ->    грядка сделана -> растение посажено -> урожай
    // (можно косить)          (можно сделать грядку) (можно засадить)                       (можно собрать)
    // (вылетает стог сена)

    public GameObject OverGrownGardenBedPrefab; // префаб заросшей грядки
    public GameObject CleanedGardenBedPrefab; // префаб не заросшей грядки
    public GameObject FreeGardenBedPrefab; // префаб не засаженной грядки
    public GameObject StackPrefab; // префаб снопа сена
    public GameObject Hint; // подсказки пользователю

    private GameObject _currentPrefab; // объект, необходимый для корректного удаления префабов со сцены
    protected GameObject _stack;

    private bool _isOverGrown; // заросла травой
    private bool _isHaveNoGardenBed; // грядки на месте нет
    private bool _isReady; // урожай готов к сборке
    private bool _canPlant; // можно ли сажать на грядку

    private void Awake()
    {
        Hint.SetActive(false);

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
                    StartCoroutine(WaitUntilGrown());
                }
                else if (_isReady)
                {
                    Debug.Log($"HANDS ARE BUSY BY {instance.ItemInHands.name}");
                    Hint.GetComponent<Text>().text = "Ваши руки заняты";
                    Hint.SetActive(true);
                }
            }
            else if (_isReady) // урожай готов к сборке
            {
                Debug.Log("TRY TO TAKE READY PLANT");

                GetComponentInChildren<PlantGrownController>().TakeReadyPlant();
                instance.AddPlant(); // Update UI
                _isReady = false;
            }
        }
    }

    private IEnumerator WaitUntilGrown()
    {
        yield return new WaitForSeconds(GetComponentInChildren<PlantGrownController>().TimeOfGrown * 2);

        _isReady = true;
        Debug.Log($"PLANT IS READY {_isReady}");
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
        _stack.transform.position = transform.position + new Vector3(2, 0, -3.5f);
        _stack.transform.rotation = Quaternion.Euler(-90, 0, 0);
        _stack.tag = "Stack";
    }

    public void TakeStackToPlayer()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"PLAYER TAKE {this.name}");

            if (instance.ItemInHands != null) // в руках что-то есть
            {
                Debug.Log("HANDS ARE BUSY");
                Hint.GetComponent<Text>().text = "Ваши руки заняты";
                Hint.SetActive(true);
            }
            else // руки свободны
            {
                Debug.Log("TAKE STACK TO PLAYER");
                Hint.SetActive(false);
                //instance.ItemInHands = _stack;
                Destroy(_stack);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Hint.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
    }
}
