using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource levelMusic;
    public AudioSource bossMusic;

    [Header("Player Constraints")]
    public GameObject player;
    public Transform leftBoundary;
    public Transform bossLeftBoundary;
    public Transform bossRightBoundary;

    private PlayerScript ps;
    private Rigidbody2D rb;
    private GameObject bossSpawnerObject;
    public bool isBossFight = false;

    void Start()
    {
        levelMusic.volume = 0.10f;
        levelMusic.Play();
        ps = player.GetComponent<PlayerScript>();
        rb = player.GetComponent<Rigidbody2D>();
        bossSpawnerObject = GameObject.FindGameObjectWithTag("BossSpawners");
        bossSpawnerObject.SetActive(false);
    }

    void Update()
    {
        if (player.transform.position.x <= leftBoundary.position.x)
        {
            player.transform.position = new Vector3(leftBoundary.position.x, player.transform.position.y, player.transform.position.z);
        }

        if (player.transform.position.x > bossLeftBoundary.position.x && isBossFight == false)
        {
            StartCoroutine(AudioFadeOut.FadeOut(levelMusic, 0.5f));
            bossMusic.volume = 0.10f;
            bossMusic.Play();
            isBossFight = true;
            bossSpawnerObject.SetActive(true);
            rb.gravityScale = 2.0f;
            ps.jumpForce = 14.0f;
        }

        if (isBossFight != false)
        {
            if (player.transform.position.x <= bossLeftBoundary.position.x)
            {
                player.transform.position = new Vector3(bossLeftBoundary.position.x, player.transform.position.y, player.transform.position.z);
            }

            if (player.transform.position.x >= bossRightBoundary.position.x)
            {
                player.transform.position = new Vector3(bossRightBoundary.position.x, player.transform.position.y, player.transform.position.z);
            }
        }
    }
}

class AudioFadeOut
{
    public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
