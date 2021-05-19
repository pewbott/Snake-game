using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public ColorContainer _rightColorContainer;
    public ColorContainer _wrongColorContainer;
    private MeshRenderer _checkpointMeshRenderer;

    //  public GameObject _foodPartyPrefab;

    public Transform _foodSpawnSpot;
    public Transform _trapSpawnSpot
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

   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SnakePart" || other.tag == "SnakeHead")
        {
            other.GetComponent<MeshRenderer>().material = _rightColorContainer.unitsMaterial;
            _snakeController._curColorContainer = _rightColorContainer;
        }
    }

    
    public void CreateFoodContent(Transform parent)
    {
        GameObject go = Instantiate(_foodContentPrefab, _foodSpawnSpot.position, Quaternion.identity, parent);
        FoodContentContoller foodContentController = go.GetComponent<FoodContentContoller>();
        foodContentController._checkpointController = this;
        foodContentController.CreateFoodContent();
    }

    public void CreateTrapContent(Transform parent)
    {
        Instantiate(_trapContentsPrefabArray[Random.Range(0, _trapContentsPrefabArray.Length)], _trapSpawnSpot.position, Quaternion.identity, parent);
    }
    
}
