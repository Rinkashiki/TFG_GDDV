using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private float maxHealth;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image healthBar;
    [SerializeField] private Material fade;
    [SerializeField] private Text gameText;
    [SerializeField] private Slider musicBar;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Enemy enemy;

    // Canvas Group
    [SerializeField] private CanvasGroup inGameGroup;
    [SerializeField] private CanvasGroup endGameGroup;

    private float lerpStep;
    private float startTime;

    private float health;
    private bool endGame;

    private void Awake()
    {
        fade.color = new Color(fade.color.r, fade.color.r, fade.color.b, 0);
        startTime = Time.time;
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            gameText.text = "¡Has perdido!";
            EndGame();   
        }

        if (!audioSource.isPlaying)
        {
            gameText.text = "¡Has ganado!";
            EndGame();
        }
        else
        {
            musicBar.value = (Time.time - startTime) / audioSource.clip.length;
        }
    }

    public void SetHealth(float bulletDamage)
    {
        health -= bulletDamage;
        healthBar.fillAmount = health / maxHealth;
    }

    private void EndGame()
    {
        lerpStep += Time.deltaTime;
        endGameGroup.alpha = Mathf.Lerp(0, 1, lerpStep);
        endGameGroup.interactable = true;
        inGameGroup.alpha = Mathf.Lerp(1, 0, lerpStep);
        fade.color = Color.Lerp(new Color(fade.color.r, fade.color.g, fade.color.b, 0), new Color(fade.color.r, fade.color.g, fade.color.b, 1), lerpStep);
        audioSource.volume = Mathf.Lerp(1, 0, lerpStep);
        

        if (!endGame)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            enemy.StopAllCoroutines();
            endGame = true;
        }
    }

    private void OnApplicationQuit()
    {
        fade.color = new Color(fade.color.r, fade.color.r, fade.color.b, 0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Ikaruga");
    }
}