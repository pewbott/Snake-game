using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartingSettings : MonoBehaviour
{
    public ColorContainer[] _colorContainersArray; // массив всех возможных цветов с материалами 
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

    // метод создания чекпоинтов
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

    // метод для рандомного задания цветов для чекпоинтов (правильных и неправильных)
    // каждый чекпоинт будет спавнить группы еды с правильным и неправильным цветом
    public ColorContainer[] GetRandomColorContainers()
    {
        for (int i = 0; i < _colorContainersArray.Length; i++) // заполняем массив доступных цветов
        {
            _availableColorContainersList.Add(_colorContainersArray[i]); 
        }

        if (_lastRandomedRightColorContainer == null && _lastRandomedWrongColorContainer == null) // если это первая итерация (начало уровня)
        {
            _lastRandomedRightColorContainer = _availableColorContainersList[Random.Range(0, _availableColorContainersList.Count)]; // рандомим правильный цвет
            _availableColorContainersList.Remove(_lastRandomedRightColorContainer); // удаляем цвет из списла доступных цветов

            _lastRandomedWrongColorContainer = _availableColorContainersList[Random.Range(0, _availableColorContainersList.Count)]; // рандомим неправильный цвет
            _availableColorContainersList.Remove(_lastRandomedWrongColorContainer); // удаляем цвет из списла доступных цветов
        }
        else // если это не первая итерация
        {          
            ColorContainer tempCont = _lastRandomedRightColorContainer; // временная переменная, хранящая последний случайно выбранный правильный цвет
            _availableColorContainersList.Remove(_lastRandomedRightColorContainer); // удаляем цвет из списка, чтобы не было двух подряд одинаковых правильных цветов у чекпоинтов
            _lastRandomedRightColorContainer = _availableColorContainersList[Random.Range(0, _availableColorContainersList.Count)]; // рандомим цвет для нового чекпоинта
            _availableColorContainersList.Remove(_lastRandomedRightColorContainer); // удаляем новый рандомный цвет из списка доступных цветов
            _availableColorContainersList.Add(tempCont); // возвращаем в список доступных цветов старый (прошлый) выбранный цвет

            // такая же логика для неправильногго цвета
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
