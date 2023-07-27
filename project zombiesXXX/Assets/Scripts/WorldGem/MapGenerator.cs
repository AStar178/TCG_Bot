using UnityEngine;
using System.Linq;

public class MapGenerator : MonoBehaviour {
    
    public enum DrawMode {NoiseMap, ColourMap , Mesh};
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
    public float height;
    float[,] falloffmap;
    public AnimationCurve meshheightcurve;
    private void Awake() {
        
        falloffmap = FalloffGenerator.GenerateFalloffMap((int)MapWith);
        gameObject.AddComponent<MeshCollider>();
        GenerateMap();
    }
    public void GenerateMap()
    {
        var noisemap = Noise.GenerateNoiseMap(MapWith , mapHieght, seed , Sacale , octives , pernalenoise , lacunarity , offset);
        falloffmap = FalloffGenerator.GenerateFalloffMap((int)MapWith);
        MapDisplay mapDisplay = FindAnyObjectByType<MapDisplay>();

        Color[] colourMap = new Color[MapWith * mapHieght];
		for (int y = 0; y < mapHieght; y++) {
			for (int x = 0; x < MapWith; x++) {
                
                noisemap[x,y] = Mathf.Clamp01( noisemap[x,y] - falloffmap[x , y] );
				float currentHeight = noisemap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight <= regions [i].height) {
                        Color color = regions [i].coloir;
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
		} else if (drawMode == DrawMode.Mesh) {
			display.DrawMesh (MeshGenerator.GenerateTerrainMesh(noisemap , height , meshheightcurve) , TextureGenerator.TextureFromColourMap(colourMap , mapHieght , mapHieght));
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