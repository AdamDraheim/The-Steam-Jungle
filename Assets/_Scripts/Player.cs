using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player{

    public Unit[] unitList = new Unit[10];

    private Unit selectedUnit;

    public int spec; //0 if human player, 1 if AI player

    public void AddUnit(Unit unit)
    {
        for (int i = 0; i < unitList.Length; i++)
        {
            if (unitList[i] == null)
            {
                unitList[i] = unit;
                break;
            }
        }
    }

    public int GetSpec()
    {
        return spec;
    }

    public Unit[] GetUnitList()
    {
        return unitList;
    }

    public abstract void performAction();

    public void SelectedUnit(Unit unit)
    {
        selectedUnit = unit;
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

}
