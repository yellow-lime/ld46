using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    enum FlowerStage { SEED, BUD, FLOWER };
    private Dictionary<FlowerStage, int> minsToGrow = new Dictionary<FlowerStage, int>{
        {FlowerStage.SEED, (60 * 24)},
        {FlowerStage.BUD, (60 * 24)},
        {FlowerStage.FLOWER, 0}
    };

    private FlowerStage flowerStage = FlowerStage.SEED;
    private int gameMinutesToNextStage;

    public void updateGameMinutes(int gameMinutes){
        gameMinutesToNextStage -= gameMinutes;
        if(gameMinutesToNextStage <= 0){
            growToNextStage();
        }
    }
    
    void growToNextStage(){
        switch(flowerStage){
            case FlowerStage.SEED:
                flowerStage = FlowerStage.BUD;
                break;
            case FlowerStage.BUD:
                flowerStage = FlowerStage.FLOWER;
                break;
        }
        gameMinutesToNextStage += minsToGrow[flowerStage];
    }
}
