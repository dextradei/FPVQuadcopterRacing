[System.Serializable]
public class PID {
	public float pFactor, iFactor, dFactor;
		
	float integral;
	float lastError;
	
	
	public PID(float pFactor, float iFactor, float dFactor) {
		this.pFactor = pFactor;
		this.iFactor = iFactor;
		this.dFactor = dFactor;
	}
	
	
	public float Update(float error, float timeFrame) {
		integral += error * timeFrame;
		float deriv = (error - lastError) / timeFrame;
		lastError = error;
		return error * pFactor + integral * iFactor + deriv * dFactor;
	}
}
