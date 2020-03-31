using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public WaveManager waveManager;
    public StatsManager stats;

    private bool waiting = true;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        gameOverText.text = "<color=#A4A4A4><size=100>WAVE "+ waveManager.wave + "</size><br><size=60> " + stats.enemiesKilled + " Enemies Killed</size></color>" +
            "<br><br><color=#FF9292>GAME OVER</color><br><br><br>" +
            "<size=80>Click anywhere to return to menu </size>";
        StartCoroutine("Wait");
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator Wait()
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            yield return null;
        }
        waiting = false;
    }
}
