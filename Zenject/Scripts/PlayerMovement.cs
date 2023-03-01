using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRB;
    [SerializeField] private float _moveSpeed; // Prefer set this settings in Scriptable object
    
    private Vector3 _moveDir;
    private Joystick _joystick;

    [Inject]
    private void Construct(Joystick joystick)
    {
        _joystick = joystick;
    }

    private void Update()
    {
        _moveDir.x = _joystick.Horizontal;
        _moveDir.y = _joystick.Vertical;
    }
    
    private void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(_moveDir.x, 0, _moveDir.y);
        Vector3 movePosition = _playerRB.position + moveVector * _moveSpeed;
        _playerRB.MovePosition(movePosition);
        _playerRB.transform.forward = moveVector;
    }
}
