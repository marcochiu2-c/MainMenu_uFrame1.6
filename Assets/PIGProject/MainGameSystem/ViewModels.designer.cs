// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

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


public partial class MainGameRootViewModelBase : uFrame.MVVM.ViewModel {
    
    public MainGameRootViewModelBase(uFrame.Kernel.IEventAggregator aggregator) : 
            base(aggregator) {
    }
    
    public override void Bind() {
        base.Bind();
    }
    
    public override void Read(ISerializerStream stream) {
        base.Read(stream);
    }
    
    public override void Write(ISerializerStream stream) {
        base.Write(stream);
    }
    
    protected override void FillCommands(System.Collections.Generic.List<uFrame.MVVM.ViewModelCommandInfo> list) {
        base.FillCommands(list);
    }
    
    protected override void FillProperties(System.Collections.Generic.List<uFrame.MVVM.ViewModelPropertyInfo> list) {
        base.FillProperties(list);
    }
}

public partial class MainGameRootViewModel {
    
    public MainGameRootViewModel(uFrame.Kernel.IEventAggregator aggregator) : 
            base(aggregator) {
    }
}
