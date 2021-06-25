using System.Reflection;
using UnityEngine;


namespace Utils
{
    public static class Injector
    {
        public static T Inject<T>(this AssetsStorage storage, T target) where T : class
        {
            var targetType = target.GetType();

            while (targetType != null)
            {
                var fields = targetType.GetFields(
                    BindingFlags.Public
                    | BindingFlags.NonPublic
                    | BindingFlags.Instance
                    | BindingFlags.DeclaredOnly);

                foreach (var field in fields)
                {
                    if (field.GetCustomAttribute(typeof(InjectAssetAttribute)) is InjectAssetAttribute injectAssetAttribute)
                    {
                        var asset = storage.GetAsset(injectAssetAttribute.AssetName);
                        field.SetValue(target, asset);
                    }
                }

                targetType = targetType.BaseType;
            }

            return target;
        }
    }
}