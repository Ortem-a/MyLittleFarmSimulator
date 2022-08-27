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
        StartCoroutine(WaitTimeOfGrown1()); // �������� ���� ��������
    }

    private IEnumerator WaitTimeOfGrown1()
    {
        _currentPrefab = Instantiate(PlantPrefabs[0], transform); // ����� ������� ��������
        _currentPrefab.transform.position += new Vector3(0, 0.4f, 0);

        Debug.LogWarning($"1) WAIT FOR {TimeOfGrown} sec");
        yield return new WaitForSeconds(TimeOfGrown);

        Destroy(_currentPrefab); // �������� ������� �������
        StartCoroutine(WaitTimeOfGrown2()); // �������� ���� ��������
    }

    private IEnumerator WaitTimeOfGrown2()
    {
        _currentPrefab = Instantiate(PlantPrefabs[1], transform); // ����� ������� ��������
        _currentPrefab.transform.position += new Vector3(0, 0.4f, 0);

        Debug.LogWarning($"2) WAIT FOR {TimeOfGrown} sec");
        yield return new WaitForSeconds(TimeOfGrown);

        Destroy(_currentPrefab); // �������� ������� �������
        StartCoroutine(WaitTimeOfGrown3()); // �������� ���� ��������
    }

    private IEnumerator WaitTimeOfGrown3()
    {
        _currentPrefab = Instantiate(PlantPrefabs[2], transform); // ����� ������� ��������
        _currentPrefab.transform.position += new Vector3(0, 0.4f, 0);

        Debug.Log($"3) WAIT FOR {TimeOfGrown} sec");
        yield return new WaitForSeconds(TimeOfGrown);

        Debug.Log($"PLANT {PlantPrefabs[2].name} ON {this.name} IS READY");
    }

    public void TakeReadyPlant()
    {
        instance.ItemInHands = _currentPrefab;
        Destroy(_currentPrefab);
    }
}
