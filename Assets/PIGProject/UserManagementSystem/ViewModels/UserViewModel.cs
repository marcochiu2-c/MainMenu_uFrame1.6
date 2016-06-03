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
using UnityEngine.UI;

public partial class UserViewModel : UserViewModelBase {

	public int[] generalImageType = new int[5] ;
	//public List<StandlingDetail> StandlingDetail = new List<StandlingDetail>(){};
	public int TotalSoldierQunatity;
	public int TotalGeneral;
	public int TotalPlayer;
	public int DeadSoldiers;
	public int AliveSoldiers;
	public int LoseGear;
	public int SilverFeatherGain;
	public int ResourceGain;
}
