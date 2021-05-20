using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Transform _snakeHeadTransform;
    public Transform _snakeHeadEndPoint;

    public Transform _snakePartPrefab;

    public float _snakePartSizeZ;

    public List<Transform> _snakePartsList = new List<Transform>();
    public List<Vector3> _positionsList = new List<Vector3>();
    public List<Transform> _endPointsList;

    public int _snakePartCount;

    void Start()
    {
        _positionsList.Add(_snakeHeadTransform.position);
        for (int i = 0; i < _snakePartCount; i++)
        {
            AddSnakePart();
        }
    }

    void Update()
    {
        float distance = ((Vector3)_snakeHeadTransform.position - _positionsList[0]).magnitude; // ��������� �� �������� ��������� ������ �� ����������� �������

        if (distance > _snakePartSizeZ) // ���� ��������� ����� ������, ��� ������ ����� ����� ���� (������)
        {
            Vector3 direction = ((Vector3)_snakeHeadTransform.position - _positionsList[0]).normalized; // ���������� ����������� �� ������� ��������� ������ �� ������

            _positionsList.Insert(0, _positionsList[0] + direction * _snakePartSizeZ); // ��������� ����� ������� ������ � ������ ������ �������
            _positionsList.RemoveAt(_positionsList.Count - 1); // ������� ��������� ������� ������

            distance -= _snakePartSizeZ; // ����� ��������� ����� ��� ������������ ������� ����� ����
        }

        for(int i = 0; i < _snakePartsList.Count; i++)
        {
            // ������ ����������� ������� ������� ����� ���� � ������� ����� ����, ������ �� 1 ������� ����� � ������ ����
            _snakePartsList[i].position = Vector3.Lerp(_positionsList[i + 1], _positionsList[i], distance / _snakePartSizeZ);

            // ������ ����� ���� ���� ������� �� ����������
            if (i == 0) // ���� ����� ������ � ������, ��� ������� �� ����� ������� ������
                _snakePartsList[i].LookAt(_snakeHeadEndPoint); 
            else
                _snakePartsList[i].LookAt(_endPointsList[i-1]);
        }
    }

    // ����� ���������� ����� ����
    public void AddSnakePart()
    {
        Vector3 spawnPosition = new Vector3(_positionsList[_positionsList.Count - 1].x, _positionsList[_positionsList.Count - 1].y, _positionsList[_positionsList.Count - 1].z - _snakePartSizeZ);
        Transform newPart = Instantiate(_snakePartPrefab, spawnPosition, Quaternion.identity, transform);
        _snakePartsList.Add(newPart);
        _positionsList.Add(newPart.position);
        _endPointsList.Add(newPart.GetChild(0));
        newPart.GetComponent<MeshRenderer>().material = _snakeHeadTransform.GetComponent<MeshRenderer>().material;       
    }
}
