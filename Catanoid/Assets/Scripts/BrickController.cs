using System;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public static event Action AllBricksBroken;
    private List<Brick> _allBricks = new List<Brick>();

    private void Start()
    {
        Brick[] bricks = GetComponentsInChildren<Brick>();

        for (int i = 0; i < bricks.Length; i++)
        {
            _allBricks.Add(bricks[i]);
        }
    }

    private void OnEnable()
    {
        Brick.OnBrickBroken += BrickBroken;
    }

    private void OnDisable()
    {
        Brick.OnBrickBroken -= BrickBroken;
    }

    private void BrickBroken(Brick brick, int score)
    {
        _allBricks.Remove(brick);

        if (_allBricks.Count == 0)
        {
            AllBricksBroken?.Invoke();
        }
    }
}
