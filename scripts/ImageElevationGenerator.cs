using Godot;
public class ImageElevationGenerator : ElevationGenerator {
	private Image map;
	public ImageElevationGenerator (Image map, Weltschmerz weltschmerz, Config config) : base (weltschmerz, config) {
		this.map = map;
		map.Lock ();
	}

	public override double GetElevation (int posX, int posY) {
		return (map.GetPixel (posX, posY).r8 / 255f) * config.elevation.max_elevation;
	}

	public override bool IsLand(double elevation){
		return elevation > config.elevation.water_level;
	}

	public override void ChangeConfig(Config config){}

	public override void Update(){}
}
