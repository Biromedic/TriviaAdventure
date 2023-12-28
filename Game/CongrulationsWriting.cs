using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongrulationsWriting : MonoBehaviour
{
    public List<GameObject> writings;
    void Start()
    {
        GameEvent.ShowCongrulationWritings += ShowCongrulationWritings;
    }

    private void OnDisable()
    {
        GameEvent.ShowCongrulationWritings -= ShowCongrulationWritings;
    }

    private void ShowCongrulationWritings()
    {
        var randomIndex = Random.Range(0, writings.Count);
        writings[randomIndex].SetActive(true);
    }
}
