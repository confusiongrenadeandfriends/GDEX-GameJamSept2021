using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level : MonoBehaviour
{

    private float TILE_WIDTH = 2.0f;
    private Vector3 originalPos = new Vector3(-5, 5, 0);

    // Start is called before the first frame update
    void Start()
    {
        this.initializeLevel("1-1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initializeLevel(string levelNum)
    {
        var curTile = originalPos;
        using (var reader = new StreamReader(@"Assets/Levels/" + levelNum + ".csv"))
        {
            while (!reader.EndOfStream)
            {
                var row = reader.ReadLine();
                var elements = row.Split(',');

                for (int i = 0; i < elements.Length; i++)
                {
                    if (dict.ContainsKey(elements[i]))
                    {
                        var gameEntity = dict[elements[i]];
                        gameEntity.transform.position = curTile;
                    }
                    curTile.x += TILE_WIDTH;

                }

                curTile.x = originalPos.x;
                curTile.y -= TILE_WIDTH;
            }
        }
    }

    private Dictionary<string, GameObject> dict = new Dictionary<string, GameObject>()
{
    {"q", GameObject.CreatePrimitive(PrimitiveType.Quad)},
    {"r", GameObject.CreatePrimitive(PrimitiveType.Sphere)},
};

}