using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartingSettings : MonoBehaviour
{
    public ColorContainer[] _colorContainersArray;
    public SnakeController _snakeController;

    public GameObject _checkPointPrefab;
    public float _distanceBetweenCheckpoints; // расстояние между чекпоинтами
    public int _checkPointCounter; // последний чекпоинт - конец уровня

    public ColorContainer _lastRandomedRightColorContainer;
    public ColorContainer _lastRandomedWrongColorContainer;
    public List<ColorContainer> _availableColorContainersList;

    private void Awake()
    {
        

        for(int i = 0; i < _checkPointCounter; i++)
        {
            AddCheckpoint(i);

                
        }

        
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }


    private void AddCheckpoint(int checkpointNumber)
    {
        Vector3 pos = new Vector3(0, 0, checkpointNumber * _distanceBetweenCheckpoints);
        GameObject checkpoint = Instantiate(_checkPointPrefab, pos, Quaternion.identity, transform);
        CheckpointController checkpointController = checkpoint.transform.GetChild(0).GetComponent<CheckpointController>();

        ColorContainer[] contArray = GetRandomColorContainers();

        checkpointController._rightColorContainer = contArray[0];
        checkpointController._wrongColorContainer = contArray[1];

        // если это нулевой чекпоинт, сразу задаем цвет змейке
        if (checkpointNumber == 0)
        {
            _snakeController.SetMaterial(contArray[0]);
            checkpoint.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }

        checkpointController.CreateFoodContent(checkpoint.transform);
        checkpointController.CreateTrapContent(checkpoint.transform);
    }

    public ColorContainer[] GetRandomColorContainers()
    {
        for (int i = 0; i < _colorContainersArray.Length; i++)
        {
            _availableColorContainersList.Add(_colorContainersArray[i]);
        }

        if (_lastRandomedRightColorContainer == null && _lastRandomedWrongColorContainer == null)
        {
            _lastRandomedRightColorContainer = _availableColorContainersList[Random.Range(0, _availableColorContainersList.Count)];
            _availableColorContainersList.Remove(_lastRandomedRightColorContainer);

            _lastRandomedWrongColorContainer = _availableColorContainersList[Random.Range(0, _availableColorContainersList.Count)];
            _availableColorContainersList.Remove(_lastRandomedWrongColorContainer);
        }
        else
        {          
            ColorContainer tempCont = _lastRandomedRightColorContainer;
            _availableColorContainersList.Remove(_lastRandomedRightColorContainer);
            _lastRandomedRightColorContainer = _availableColorContainersList[Random.Range(0, _availableColorContainersList.Count)];
            _availableColorContainersList.Remove(_lastRandomedRightColorContainer);
            _availableColorContainersList.Add(tempCont);

            tempCont = _lastRandomedWrongColorContainer;
            _availableColorContainersList.Remove(_lastRandomedWrongColorContainer);
            _lastRandomedWrongColorContainer = _availableColorContainersList[Random.Range(0, _availableColorContainersList.Count)];
            _availableColorContainersList.Add(tempCont);         
        }

        ColorContainer[] returnArray = {_lastRandomedRightColorContainer, _lastRandomedWrongColorContainer };

        _availableColorContainersList.Clear();

        return returnArray;
    }
}
