using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsScript : MonoBehaviour
{
    public float timeBetweenMissions = 10;
    private float timer;
    public float tier0MissionCount = 10;
    public float tier1MissionCount = 20;
    
    private List<Mission> missions = new List<Mission>();
    private int missionCount = 0;
    private int currentTier = 0;

    public GameObject[] tier0;
    public GameObject[] tier1;
    public GameObject[] tier2;
    
    public class Mission {
        public int tier;
        public GameObject ingredient;
    }

    private Mission currentMission;
    public SheepScript sheep;
    public GameObject sheepBubble;
    public SpriteRenderer ingredientImage;
    public Sprite heart;

    public float bonusPercent = 0.25f;
    
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
        ingredientImage.sprite = currentMission.ingredient.GetComponent<SpriteRenderer>().sprite;
    }
    private void HandleEating(FoodScript food)
    {
        if(currentMission == null) return;
        if(currentMission.ingredient.GetComponent<FoodScript>().type != food.type) return;

        var lifeBonus = food.life * bonusPercent;
        sheep.currentLife += lifeBonus;
        StartCoroutine(MissionComplete());
    }

    IEnumerator MissionComplete() {
        ingredientImage.sprite = heart;
        yield return new WaitForSeconds(1);

        currentMission = null;
        sheepBubble.SetActive(false);
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
