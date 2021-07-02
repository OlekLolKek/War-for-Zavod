using Abstractions;
using UnityEngine;
using Utils;
using Zenject;


namespace InputSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(ScriptableModelInstaller), menuName = "Installers/" + nameof(ScriptableModelInstaller))]
    public class ScriptableModelInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AssetsStorage _assetsStorage;
        [SerializeField] private GroundClickModel _groundClickModel;
        [SerializeField] private SelectedItemModel _selectedItemModel;
        [SerializeField] private AttackableTargetModel _dummyTarget;
        
        public override void InstallBindings()
        {
            Container.Bind<AssetsStorage>().FromInstance(_assetsStorage).AsSingle();
            Container.Bind<GroundClickModel>().FromInstance(_groundClickModel).AsSingle();
            Container.Bind<SelectedItemModel>().FromInstance(_selectedItemModel).AsSingle();
            
            Container.Bind<IAwaitable<Vector3>>().FromInstance(_groundClickModel).AsSingle();
            Container.Bind<IAwaitable<ISelectableItem>>().FromInstance(_selectedItemModel).AsSingle();
        }
    }
}