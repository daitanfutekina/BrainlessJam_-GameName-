using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleManager : MonoBehaviour
{
    public WhackConfig config;
    public GameObject molePrefab;
    public Transform spawnPointsParent; // parent that contains spawn point children
    public Text scoreText;
    public Text timerText;

    List<Transform> spawnPoints = new List<Transform>();
    List<Mole> pool = new List<Mole>();
    int score = 0;
    float endTime;
    bool running = false;

    void Start()
    {
        // gather spawn points
        spawnPoints.Clear();
        foreach (Transform t in spawnPointsParent) spawnPoints.Add(t);

        // create pool
        for (int i = 0; i < config.poolSize; i++)
        {
            var go = Instantiate(molePrefab, transform);
            var mole = go.GetComponent<Mole>();
            pool.Add(mole);
        }

        StartRound();
    }

    void StartRound()
    {
        score = 0;
        UpdateScoreUI();
        endTime = Time.time + config.gameDuration;
        running = true;
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (running && Time.time < endTime)
        {
            int activeCount = CountActive();
            if (activeCount < config.maxSimultaneous)
            {
                TrySpawnOne();
            }
            float wait = Random.Range(config.spawnIntervalMin, config.spawnIntervalMax);
            yield return new WaitForSeconds(wait);
        }
        running = false;
        EndRound();
    }

    void Update()
    {
        if (!running) return;
        var timeLeft = Mathf.Max(0, endTime - Time.time);
        if (timerText) timerText.text = Mathf.CeilToInt(timeLeft).ToString();
    }

    int CountActive()
    {
        int c = 0;
        foreach (var m in pool) if (m.IsActive()) c++;
        return c;
    }

    void TrySpawnOne()
    {
        // pick random spawn point
        var freeSpawns = new List<Transform>(spawnPoints);
        // optionally exclude occupied spawns (if desired).
        if (freeSpawns.Count == 0) return;
        var spawn = freeSpawns[Random.Range(0, freeSpawns.Count)];

        // get an inactive mole
        Mole mole = pool.Find(m => !m.IsActive());
        if (mole == null) return;

        mole.Activate(spawn.position, config.moleUpTime, OnMoleFinished);
    }

    void OnMoleFinished(Mole m, bool wasHit)
    {
        if (wasHit)
        {
            score += config.scorePerHit;
            UpdateScoreUI();
            // play sfx, particles, etc
        }
        // else do nothing
    }

    void UpdateScoreUI()
    {
        if (scoreText) scoreText.text = score.ToString();
    }

    void EndRound()
    {
        // stop all active moles
        foreach (var m in pool) if (m.IsActive()) m.SendMessage("Finish", false, SendMessageOptions.DontRequireReceiver);
        // show round over UI / callback
        Debug.Log("round over. final score: " + score);
    }
}
