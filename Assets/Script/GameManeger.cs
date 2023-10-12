using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManeger : MonoBehaviour
{
    public GameObject Gameover;
    public GameObject player;
    public GameObject enemy;
    public GameObject finishPoint;

    [Header("Visible Objects Falsing")]

    public GameObject Arrow;
    public GameObject Jump;
    public GameObject Shoot;
    public GameObject MiniMap;
    public GameObject Settings;
    public GameObject Ammo;
    public GameObject Health;
    public GameObject Lives;
    public GameObject Kills;
    public GameObject LevelCount;

    [SerializeField] Animator transitionAnim;
    public void GameOver()
    {
        player.SetActive(false);
        enemy.SetActive(false);
        Arrow.SetActive(false);
        Jump.SetActive(false);
        Shoot.SetActive(false); 
        MiniMap.SetActive(false);
        Settings.SetActive(false);
        Ammo.SetActive(false);  
        LevelCount.SetActive(false);
        Gameover.SetActive(true);
    }
    public void FinishPoint()
    {
        player.SetActive(false);
        enemy.SetActive(false);
        Arrow.SetActive(false);
        Jump.SetActive(false);
        Shoot.SetActive(false);
        MiniMap.SetActive(false);
        Settings.SetActive(false);
        Ammo.SetActive(false);
        Health.SetActive(false);
        Lives.SetActive(false);
        Kills.SetActive(false);
        LevelCount.SetActive(false);
        finishPoint.SetActive(true);
        AudioManeger.Instance.musicSource.Stop();
        AudioManeger.Instance.PlaySFX("LevelComplete");
       
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManeger.Instance.PlayMusic("Theme");
    }

    public void Next()
    {
        StartCoroutine(LoadLevel());
        UnlockNewLevel();
    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator LoadLevel()
    {
        finishPoint.SetActive(false);
        transitionAnim.SetTrigger("End");

        Arrow.SetActive(false);
        Jump.SetActive(false);
        Shoot.SetActive(false);
        MiniMap.SetActive(false);
        Settings.SetActive(false);
        Ammo.SetActive(false);
        Health.SetActive(false);
        Lives.SetActive(false);
        Kills.SetActive(false);
        LevelCount.SetActive(false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger("Start");
        AudioManeger.Instance.PlayMusic("Theme");
    }
    void UnlockNewLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex3"))
        {
            PlayerPrefs.SetInt("ReachedIndex3", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel3", PlayerPrefs.GetInt("UnlockedLevel3", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}

