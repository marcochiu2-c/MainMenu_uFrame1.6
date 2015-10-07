using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using uFrame.MVVM;
using uFrame.MVVM.Bindings;
using uFrame.Serialization;
using UnityEngine;
using UniRx;


public partial class EntityViewModel : EntityViewModelBase {
	public float actualHit;
	public float hitRate;
	public float otherHit;
	public float physiqueCapacity;
	public float otherHitCover;
	public float otherHitNoCover;
	public float criticalHit;
	public float criticalHitCover;
	public float criticalHitNoCover;
	public float fatalHit;
	public float fatalHitCover;
	public float fatalHitNoCover;
	public float pureHurt;
	public float hurtByArmorBroken;
	public float otherHitCoverNoHurt;
	public float otherHitCoverHurt;
	public float otherHitNoCoverNoHurt;
	public float otherHitNoCoverHurt;
	public float criticalCoverNoHurt;
	public float criticalCoverHurt;
	public float criticalNoCoverHurt;
	public float fatalCoverNoHurt;
	public float noHurt;
	public float hurt;
	public float dead;
	public float blockage;
	public float blockageNoHurt;
	public float blockageHurt;
	public float missHit;
	public float dodgeToHit;
	public float moraleStandard;
	public object thisLock;
	public System.Timers.Timer Timer;
	public Weapons Weapon = new Weapons {Weight = 2,OtherHit = 50, CriticalHit = 30, FatalHit = 20, Sharpness = 90, IsSharp = true};
	public Armors Armor = new Armors {Weight = 2, OtherCover = 30, CriticalCover = 30, FatalCover = 30, Hardness =30};
	public Shields Shield = new Shields{Weight = 0 , BlockRate = 0, Hardness =0};
	public Formations Formation = new Formations {HitPoint = 10, Dodge = 10, Morale =5};



	public int getGCDOfAttackSpeed(){
		int Remainder;
		int a = AttackSpeed;
		int b = Opponent.AttackSpeed;
		Debug.Log ("Attack Speed of A: " + a + " Attack Speed of B: " + b);
		
		while( b != 0 )
		{
			Remainder = a % b;
			a = b;
			b = Remainder;
			//Debug.Log ("Remainder: "+Remainder);
		}
		
		return a;
	}

	public float WeightDeduct (){
		//int w = Weapon.Weight + Armor.Weight + Shield.Weight;
		int w = 10;
		if (w > Physique) {
			Debug.LogError (Name + ", Physique: " + Physique + " cannot be larger than the sum of the weight of all equipment." + w);
			//if (DEBUG) w = (float) Physique;
		}
		return 1 - w / Physique;
	}
	
	public float ActualHit(){
		const float MIN = 0.01f;
		const float MAX = 0.99f;
		//Debug.Log (Name+"ActualMorale(): " + ActualMorale ());
		//if (this.DEBUG) Debug.Log (Name+" Formation.HitPoint: " + Formation.HitPoint);
		float ah = HitPoint * WeightDeduct () * WeaponProficiency * (ActualMorale () / moraleStandard) / 10000f;
		ah += ah * Formation.HitPoint / 100;
		ah = ah > MAX ? MAX : ah;
		ah = ah < MIN ? MIN : ah;
		return ah;
		//return HitPoint * WeightDeduct() * WeaponProficiency /10000;	
	}
	
	public float ActualDodge(){
		const float MIN = 0.01f;
		const float MAX = 0.99f;
		float ad = 0;
		if (this.DEBUG) 
			Debug.Log (Name + " ActualMorale(): " + ActualMorale ()+"%");
		ad = Dodge * WeightDeduct () * (ActualMorale () / moraleStandard) * (100 + Formation.Dodge)/10000f;
		ad = ad > MAX ? MAX : ad;
		ad = ad < MIN ? MIN : ad;
		
		return ad;
	}
	
	public float ActualMorale(){  
		if (this.DEBUG) {
			Debug.Log (Name + " InitialMorale: " + InitialMorale);
			//Debug.Log (Name + " AdvisorPrestige - oppo.AdvisorPrestige: " + AdvisorPrestige + "  "+ Opponent.AdvisorPrestige);
			//Debug.Log (Name + " GeneralPrestige - oppo.GeneralPrestige: " + GeneralPrestige  + "  "+ Opponent.GeneralPrestige);
		}
		return InitialMorale * (200f + (Prestige - Opponent.Prestige)) * (100f + Formation.Morale)/20000f;
	}

	public float Hit (){ 
		//if (SoldiersDEBUG) Debug.Log (Name + " Opponent.ActualDodge(): " + (Opponent.ActualDodge()*100)+"%");
		return actualHit * (1 - Opponent.ActualDodge () - (Opponent.Shield.BlockRate/100f));
		//return (100 - blockage - missHit - dodgeToHit)/100f;
	}
	
