using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positioning {



    public static bool AllyNearby(int posX, int posY, int range, int team)
    {

        for(int i = posX - range; i < posX + range; i++)
        {
            for(int j = posY - range; j < posY + range; j++)
            {
                if(!(i < 0 || j < 0 || i > GameMapping.map.sizeX || j > GameMapping.map.sizeY))
                {

                    if(team == GameMapping.map.teamControlled[i, j])
                    {

                        return true;

                    }

                }
            }
        }

        return false;
    }

    public static int SquaresAdvanced(int PosX, int PosY, int origLocX, int origLocY, int enemyPosX, int enemyPosY)
    {

        int dirX = (enemyPosX - PosX) / Mathf.Abs((enemyPosX - PosX));
        int dirY = (enemyPosY - PosY) / Mathf.Abs((enemyPosY - PosY));


        int changeX = (PosX - origLocX) / dirX;
        int changeY = (PosY - origLocY) / dirY;

        return changeX + changeY;
    }

    public static int HealthCheck(int health, int maxHealth, int posX, int posY, int range, int team)
    {

        int hpValue = 0;

        if(health < (maxHealth / 4)){
         
        }
        else
        {

        }

        return hpValue;
    }

}
