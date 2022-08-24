using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBedController : Player
{
    // �������� ������ ����� -> ����� ��� ����� ->    ������ ������� -> �������� �������� -> ������
    // (����� ������)          (����� ������� ������) (����� ��������)                       (����� �������)

    public GameObject OverGrownGardenBedPrefab; // ������ �������� ������
    public GameObject CleanedGardenBedPrefab; // ������ �� �������� ������
    public GameObject FreeGardenBedPrefab; // ������ �� ���������� ������

    //public GameObject PlantedGardenBedPrefab; // ������ ���������� ������

    private GameObject currentPrefab; // ������, ����������� ��� ����������� �������� �������� �� �����

    private bool _isOverGrown; // ������� ������
    private bool _isHaveNoGardenBed; // ������ �� ����� ���
    private bool _isFree; // ����� �� ��������
    private bool _canPlant; // ����� �� ������ �� ������

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
