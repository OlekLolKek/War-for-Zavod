using DefaultNamespace.CommandExecutors;
using Zenject;


namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<CoinModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProduceUnitCommandExecutor>().FromComponentsInHierarchy().AsSingle();
        }
    }
}