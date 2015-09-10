using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Kernel;
using uFrame.MVVM;
using uFrame.MVVM.Services;
using uFrame.MVVM.Bindings;
using uFrame.Serialization;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/*
 * This view shows an example of manually defined bindings. Bindings that we specify in the designer
 * are autogenerated for us, but they are quite simple. In this example we define some bindings manually.
 * This view also containes auto-generated bindings in the base class (SettingsScreenViewBase)
 * You can configure them using the inspector.
 */
public class SettingsScreenView : SettingsScreenViewBase
{

    public Text ResolutionText;
    public Text VolumeText;
    public Button NextResolutionButton;
    public Button PrevResolutionButton;

    public override void Bind() {
        base.Bind();

        /*
         * Manual binding for the buttons: invoke same method with different arguments,
         * based on what button you click.
         */
        this.BindButtonToHandler(NextResolutionButton, () => SelectResolutionWithOffset(1));
        this.BindButtonToHandler(PrevResolutionButton, () => SelectResolutionWithOffset(-1));
        
        /*
         * Manual binding for the text object:
         * we use property selector to convert float to text and format the result string
         */
        this.BindTextToProperty(VolumeText, SettingsScreen.VolumeProperty, f => string.Format("Volume: {0}", f));
    }

    private void SelectResolutionWithOffset(int offset)
    {
        /* Grab all the resolutions */
        var resolutions = SettingsScreen.AvailableResolutions;
        
        /* 
         * Find the index of the current resultion 
         * Please notice: we only do this to avoid boilerplate
         * variable to store index of currently selected item,
         * which you can freely define to avoid searching overhead.
         */
        var currentIndex = resolutions.IndexOf(SettingsScreen.Resolution);
        
        /* shift index by offset. Use modulo to keep index inside of collection bounds. */
        var i = (currentIndex + offset) % resolutions.Count;

        /* In case result index is negative, switch to the last element */
        if (i < 0) i = resolutions.Count - 1;

        /* 
         * IMPORTANT: modifying view model property from the view is GENERALLY not a good practice.
         * However if you do not need any validation logic, it makes sence. Bindings work this way. They live
         * on the view and modify the properties of the viewmodel based on UI controls. However, if you need any kind
         * of validation in the Controllers/Service layer, you should introduce a command to set this value. Then,
         * in the command handler you can check if value is valid and set the view model property.
         */
        SettingsScreen.Resolution = resolutions[i];
    }

    /*
    * Here we use regular binding handler to setup text based on current resolution information.
    */
    public override void ResolutionChanged(ResolutionInformation arg1)
    {
        if(arg1 != null) ResolutionText.text = string.Format("{0} x {1}", arg1.Width, arg1.Height);
    }




}
