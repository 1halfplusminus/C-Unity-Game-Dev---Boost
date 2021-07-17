using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    START_LEVEL, PLAYING, TRANSITIONING, END_LEVEL
}
public class Player : MonoBehaviour
{
    [SerializeField]
    float LevelLoadDelay = 2f;
    [SerializeField]
    AudioClip crashSound;
    [SerializeField]
    AudioClip levelCompleteSound;
    [SerializeField]
    ParticleSystem crashEffect;
    Movement movement;
    AudioSource audioSource;

    private PlayerState state;
    void Start()
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
        state = PlayerState.START_LEVEL;
        StartCoroutine(WaitForFirstMouvement());
    }
    IEnumerator WaitForFirstMouvement()
    {
        yield return new WaitUntil(() => movement.spacePressed);
        state = PlayerState.PLAYING;
    }
    private IEnumerator ReloadSceneAfterDelay()
    {
        yield return new WaitForSeconds(LevelLoadDelay);
        SceneManager.LoadSceneAsync(GetCurrentScene());
    }
    public void OnCollisionWithObstacle()
    {
        if (state == PlayerState.TRANSITIONING) { return; }
        StartCoroutine(ReloadSceneAfterDelay());
        movement.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashEffect.Play();
        state = PlayerState.TRANSITIONING;
    }
    public void OnCollisionWithEndPoint()
    {
        if (state == PlayerState.TRANSITIONING) { return; }
        if (state != PlayerState.TRANSITIONING)
        StartCoroutine(LoadNextLevelCoroutine());
        movement.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(levelCompleteSound);
        state = PlayerState.TRANSITIONING;
    }
    private IEnumerator LoadNextLevelCoroutine()
    {
        yield return new WaitForSeconds(LevelLoadDelay + 3);
        int nextSceneIndex = GetCurrentScene() + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadSceneAsync(nextSceneIndex);
        state = PlayerState.END_LEVEL;
    }

    public int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
