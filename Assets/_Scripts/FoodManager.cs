using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public List<GameObject> _unitsList;
    public ColorContainer _colorContainer; 

    private void PartySettings()
    {
        int count = Random.Range(3, 5); // рандомно выбирается число юнитов в группе
        for(int i = 0; i < count; i++)
        {
            int index = Random.Range(0, _unitsList.Count);

            float yAngle = Random.Range(-180, 180); // выбираем рандомный угол поворота юнити
            _unitsList[index].transform.eulerAngles = new Vector3(0, yAngle, 0);

            FoodController _foodController = _unitsList[index].GetComponent<FoodController>();
            
            _foodController.SetColorContainer(_colorContainer);

            _unitsList.RemoveAt(index); // удаляем из списка все выбранные юниты                       
        }

        // удаляем все невыбранные юниты
        for(int i = 0; i < _unitsList.Count; i++)
        {
            _unitsList[i].SetActive(false);
        }
    }

    // метод создания группы с заданным цветом
    public void CreateFoodParty(ColorContainer colorCont)
    {
        _colorContainer = colorCont;
        PartySettings();
    }
}