	public float Blockage (){
		return ActualHit()*(1-Opponent.ActualDodge())*Opponent.Shield.BlockRate;
	}

	

	public void GetHealthProbabilities(){
		blockage = Blockage ();
		actualHit = ActualHit ();
		missHit = (1 - actualHit) * 100f;
		dodgeToHit = actualHit * Opponent.ActualDodge () * 100f;
		//Debug.Log (Name + " Counter: " + Counter);
		//while (Counter!=Opponent.Counter) {
		//	Thread.Sleep(1);
		//}
		int equipmentApplication = Weapon.IsSharp ? 100:30;
		
		hitRate = Hit ();
		otherHit = hitRate * Weapon.OtherHit;
		physiqueCapacity = Weapon.IsSharp ? 50:100;
		otherHitCover = otherHit * Opponent.Armor.OtherCover / 100f;
		otherHitNoCover = otherHit - otherHitCover; 
		criticalHit = hitRate * Weapon.CriticalHit;
		criticalHitCover = criticalHit * Opponent.Armor.CriticalCover / 100f;
		criticalHitNoCover = criticalHit - criticalHitCover;
		fatalHit = hitRate * Weapon.FatalHit;
		fatalHitCover = fatalHit * Opponent.Armor.FatalCover / 100f;
		fatalHitNoCover = fatalHit - fatalHitCover;
		pureHurt = Weapon.Sharpness;
		hurtByArmorBroken = pureHurt  - Opponent.Armor.Hardness  * equipmentApplication / 100f;
		otherHitCoverNoHurt = (hurtByArmorBroken / Opponent.Physique *100) > physiqueCapacity ? 0 : otherHitCover;
		otherHitCoverHurt = (hurtByArmorBroken / Opponent.Physique *100) > physiqueCapacity ? otherHitCover : 0;
		otherHitNoCoverNoHurt = (pureHurt / Opponent.Physique *100) > physiqueCapacity ? 0 : otherHitNoCover;
		otherHitNoCoverHurt = (pureHurt / Opponent.Physique *100) > physiqueCapacity ? otherHitNoCover : 0;
		criticalCoverNoHurt = (hurtByArmorBroken > 0) ? 0 : criticalHitCover;
		criticalCoverHurt = (hurtByArmorBroken > 0) ? criticalHitCover : 0;
		criticalNoCoverHurt = criticalHitNoCover;
		fatalCoverNoHurt = (hurtByArmorBroken > 0) ? 0 : fatalHitCover;
		#if COMMENT
		// Will group follwing 2 equations to below and assign to dead
		float fatalCoverDead = (hurtByArmorBroken > 0) ? 0 : fatalHitCover;
		float fatalNoCoverDead = fatalHitNoCover;
		#endif
		blockageNoHurt = (((Weapon.Sharpness / 100f - Opponent.Shield.Hardness * equipmentApplication / 100f) - Opponent.Armor.Hardness / 100f * equipmentApplication / 100f) > 0) ? 0 : blockage;
		blockageHurt = (blockageNoHurt == 0) ? blockage : 0;
		
		
		#if DEBUG
		Debug.Log (Name + " blockageNoHurt: " + blockageNoHurt);
		Debug.Log (Name + " blockageHurt: " + blockageHurt);
		Debug.Log (Name + " missHit: " + missHit);
		Debug.Log (Name + " dodgeToHit: " + dodgeToHit);
		#endif
		
		Opponent.noHurt = (otherHitCoverNoHurt + otherHitNoCoverNoHurt + criticalCoverNoHurt + fatalCoverNoHurt + blockageNoHurt + missHit + dodgeToHit) / 100f;
		Opponent.hurt = (otherHitCoverHurt + otherHitNoCoverHurt + criticalCoverHurt + criticalNoCoverHurt + blockageHurt) / 100f;
		Opponent.dead = (hurtByArmorBroken > 0) ? (fatalHitCover + fatalHitNoCover) / 100f : fatalHitCover / 100f;
		Debug.Log (Opponent.Name+" <color=red>Results:</color> " + (Opponent.noHurt*100) + "%  " + (Opponent.hurt*100)+ "% "+ (Opponent.dead*100)+"%");
	}

	public class Weapons{
		public int Weight { get; set; }
		public int OtherHit { get; set; }
		public int CriticalHit { get; set; }
		public int FatalHit { get; set; }
		public int Sharpness { get; set; }
		public bool IsSharp { get ; set; }
	}
	
	public class Armors{
		public int Weight { get; set; }
		public int OtherCover { get; set; }
		public int CriticalCover { get; set; }
		public int FatalCover { get; set; }
		public int Hardness { get; set; }
	}
	
	public class Shields{
		public int Weight { get; set; }
		public int BlockRate { get; set; }
		public int Hardness { get; set; }
	}
	
	public class Formations{
		public int HitPoint { get; set; }
		public int Dodge { get; set; }
		public int Morale { get; set; }
	}


}
