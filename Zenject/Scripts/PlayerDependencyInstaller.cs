using UnityEngine;
using Zenject;

public class PlayerDependencyInstaller : MonoInstaller
{
    [SerializeField] private Transform _playerSpawnPos;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Joystick _movementJoystick;
    public override void InstallBindings()
    {
        BindJoystick();
        BindHero();
    }

    private void BindJoystick()
    {
        Container
            .Bind<Joystick>()
            .FromInstance(_movementJoystick)
            .AsSingle();
    }
    
    private void BindHero()
    {
        PlayerMovement playerMovement = Container.InstantiatePrefabForComponent<PlayerMovement>(_playerPrefab, 
            _playerSpawnPos.position, Quaternion.identity, null);
        
        Container
            .Bind<PlayerMovement>()
            .FromInstance(playerMovement)
            .AsSingle();
    }
}
