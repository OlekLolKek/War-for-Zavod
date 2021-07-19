using Abstractions;
using UnityEngine;
using Utils;
using Zenject;


namespace InputSystem.UI.Model
{
    public class ModelInstaller : MonoInstaller
    {
        [SerializeField] private AttackableTargetModel _dummyTarget;
        
        public override void InstallBindings()
        {
            Container.Bind<ControlButtonPanel>().AsSingle();
            Container.Bind<ProductionPanel>().AsSingle();

            Container.Bind<CommandCreator<IProduceUnitCommand>>().To<ProduceUnitCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<IMoveCommand>>().To<MoveCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<IAttackCommand>>().To<AttackCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<IPatrolCommand>>().To<PatrolCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<IStopCommand>>().To<StopCommandCreator>().AsSingle();

            Container.BindFactory<IProduceUnitCommand, CommandFactory<IProduceUnitCommand>>().To<ProduceUnitCommand>();
            
            Container.Bind<int>().WithId("TestUnitProductionTime").FromInstance(3);
            Container.Bind<string>().WithId("TestUnitName").FromInstance("Test Unit");
            
            _dummyTarget.SetValue(GameObject.FindWithTag("DummyTarget").GetComponent<IAttackable>());
            Container.Bind<IAwaitable<IAttackable>>().FromInstance(_dummyTarget).AsSingle();
        }
    }
}