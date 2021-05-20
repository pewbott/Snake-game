using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    public GameManager _gameManager;

    public Text scoreText;
    public Text eatedFoodCounterText;

    public float tempCrystalValue;
    public float _updateValueTimer;
    public float _deltaAmount;

    public GameObject _feverPanel;
    public Text _feverComboText;
    public GameObject[] _pointImagesArray;


    private void Awake()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        tempCrystalValue = _gameManager._score;
    }

    void Update()
    {
        CountersUpdater();
    }

    // метод обновления UI 
    public void CountersUpdater()
    {
        eatedFoodCounterText.text = "" + _gameManager._eatedFood; // Обновления текста числа съеденный еды

        // плавное увеличение и обновление текста очков
        if (tempCrystalValue < _gameManager._score)
        {
            _updateValueTimer += Time.deltaTime;

            if(_updateValueTimer > _deltaAmount)
            {
                tempCrystalValue++;
                _updateValueTimer = 0;
            }
        }
        if (tempCrystalValue > _gameManager._score)
            tempCrystalValue = 0;

        scoreText.text = "" + (int)tempCrystalValue;

        // обновление и поведение панели с комбо для февера

        if (_gameManager._curFeverPoints < _gameManager._maxFeverPoints)
        {
            _feverPanel.SetActive(true);
            int combopoint = _gameManager._maxFeverPoints - _gameManager._curFeverPoints;
            _feverComboText.text = "FEVER COMBO X " + combopoint;
            for(int i = 1; i < _gameManager._maxFeverPoints + 1; i++)
            {
                if (_gameManager._maxFeverPoints - _gameManager._curFeverPoints == i)
                    _pointImagesArray[i-1].SetActive(true);
            }        
        }
        else
        {
            _feverPanel.SetActive(false);
            for(int i = 0; i < _pointImagesArray.Length; i++)
            {
                _pointImagesArray[i].SetActive(false);
            }
        }
    }
}
