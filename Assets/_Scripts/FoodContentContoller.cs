using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodContentContoller : MonoBehaviour
{
    public List<GameObject> _foodPartysList;
    public List<FoodManager> _foodManagersList;
    public int _partysRandomCount;

    public CheckpointController _checkpointController;

    public void CreateFoodContent()
    {
        _partysRandomCount = Random.Range(_foodPartysList.Count - 2, _foodPartysList.Count); // выбираем рандомное число групп, которое будет на участке

        for (int i = 0; i < _foodPartysList.Count; i++)
        {
            _foodManagersList.Add(_foodPartysList[i].GetComponent<FoodManager>());
        }

        // для каждой пары групп рандомно задаем цвет (правильный или неправильный)
        for (int i = 0; i < _foodPartysList.Count/2; i++)
        {
            bool leftPartyTrueFlag = RandomBool();        

            if (leftPartyTrueFlag)
            {
                _foodManagersList[i].CreateFoodParty(_checkpointController._rightColorContainer);
                _foodManagersList[i + _foodManagersList.Count / 2].CreateFoodParty(_checkpointController._wrongColorContainer);
            }
            else
            {
                _foodManagersList[i].CreateFoodParty(_checkpointController._wrongColorContainer);
                _foodManagersList[i + _foodManagersList.Count / 2].CreateFoodParty(_checkpointController._rightColorContainer);
            }
        }

        // рандомим из общего списка группы, которые будут заспавнены и удаляем их из списка
        for (int i = 0; i < _partysRandomCount; i++)
        {
            GameObject randomedParty = _foodPartysList[Random.Range(0, _foodPartysList.Count)];
            _foodPartysList.Remove(randomedParty);
        }

        // скрываем все объекты, которые не были рандомно выбраны (оставшиеся в списке)
        for (int i = 0; i < _foodPartysList.Count; i++) 
        {
            //   Destroy(_foodPartysList[i]);
            _foodPartysList[i].SetActive(false);    
        }
    }

    // метод получения рандомного булевого значения
    bool RandomBool()
    {
        return (Random.value > 0.5f);
    }
}
