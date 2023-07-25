using UnityEngine;
using System.Linq;

public class MapGenerator : MonoBehaviour {
    
    public enum DrawMode {NoiseMap, ColourMap};
    public DrawMode drawMode;
    public int MapWith;
    public int mapHieght;
    public float Sacale;
    public int octives;
    [Range(0 , 1)]
    public float pernalenoise;
    public float lacunarity;
    public bool update;
    public Vector2 offset;
    public int seed;
    public TrainnType[] regions;
    [Range(0 , 1)]
    public float closenes;
    public void GenerateMap()
    {
        var noisemap = Noise.GenerateNoiseMap(MapWith , mapHieght, seed , Sacale , octives , pernalenoise , lacunarity , offset);

        MapDisplay mapDisplay = FindAnyObjectByType<MapDisplay>();

        Color[] colourMap = new Color[MapWith * mapHieght];
		for (int y = 0; y < mapHieght; y++) {
			for (int x = 0; x < MapWith; x++) {
				float currentHeight = noisemap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight <= regions [i].height) {
                        Color color = regions [i].coloir;


                        if ((regions.Count() > i+1))
                        {
                            var colorwow = regions[i+1].coloir;

                                var starte = (regions[i].height + ((regions[i+1].height - regions[i].height)* closenes));
                                var owo = regions[i+1].height - starte;
                                var currentpos = regions[i+1].height - currentHeight;
                                var wopw = owo - currentpos;
                                var yes = wopw/owo;
                                
                                    

                                color = Color.Lerp(color , colorwow , yes);
                                
                            

                                
                            
                            
                        }







						colourMap [y * MapWith + x] = color;
						break;
					}
				}
			}
		}

        MapDisplay display = FindObjectOfType<MapDisplay> ();
		if (drawMode == DrawMode.NoiseMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap(noisemap));
		} else if (drawMode == DrawMode.ColourMap) {
			display.DrawTexture (TextureGenerator.TextureFromColourMap(colourMap , mapHieght , mapHieght));
		}
    }


}
[System.Serializable]
public struct TrainnType
{
    public string names;
    public float height;
    public Color coloir;
}