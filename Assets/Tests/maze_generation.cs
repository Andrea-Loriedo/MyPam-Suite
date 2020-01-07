using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class maze_generation {

    [Test]
    public void next_maze_is_never_the_same_as_previous()
    {
        // ARRANGE
        // TilemapGenerator maze = new TilemapGenerator();

        // ACT
        Dictionary<string,object> maps = TilemapGenerator.LoadTilemaps();
        int n = maps.Count;
        int previousMaze = int.Parse(TilemapGenerator.Shuffle(n));

        // ASSERT
        for (int i = 0; i < n-1; i++) 
        {
            int currentMaze = int.Parse(TilemapGenerator.Shuffle(n));
            if (i != 0)
                Assert.AreNotEqual(currentMaze, previousMaze);
            previousMaze = currentMaze;
        }
    }

    [Test]
    public void mazes_are_cycled_through_without_repetition()
    {
        // ARRANGE
        // TilemapGenerator maze = new TilemapGenerator();

        // ACT
        Dictionary<string,object> maps = TilemapGenerator.LoadTilemaps();
        int n = maps.Count;
        List<int> usedMaps = new List<int>();

        // ASSERT
        for (int i = 0; i < n-1; i++) 
        {
            int currentMaze = int.Parse(TilemapGenerator.Shuffle(n));
            Assert.IsFalse(usedMaps.Contains(currentMaze));
            usedMaps.Add(currentMaze);
        }
    }
}
