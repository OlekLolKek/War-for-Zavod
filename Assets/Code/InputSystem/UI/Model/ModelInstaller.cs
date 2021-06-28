using Abstractions;
using UnityEngine;
using Utils;
using Zenject;


namespace InputSystem.UI.Model
{
    public class ModelInstaller : MonoInstaller
    {
        [SerializeField] private AssetsStorage _assetsStorage;
        [SerializeField] private GroundClickModel _groundClickModel;
        
        public override void InstallBindings()
        {
            Container.Bind<AssetsStorage>().FromInstance(_assetsStorage).AsSingle();
            Container.Bind<GroundClickModel>().FromInstance(_groundClickModel).AsSingle();
            Container.Bind<ControlButtonPanel>().AsSingle();
            
            Container.Bind<CommandCreator<IProduceUnitCommand>>().To<ProduceUnitCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<IMoveCommand>>().To<MoveCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<IAttackCommand>>().To<AttackCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<IPatrolCommand>>().To<PatrolCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<IStopCommand>>().To<StopCommandCreator>().AsSingle();
        }
    }
}