using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMangre : MonoBehaviour
{

    [SerializeField] private PlayerHealth playerHealth;
    int currentBuildIndex;

    private void Start()
    {
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (playerHealth == null) { Debug.Log("Player health is null"); }
        if (currentBuildIndex == 0) { Debug.LogError("currentBuildIndex == null"); }
    }
    private void Update()
    {
        // if (playerHealth != null && playerHealth.health <= 0)
        // {
        //     LoadScene(currentBuildIndex);
        // }
    }
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
