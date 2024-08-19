using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsScript : MonoBehaviour
{
    [HideInInspector]
    public int successfullMissions = 0;
    private List<Mission> missions = new List<Mission>();
    private float timer;
    private int missionCount = 0;
    private int currentTier = 0;
    private Mission currentMission;

    public AudioClip missionAccomplie;
    public AudioClip missionFailed;
    public AudioClip nouvelleMission;
    public float timeBetweenMissions = 10;
    public float tier0MissionCount = 10;
    public float tier1MissionCount = 20;
    
    public GameObject[] tier0;
    public GameObject[] tier1;
    public GameObject[] tier2;
    
    public class Mission {
        public int tier;
        public GameObject ingredient;
    }

    public SheepScript sheep;
    public GameObject sheepBubble;
    public GameObject indicatorBubble;
    public SpriteRenderer ingredientImage;
    public SpriteRenderer indicatorIngredientImage;
    public Sprite hex;
    public Sprite heart;

    public float bonusLifePercent = 0.25f;
    public float bonusWeightPercent = 0.25f;
    
    void Start() {
        foreach(var tier in tier0) missions.Add(new Mission() {tier = 0, ingredient = tier});
        foreach(var tier in tier1) missions.Add(new Mission() {tier = 1, ingredient = tier});
        foreach(var tier in tier2) missions.Add(new Mission() {tier = 2, ingredient = tier});
        ServiceManager.Instance.Get<OnSheepEat>().Subscribe(HandleEating);
    }
    void OnDestroy() {
        ServiceManager.Instance.Get<OnSheepEat>().Unsubscribe(HandleEating);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMission != null) return;
        timer += Time.deltaTime;
        timer = Mathf.Min(timeBetweenMissions, timer);
        if(timer < timeBetweenMissions) return;

        ChooseMission();
    }

    private void ChooseMission() {
        var availableMissions = missions.FindAll(mission => mission.tier <= currentTier);
        currentMission = availableMissions.GetRandom();
        
        sheepBubble.SetActive(true);
        indicatorBubble.SetActive(true);
        ingredientImage.sprite = currentMission.ingredient.GetComponent<SpriteRenderer>().sprite;
        indicatorIngredientImage.sprite = ingredientImage.sprite;
        AudioSource.PlayClipAtPoint(nouvelleMission, sheep.transform.position);
    }
    private void HandleEating(FoodScript food)
    {
        if(currentMission == null) return;
        if(currentMission.ingredient.GetComponent<FoodScript>().type != food.type) {
            AudioSource.PlayClipAtPoint(missionFailed, sheep.transform.position, 1.5f);
            sheep.GetComponentInChildren<Animator>().Play("MissionFailed");
            ingredientImage.sprite = hex;
            indicatorIngredientImage.sprite = hex;
            var lifeBonus = food.life * bonusLifePercent;
            var weightBonus = food.weight * bonusWeightPercent;
            sheep.currentLife -= lifeBonus;
            sheep.weight -= weightBonus;
        }
        else {
            ingredientImage.sprite = heart;
            indicatorIngredientImage.sprite = heart;
            AudioSource.PlayClipAtPoint(missionAccomplie, sheep.transform.position, 1.5f);
            sheep.GetComponentInChildren<Animator>().Play("MissionSuccess");
            var lifeBonus = food.life * bonusLifePercent;
            var weightBonus = food.weight * bonusWeightPercent;
            sheep.currentLife += lifeBonus;
            sheep.weight += weightBonus;
            successfullMissions++;
        }
        StartCoroutine(EndMission());
    }

    IEnumerator EndMission() {
        yield return new WaitForSeconds(1);

        currentMission = null;
        sheepBubble.SetActive(false);
        indicatorBubble.SetActive(false);
        timer = 0;
        missionCount++;
        if(currentTier == 0  && missionCount >= tier0MissionCount) {
            missionCount = 0;
            currentTier++;
        }
        else if(currentTier == 1 && missionCount >= tier1MissionCount) {
            missionCount = 0;
            currentTier++;
        }
    }
}
