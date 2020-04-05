﻿using System.Collections.Generic;
using UnityEngine;

public class StampPapersTask : Task
{
    public List<Paper> Papers { get; set; }

    public override string Name { get; protected set; } = "Stamp Papers";

    public override bool IsComplete()
    {
        Debug.Log("Complete?");
        if (Papers == null) return false;
        foreach (var paper in Papers)
        {
            if (paper.Contents.Count == 0)
            {
                return false;
            }
        }
        return true;
    }

    public override void OnStart()
    {
        base.OnStart();
        SoundManager.PlayEffect("Knock");
    }
    public override void OnFinished()
    {
        base.OnFinished();
        foreach (var paper in Papers.ToArray()) Object.Destroy(paper.gameObject);
        Papers = null;
    }

    public override void OnUpdate()
    {
        if (_minutesSinceTaskStart > 3 && Papers == null)
        {
            // boss comes in
            // he's like stamp these papers
            // you're like oh shit gotta stamp these papers
            Papers = new List<Paper>();
            int count = Random.Range(1, 11);
            for (int i = 0; i < count; i++)
            {
                // TODO: This is bad.
                Papers.Add(GameObject.Instantiate(
                    Resources.Load<GameObject>("Prefabs/Paper"),
                    GameObject.Find("StuffOnDesk").transform).GetComponent<Paper>());
            }
        }
    }
}