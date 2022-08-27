using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrownController : Player
{
    // состояния растений
    public GameObject[] PlantPrefabs;

    private GameObject _currentPrefab;

    public float TimeOfGrown;

    public void ChangePlantState()
    {
        StartCoroutine(WaitTimeOfGrown1()); // ожидание пока вырастет
    }

    private IEnumerator WaitTimeOfGrown1()
    {
        _currentPrefab = Instantiate(PlantPrefabs[0], transform); // спавн префаба растения
        _currentPrefab.transform.position += new Vector3(0, 0.4f, 0);

        Debug.LogWarning($"1) WAIT FOR {TimeOfGrown} sec");
        yield return new WaitForSeconds(TimeOfGrown);

        Destroy(_currentPrefab); // удаление старого префаба
        StartCoroutine(WaitTimeOfGrown2()); // ожидание пока вырастет
    }

    private IEnumerator WaitTimeOfGrown2()
    {
        _currentPrefab = Instantiate(PlantPrefabs[1], transform); // спавн префаба растения
        _currentPrefab.transform.position += new Vector3(0, 0.4f, 0);

        Debug.LogWarning($"2) WAIT FOR {TimeOfGrown} sec");
        yield return new WaitForSeconds(TimeOfGrown);

        Destroy(_currentPrefab); // удаление старого префаба
        StartCoroutine(WaitTimeOfGrown3()); // ожидание пока вырастет
    }

    private IEnumerator WaitTimeOfGrown3()
    {
        _currentPrefab = Instantiate(PlantPrefabs[2], transform); // спавн префаба растения
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
