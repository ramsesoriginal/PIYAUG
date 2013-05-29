using UnityEngine;
using System.Collections;

/// <summary>
/// Take and store hi-res screenshots. Apply on a Camera
/// </summary>
public class HiResScreenShots : PIYAUGBehaviourBase {
	
	[HideInInspector]
    public int resWidth = 2550; 
	[HideInInspector]
    public int resHeight = 3300;
 
    private bool takeHiResShot = false;
	private string fileName = "";
 
    private static string ScreenShotName(int width, int height) {
        return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
                             Application.dataPath, 
                             width, height, 
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
 
	/// <summary>
	/// Take a hires shot.
	/// </summary>
    public string TakeHiResShot() {
        takeHiResShot = true;
		fileName = ScreenShotName(resWidth, resHeight);
		return fileName;
    }
 
	/// <summary>
	/// if necesarry, takes a screenshot and saves it
	/// </summary>
    void LateUpdate() {
        takeHiResShot |= Input.GetKeyDown("p");
        if (takeHiResShot) {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            camera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            camera.targetTexture = null;
            RenderTexture.active = null; // added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();
			if (string.IsNullOrEmpty(fileName))
            	fileName = ScreenShotName(resWidth, resHeight);
            System.IO.File.WriteAllBytes(fileName, bytes);
            Logger.Log(string.Format("Took screenshot to: {0}", fileName));
            takeHiResShot = false;
			fileName = "";
        }
    }
}