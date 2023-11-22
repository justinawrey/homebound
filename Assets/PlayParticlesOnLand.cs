using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticlesOnLand : MonoBehaviour
{
    [SerializeField] private ParticleSystem _system;
    [SerializeField] private int _minEmissionAmount = 3;
    [SerializeField] private int _maxEmissionAmount = 6;

    // Called from animation event
    public void OnLand()
    {
        _system.Emit(Random.Range(_minEmissionAmount, _maxEmissionAmount + 1));
    }
}
