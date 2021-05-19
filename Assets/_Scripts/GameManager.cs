using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int _score;
    public int _eatedFood;
    public int _curFeverPoints;
    public int _maxFeverPoints;
    public GameObject _levelFailedPanel;
    public GameObject _levelCompletePanel;

    public SnakeController _snakeController;

    public float comboTimer;
    public float timeBetweenComboPoints;
    public bool _needToUpdateComboTimer;
   
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

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelFailed()
    {
        _snakeController.enabled = false;
        _levelFailedPanel.SetActive(true);      
    }

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

    public void UpdateComboTimer()
    {
        if(_needToUpdateComboTimer)
        {
            comboTimer += Time.deltaTime;

            if(comboTimer > timeBetweenComboPoints)
            {
                _curFeverPoints = _maxFeverPoints;
                _needToUpdateComboTimer = false;
            }
        }

        if (_curFeverPoints == 0)
            _snakeController.TurnOnFever();
    }

    


}
