using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int _score; // очки за кристаллы
    public int _eatedFood; // число съеденной еды   
    public int _maxFeverPoints; // сколько необходимо собрать кристаллов по правилам, чтобы активировался февер
    public int _curFeverPoints;
    public GameObject _levelFailedPanel;
    public GameObject _levelCompletePanel;

    public SnakeController _snakeController;

    public float comboTimer; // текущее время для срабатывания комбо 
    public float timeBetweenComboPoints; // время, за которое нужно подобрать следующий комбо-поинт (кристалл)
    public bool _needToUpdateComboTimer; // флаг, срабатывающий при поднятии кристалла. После срабатыватия, время для подбирания нового комбо-поинта (кристалла) обновляется

    void Start()
    {
        _snakeController = GameObject.Find("Snake").GetComponent<SnakeController>();
        _levelFailedPanel.SetActive(false);
        _levelCompletePanel.SetActive(false);
        _maxFeverPoints = 3;
        _curFeverPoints = _maxFeverPoints;
        comboTimer = 0;
    }
  
    void Update()
    {
        UpdateComboTimer();
    }

    // метод перезапуска уровня
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // метод, вызываемый при проигрыше
    public void LevelFailed()
    {
        _snakeController.enabled = false;
        _levelFailedPanel.SetActive(true);      
    }

    // метод, вызываемый при успешном прохождении уровня
    public void LevelComplete()
    {
        _snakeController.enabled = false;
        _levelCompletePanel.SetActive(true);
    }

    public void GetCrystal()
    {
        _score += 10;
        
        _needToUpdateComboTimer = true;
        comboTimer = 0;
        _curFeverPoints--;
    }

    public void GetFood()
    {
        _eatedFood++;
    }

    // метод отслеживающий состояние комбо
    public void UpdateComboTimer()
    {
        if(_needToUpdateComboTimer) // если игрок подобрал кристал и сработал флаг
        {
            comboTimer += Time.deltaTime;

            if(comboTimer > timeBetweenComboPoints) // если игрок не успел собрать следующий поинт до конца времени
            {
                _curFeverPoints = _maxFeverPoints;
                _needToUpdateComboTimer = false;
            }
        }

        if (_curFeverPoints == 0) // если игрок успел собрать все поинты
            _snakeController.TurnOnFever();
    }

    


}
