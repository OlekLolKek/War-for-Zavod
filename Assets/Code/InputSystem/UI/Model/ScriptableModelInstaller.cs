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
        
        public override void InstallBindings()
        {
            Container.Bind<AssetsStorage>().FromInstance(_assetsStorage).AsSingle();
            Container.Bind<GroundClickModel>().FromInstance(_groundClickModel).AsSingle();
            Container.Bind<SelectedItemModel>().FromInstance(_selectedItemModel).AsSingle();
        }
    }
}