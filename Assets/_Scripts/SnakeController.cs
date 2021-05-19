using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public Transform _snakeTransform;
    public Transform _snakeHeadTransform;
    public float _moveSpeed = 5f;
    public float _xSpeed = 5f;
    private Camera _camera;

    public float _rotationSpeed = 50f;

    public float _angleMultiplier;

    public ColorContainer _curColorContainer;

    public bool _feverMod;
    public float _feverTimer;
    public float _feverDuraction = 3f;
    public GameObject _feverColliderObject;

    public GameManager _gameManager;

    private void Awake()
    {
        _snakeTransform = GetComponent<Transform>();
        _camera = Camera.main;
        _feverMod = false;
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {

        if (!_feverMod)
        {
            _snakeTransform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime, Space.World);
            SnakesHeadMoveController();
        }
        else
        {
            _snakeTransform.Translate(Vector3.forward * _moveSpeed * 3 * Time.deltaTime, Space.World);
            FeverMoveController();
        }

        FeverBehaviour();
    }

    private void SnakesHeadMoveController()
    {
        if (Input.touchCount > 0)
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                float x = hit.point.x;
                Vector3 targetPosition = new Vector3(x, _snakeHeadTransform.position.y, _snakeHeadTransform.position.z);
                _snakeHeadTransform.position = Vector3.MoveTowards(_snakeHeadTransform.position, targetPosition, _xSpeed * Time.deltaTime);


                Vector3 movementDirection = targetPosition - _snakeHeadTransform.position;

                if (movementDirection != Vector3.zero)
                {
                    _snakeHeadTransform.rotation = Quaternion.Slerp(_snakeHeadTransform.rotation, Quaternion.LookRotation(movementDirection), Time.deltaTime * _rotationSpeed);
                }
                else
                {
                    _snakeHeadTransform.rotation = Quaternion.Slerp(_snakeHeadTransform.rotation, Quaternion.LookRotation(Vector3.forward), Time.deltaTime * _rotationSpeed * _angleMultiplier);
                }
            }
        }
        else
        {
            _snakeHeadTransform.rotation = Quaternion.Slerp(_snakeHeadTransform.rotation, Quaternion.LookRotation(Vector3.forward), Time.deltaTime * _rotationSpeed * _angleMultiplier);
        }
    }

    public void FeverMoveController()
    {
        Vector3 targetPosition = new Vector3(0, _snakeHeadTransform.position.y, _snakeHeadTransform.position.z);
        _snakeHeadTransform.position = Vector3.MoveTowards(_snakeHeadTransform.position, targetPosition, _xSpeed * 3 * Time.deltaTime);
        Vector3 movementDirection = targetPosition - _snakeHeadTransform.position;
        if (movementDirection != Vector3.zero)
        {
            _snakeHeadTransform.rotation = Quaternion.Slerp(_snakeHeadTransform.rotation, Quaternion.LookRotation(movementDirection), Time.deltaTime * _rotationSpeed);
        }
        else
        {
            _snakeHeadTransform.rotation = Quaternion.Slerp(_snakeHeadTransform.rotation, Quaternion.LookRotation(Vector3.forward), Time.deltaTime * _rotationSpeed * _angleMultiplier);
        }
    }

    public void SetMaterial(ColorContainer _colorCont)
    {
        _curColorContainer = _colorCont;
        _snakeHeadTransform.GetComponent<MeshRenderer>().material = _curColorContainer.unitsMaterial;
    }

    public void FeverBehaviour()
    {
        if (_feverMod)
        {
            _feverColliderObject.SetActive(true);
            _feverTimer += Time.deltaTime;

            if(_feverTimer >= _feverDuraction)
            {
                TurnOffFeverMod();
            }
        }
        else
        {
            _feverColliderObject.SetActive(false);
        }    
    }

    public void TurnOffFeverMod()
    {
        _gameManager._score = 0;
        _feverTimer = 0;
        _feverMod = false;
        _gameManager._curFeverPoints = _gameManager._maxFeverPoints;
    }

    public void TurnOnFever()
    {
        _feverMod = true;
    }


}
