using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Player
{
    public GameObject StartUI;
    public GameObject FinishUI;
    public GameObject NotReadyYetUI;
    public GameObject HintsUI;

    private void Awake()
    {
        StartUI.SetActive(true);
        FinishUI.SetActive(false);
        NotReadyYetUI.SetActive(false);
        HintsUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToMainMenu();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            HintsOn();
        }
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
        
        ToMainMenu();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void HintsOn()
    {
        HintsUI.SetActive(true);
    }

    public void HintsOff()
    {
        HintsUI.SetActive(false);
    }

    public void StartUIOff()
    {
        StartUI.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}
