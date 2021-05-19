using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SnakeFoodCollision")
        {
            _gameManager.GetCrystal();
            gameObject.SetActive(false);
        }

        if (other.tag == "FeverCollider")
        {
            gameObject.SetActive(false);
        }
    }
}
