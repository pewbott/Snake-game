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

        for(int i = 0; i < _foodPartysList.Count/2; i++)
        {
            bool leftPartyTrueFlag = RandomBool();        

            if (leftPartyTrueFlag)
            {
                _foodManagersList[i].SetColorContainer(_checkpointController._rightColorContainer);
                _foodManagersList[i + _foodManagersList.Count / 2].SetColorContainer(_checkpointController._wrongColorContainer);
            }
            else
            {
                _foodManagersList[i].SetColorContainer(_checkpointController._wrongColorContainer);
                _foodManagersList[i + _foodManagersList.Count / 2].SetColorContainer(_checkpointController._rightColorContainer);
            }
        }

        for(int i = 0; i < _partysRandomCount; i++)
        {
            GameObject randomedParty = _foodPartysList[Random.Range(0, _foodPartysList.Count)];
            _foodPartysList.Remove(randomedParty);
        }

        for (int i = 0; i < _foodPartysList.Count; i++) 
        {
            //   Destroy(_foodPartysList[i]);
            _foodPartysList[i].SetActive(false);    
        }
    }

    bool RandomBool()
    {
        return (Random.value > 0.5f);
    }
}
