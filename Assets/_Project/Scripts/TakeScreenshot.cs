using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

namespace _Project.Scripts
{
    public class TakeScreenshot : MonoBehaviour
    {
        [Tooltip("Name for the screenshot file")]
        public string fileName = "Screenshot";
        
        private static Random random = new Random();

        public CanvasGroup m_MenuCanvas = null;
        Texture2D m_Texture;

        bool m_TakeScreenshot;
        bool m_ScreenshotTaken;
        bool m_IsFeatureDisable;

        string getPath() => k_ScreenshotPath + fileName + "-" + RandomString(5)+".png";

        const string k_ScreenshotPath = "Assets/Screenshots/";

        void Update()
        {
            if (m_IsFeatureDisable)
                return;

            if (m_TakeScreenshot)
            {
                m_MenuCanvas.alpha = 0;
                ScreenCapture.CaptureScreenshot(getPath());
                m_TakeScreenshot = false;
                m_ScreenshotTaken = true;
                return;
            }

            if (m_ScreenshotTaken)
            { 
                LoadScreenshot();
#if UNITY_EDITOR
                AssetDatabase.Refresh();
#endif

                m_MenuCanvas.alpha = 1;
                m_ScreenshotTaken = false;
            }
        }

        public void OnTakeScreenshotButtonPressed()
        {
            m_TakeScreenshot = true;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        void LoadScreenshot()
        {
            if (File.Exists(getPath()))
            {
                var bytes = File.ReadAllBytes(getPath());

                m_Texture = new Texture2D(2, 2);
                m_Texture.LoadImage(bytes);
                m_Texture.Apply();
                // previewImage.texture = m_Texture;
            }
        }
    }
}
