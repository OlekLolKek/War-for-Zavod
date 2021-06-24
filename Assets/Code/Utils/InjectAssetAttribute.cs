using System;
using JetBrains.Annotations;


namespace Utils
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectAssetAttribute : Attribute
    {
        [UsedImplicitly] public string AssetName;

        public InjectAssetAttribute(string assetName)
        {
            AssetName = assetName;
        }
    }
}