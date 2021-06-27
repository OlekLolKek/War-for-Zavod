using UnityEngine;
using Utils;
using Zenject;


namespace InputSystem.UI.Model
{
    public class ModelInstaller : MonoInstaller
    {
        [SerializeField] private AssetsStorage _assetsStorage;
        
        public override void InstallBindings()
        {
            Container.Bind<AssetsStorage>().FromInstance(_assetsStorage).AsSingle();
            Container.Bind<ControlButtonPanel>().AsSingle();
        }
    }
}