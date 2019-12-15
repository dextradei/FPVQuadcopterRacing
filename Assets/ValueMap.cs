[System.Serializable]
public class ValueMap {

	public float min, max, toMin, toMax;
	
	public ValueMap(float min, float max, float toMin, float toMax) {
		this.min = min;
		this.max = max;
		this.toMin = toMin;
		this.toMax = toMax;
	}

	public float Map(float value) {
		float range = max - min;
		float toRange = toMax - toMin;
		float percent = (value - min) / range;
		float newValue = (toRange * percent) + toMin;
		return newValue;
	}
}
