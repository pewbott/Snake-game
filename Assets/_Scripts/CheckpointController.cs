using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public ColorContainer _rightColorContainer;
    public ColorContainer _wrongColorContainer;
    private MeshRenderer _checkpointMeshRenderer;

    //  public GameObject _foodPartyPrefab;

    public Transform _foodSpawnSpot; // позиция спавна части уровня с едой
    public Transform _trapSpawnSpot // позиция спавна части уровня с препятствиями и кристаллами
        ;
    public GameObject _foodContentPrefab;
    public GameObject[] _trapContentsPrefabArray;

    public SnakeController _snakeController;

    private void Awake()
    {
        _checkpointMeshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        _checkpointMeshRenderer.material = _rightColorContainer.checkpointMaterial;
        _snakeController = GameObject.Find("Snake").GetComponent<SnakeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // при контакте чекпоинта с объектами головы или другой части тела змеи, эта часть тела принимает материал цвета чекпоинта
        if (other.tag == "SnakePart" || other.tag == "SnakeHead")
        {
            other.GetComponent<MeshRenderer>().material = _rightColorContainer.unitsMaterial;
            _snakeController._curColorContainer = _rightColorContainer;
        }
    }

    // создание части уровня с едой
    public void CreateFoodContent(Transform parent)
    {
        GameObject go = Instantiate(_foodContentPrefab, _foodSpawnSpot.position, Quaternion.identity, parent);
        FoodContentContoller foodContentController = go.GetComponent<FoodContentContoller>();
        foodContentController._checkpointController = this;
        foodContentController.CreateFoodContent();
    }

    // создание части уровня с ловушками и кристаллами
    public void CreateTrapContent(Transform parent)
    {
        Instantiate(_trapContentsPrefabArray[Random.Range(0, _trapContentsPrefabArray.Length)], _trapSpawnSpot.position, Quaternion.identity, parent);
    }
    
}
