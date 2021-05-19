using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    public ColorContainer _colorContainer;
    public SkinnedMeshRenderer _mesh;
    public SnakeController _snakeController;
    public GameManager _gameManager;

    private void Awake()
    {
        _snakeController = GameObject.Find("Snake").GetComponent<SnakeController>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void SetColorContainer(ColorContainer colorCont)
    {     
        _colorContainer = colorCont;
        Material[] mats = { colorCont.unitsMaterial, colorCont.unitsMaterial };
        _mesh.materials = mats;      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SnakeFoodCollision")
        {
            if (_snakeController._curColorContainer == _colorContainer)
            {
                _gameManager.GetFood();
                gameObject.SetActive(false);
            }
            else
            {
                _gameManager.LevelFailed();
            }
        }

        if (other.tag == "FeverCollider")
        {
            _gameManager.GetFood();
            gameObject.SetActive(false);
        }
    }
}
