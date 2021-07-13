using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace Utils
{
    [CreateAssetMenu(fileName = nameof(AssetsStorage), menuName = "Strategy/" + nameof(AssetsStorage))]
    public class AssetsStorage : ScriptableObject
    {
        [SerializeField] private List<GameObject> _assets;

        public GameObject GetAsset(string assetName)
        {
            return _assets.FirstOrDefault(asset => asset.name == assetName);
        }
    }
}