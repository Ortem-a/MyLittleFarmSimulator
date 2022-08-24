using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBedController : Player
{
    // заросшее травой место -> место без травы ->    грядка сделана -> растение посажено -> урожай
    // (можно косить)          (можно сделать грядку) (можно засадить)                       (можно собрать)

    public GameObject OverGrownGardenBedPrefab; // префаб заросшей грядки
    public GameObject CleanedGardenBedPrefab; // префаб не заросшей грядки
    public GameObject FreeGardenBedPrefab; // префаб не засаженной грядки

    //public GameObject PlantedGardenBedPrefab; // префаб засаженной грядки

    private GameObject currentPrefab; // объект, необходимый для корректного удаления префабов со сцены

    private bool _isOverGrown; // заросла травой
    private bool _isHaveNoGardenBed; // грядки на месте нет
    private bool _isFree; // ничем не засажена
    private bool _canPlant; // можно ли сажать на грядку

    private void Awake()
    {
        _isOverGrown = true;
        _isHaveNoGardenBed = true;
        _canPlant = false;
        _isFree = true;
        
        currentPrefab = Instantiate(OverGrownGardenBedPrefab, transform);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("AROUND GARDEN BED");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isOverGrown) // && Player.HaveSickle)
            {
                Destroy(currentPrefab);
                currentPrefab = Instantiate(CleanedGardenBedPrefab, transform);
                CleanedGardenBedPrefab.transform.position = Vector3.zero;
                _isOverGrown = false;
            }
            else if (_isHaveNoGardenBed) // && Player.HaveHammer)
            {
                Destroy(currentPrefab);
                currentPrefab = Instantiate(FreeGardenBedPrefab, transform);
                FreeGardenBedPrefab.transform.position = Vector3.zero;
                _isHaveNoGardenBed = false;
                _canPlant = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
    }
}
