using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class CoreLoopSystem : MonoBehaviour
{
    [SerializeField] private Transform levelParent;
    [SerializeField] private AssetReferenceGameObject levelAssetReference;

    private Level createdLevel;
    private PlayTimer playTimer;
    private SaveSystem saveSystem;
    private AssetProvider assetProvider;
    private AdsManager adsManager;

	private bool levelEnded;
	private int currentLevelValue;

	public int CurrentLevelValue => currentLevelValue;

	public Action onWin;
	public Action onLose;
	public Action onLevelCreated;

	[Inject]
	private void Construct(PlayTimer playTimer, SaveSystem saveSystem, AssetProvider assetProvider, AdsManager adsManager)
	{
		this.playTimer = playTimer;
		this.saveSystem = saveSystem;
		this.assetProvider = assetProvider;
		this.adsManager = adsManager;
		LoadData();

		CreateLevel();

		playTimer.onTimerEnd += LevelLose;
	}

	public async void CreateLevel()
	{
        Task<Level> levelAsset = assetProvider.LoadAsset<Level>(levelAssetReference);
		await levelAsset;
        createdLevel = Instantiate(levelAsset.Result, levelParent);
        createdLevel.Init();

		onLevelCreated?.Invoke();

		createdLevel.onAllPointedActivated += LevelComplete;

		levelEnded = false;
		playTimer.ResetTimer();
		playTimer.StartTimer();
    }

	public void DestroyLevel()
	{
        createdLevel.onAllPointedActivated -= LevelComplete;
		Destroy(createdLevel.gameObject);
    }

	public void NextButtonClick()
	{
		currentLevelValue++;
		saveSystem.SavesData.CurrentLevel = currentLevelValue;
		saveSystem.SaveToJson();

		if (adsManager.TryShowInterstitial())
			adsManager.onInterstitialClose += RewardAfterInterstitial;
		else
			CreateNewLevel();


        void RewardAfterInterstitial()
		{
            adsManager.onInterstitialClose -= RewardAfterInterstitial;
            CreateNewLevel();
		}

		void CreateNewLevel()
		{
            DestroyLevel();
            CreateLevel();
        }
	}

	private void LevelComplete()
	{
		if (levelEnded)
			return;

		onWin?.Invoke();
		playTimer.StopTimer();
		levelEnded = true;
	}

	private void LevelLose()
	{
		if (levelEnded)
			return;

        onLose?.Invoke();
        levelEnded = true;
    }

	private void LoadData()
	{
		currentLevelValue = saveSystem.SavesData.CurrentLevel;
	}
}