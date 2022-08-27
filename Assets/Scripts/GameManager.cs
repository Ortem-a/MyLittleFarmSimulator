using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Player
{
    public GameObject FinishUI;
    public GameObject NotReadyYetUI;

    private void Awake()
    {
        FinishUI.SetActive(false);
        NotReadyYetUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (instance.PlantsCount >= 4)
        {
            FinishUI.SetActive(true);
            StartCoroutine(EndGame());
        }
        else
        {
            NotReadyYetUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        NotReadyYetUI.SetActive(false);
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}
