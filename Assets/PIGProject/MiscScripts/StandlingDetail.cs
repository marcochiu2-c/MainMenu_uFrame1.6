using UnityEngine;
using System.Collections;

public struct StandlingDetail 
{
	public int TotalSoldierQunatity;
	public int TotalGeneral;
	public int TotalPlayer;
	public int DeadSoldiers;
	public int AliveSoldiers;
	public int LoseGear;
	public int SilverFeatherGain;
	public int ResourceGain;
	
	public StandlingDetail (int totalSoldierQunatity, int totalGeneral, int totalPlayer, int deadSoldiers, int aliveSoldiers, int loseGear,int silverFeatherGain, int resourceGain)            
	{
		this.TotalSoldierQunatity = totalSoldierQunatity;
		this.TotalGeneral = totalGeneral;
		this.TotalPlayer = totalPlayer;
		this.DeadSoldiers = deadSoldiers;
		this.AliveSoldiers = aliveSoldiers;
		this.LoseGear = loseGear;
		this.SilverFeatherGain = silverFeatherGain;
		this.ResourceGain = resourceGain;
	}
	
}
