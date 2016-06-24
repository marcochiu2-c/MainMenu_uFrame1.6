using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using uFrame.Kernel;

/*
 * This is an example of how to use uFrameComponent.
 * uFrameComponent is generally just a monobehaviour connected with the
 * uFrame infrastructure using EventAggregator. As a result, you can
 * Publish and Subscribe to events. Please note, that uFrameComponent
 * is extremely simple, so you can refere to the source code and recreate similar
 * functionality in your own monobehaviour-based classes.
 */
public class IntroSceneFlow : uFrameComponent
{

    /* Sprite to be animated */
    public SpriteRenderer Poem0_1;
	public SpriteRenderer Poem0_2;
	public SpriteRenderer Poem0_3;
	public SpriteRenderer Poem0_4;
	
	public GameObject BrokenChurch;
	public GameObject Poem0;

	public Slider loadingBar;
	public GameObject loadingImage;
	
	private AsyncOperation _async;
	
    /* Animation progress (transparency in our case) */
    private float progress;

    /* This method is invoked when uFrame infrastructure is ready: 
     * SystemLoaders and SceneLoaders are registered, Services are set up and ready;
     * Simply speaking: kernel is loaded.
     */
    public override void KernelLoaded()
    {
        base.KernelLoaded();
		//SetZeroAlpha(Poem0_1);
		//SetZeroAlpha(Poem0_2);
		//SetZeroAlpha(Poem0_3);
		//SetZeroAlpha(Poem0_4);
        StartCoroutine(ShowIntro());
    }

    /*
     * Coroutine with animation, notice that it publishes event in the end
     */
    private IEnumerator ShowIntro()
    {
        
		StartCoroutine(SetAlpha(Poem0_1, 0.3f));
		
		yield return new WaitForSeconds(2f);
		
		StartCoroutine(SetAlpha(Poem0_2, 0.3f));
		
		yield return new WaitForSeconds(2f);
		
		StartCoroutine(SetAlpha(Poem0_3, 0.3f));
		
		yield return new WaitForSeconds(2f);
		
		StartCoroutine(SetAlpha(Poem0_4, 0.3f));
		
		yield return new WaitForSeconds(4f);
		
		BrokenChurch.gameObject.SetActive(true);
		
		Poem0.gameObject.SetActive(false);
		
		Publish(new DialogueCommand()
		{
			ConversationName = "Ch0_1"
		});
		
		//BrokenChurch.gameObject.SetActive(false);
		
		Publish(new DialogueCommand()
		{
			ConversationName = "Ch0_2"
		});
		
		//loadingImage.SetActive (true);
		//StartCoroutine (LoadLevelWithBar ("MainMenuScene"));
        //In the end let the system know that intro is finished
        //Publish(new IntroFinishedEvent());
    }
	
	private void SetZeroAlpha(SpriteRenderer image)
	{
		var solidColor = image.color;
		var transparentColor = new Color(solidColor.r,solidColor.g,solidColor.b,0);
		image.color = transparentColor;
	}
	
	private IEnumerator SetAlpha(SpriteRenderer image, float time)
	{
		var solidColor = new Color(image.color.r,image.color.g,image.color.b,1);
		var transparentColor = new Color(image.color.r,image.color.g,image.color.b,0);
		image.color = transparentColor;
		
		while (progress < 1f)
		{
			image.color = Color.Lerp(transparentColor, solidColor, progress);
			progress += time * Time.deltaTime;
			yield return new WaitForSeconds(0.1f);
		}
	}
	
	private IEnumerator LoadLevelWithBar (string level)
	{
		_async = Application.LoadLevelAsync(level);
		while(!_async.isDone)
		{
			loadingBar.value=_async.progress;
			yield return null;
		}
		
	}


}
