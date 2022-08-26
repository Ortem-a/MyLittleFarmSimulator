using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrownController : Player
{
    // ��������� ��������
    public GameObject[] PlantPrefabs;

    private GameObject _currentPrefab;

    public float TimeOfGrown;

    public void ChangePlantState()
    {
        for (int i = 0; i < 2; i++)
        {
            
            StartCoroutine(WaitTimeOfGrown(i)); // �������� ���� ��������
            //Destroy(_currentPrefab); // �������� ������� �������
        }
        //_currentPrefab = Instantiate(PlantPrefabs[2], transform); // ����� ������� ��������
    }

    private IEnumerator WaitTimeOfGrown(int index)
    {
        _currentPrefab = Instantiate(PlantPrefabs[index], transform); // ����� ������� ��������
        _currentPrefab.transform.position += new Vector3(0, 0.4f, 0);

        Debug.LogWarning($"WAIT FOR {TimeOfGrown} sec");
        yield return new WaitForSeconds(TimeOfGrown);

        Destroy(_currentPrefab); // �������� ������� ������
        ChangePlantState();
    }

    public void TakeReadyPlant()
    {
        instance.ItemInHands = _currentPrefab;
        Destroy(_currentPrefab);
    }
}
