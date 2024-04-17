using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetProvider
{
    private readonly Dictionary<string, AsyncOperationHandle> cachedAssets = new();

    public async Task<GameObject> LoadGameObject(AssetReferenceGameObject assetReference)
    {
        if (assetReference.RuntimeKeyIsValid() == false)
        {
            Debug.LogError(new Exception("AssetReference is null"));

            return null;
        }

        string assetID = assetReference.AssetGUID;

        if (cachedAssets.ContainsKey(assetID))
            return (GameObject)cachedAssets[assetID].Result;

        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(assetReference);
        await handle.Task;
        cachedAssets.Add(assetID, handle);
        return handle.Result;
    }

    public async Task<T> LoadAsset<T>(AssetReferenceGameObject assetReference) where T : Component
    {
        GameObject gameObject = await LoadGameObject(assetReference);

        if (gameObject.TryGetComponent(out T component))
            return component;

        Debug.LogError($"AssetReference: '{assetReference}' has no required component: " + $"'{typeof(T)}' attached");

        return null;
    }

    public void ClearCache()
    {
        foreach (AsyncOperationHandle handle in cachedAssets.Select(idHandlePair => idHandlePair.Value))
            Addressables.Release(handle);

        cachedAssets.Clear();
    }
}
