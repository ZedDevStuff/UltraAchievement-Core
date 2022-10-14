using BepInEx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace UltraAchievement
{
    [BepInPlugin("zeddev.ultraachievement.core", "ultraAchievement Core", "1.0.0")]
    public class Core : BaseUnityPlugin
    {
        public static Core current;
        private static string path;
        private static List<string> obtainedAchievements;
        private GameObject overlay;
        private GameObject achievementTemplate;
        public string[][] achivementList;
        private void Awake()
        {
            current = this;
            path = $"{Application.persistentDataPath}\\achievements.uaf";
            if(File.Exists(path))
            {
                obtainedAchievements = GetAchievements();
            }
            else File.Create(path);
            // For testing purposes only
            //UnityEngine.SceneManagement.SceneManager.sceneLoaded += Scene;
            Logger.LogInfo($"Loaded UltraAchievement core");
            overlay = CreateOverlay();
            achievementTemplate = CreateTemplate();

        }
        
        public static void ShowAchievement(string iconPath, string name = "Achievement", string desc = "Example description", string mod = "unknown")
        {
            if(!HasAchievement(name))
            {
                AddAchievement(name,desc,mod);
                GameObject achievement = CreateTemplate();
                achievement.GetComponent<RectTransform>().SetParent(current.overlay.GetComponent<RectTransform>());
                achievement.GetComponent<Achievement>().InitAchievement(iconPath,name,desc);
            }
        }
        public static void ShowAchievement(Sprite icon, string name = "Achievement", string desc = "Example description", string mod = "unknown")
        {
            if(!HasAchievement(name))
            {
                AddAchievement(name, desc, mod);
                GameObject achievement = CreateTemplate();
                achievement.GetComponent<RectTransform>().SetParent(current.overlay.GetComponent<RectTransform>());
                achievement.GetComponent<Achievement>().InitAchievementI(icon,name,desc);
            }
        }
        public static void ShowAchievement(Sprite icon, string name = "Achievement", string desc = "Example description", string sprite = "", string mod = "unknown")
        {
            if (!HasAchievement(name))
            {
                AddAchievement(name,desc,mod);
                GameObject achievement = CreateTemplate(sprite);
                achievement.GetComponent<RectTransform>().SetParent(current.overlay.GetComponent<RectTransform>());
                achievement.GetComponent<Achievement>().InitAchievementI(icon, name, desc);
            }
        }
        void Scene(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
        {
            if(scene.name.Contains("Menu"))
            {
                Logger.LogInfo("In Menu");
                ShowAchievement($"{Directory.GetCurrentDirectory()}\\BepInEx\\UKMM Mods\\UKUIHelper-v0.7.0\\Sprites\\Sprite.png", "Ultrakillin' Time", "Open Ultrakill");
            }
        }

        public static GameObject CreateTemplate()
        {
            GameObject blank = CreatePanel();
            blank.name = "Achievement";
            blank.AddComponent<Achievement>();
            blank.transform.position = new Vector3(1000,1000);

            GameObject title = CreateText();
            title.name = "Title";
            RectTransform rect = title.GetComponent<RectTransform>();
            rect.SetParent(blank.GetComponent<RectTransform>());
            rect.SetAnchor(AnchorPresets.TopLeft);
            rect.SetPivot(PivotPresets.TopLeft);
            rect.anchoredPosition = new Vector2(102,0);
            rect.sizeDelta = new Vector2(300,25);
            title.GetComponent<Text>().text = "Title";
            title.GetComponent<Text>().fontSize = 20;
            title.GetComponent<Text>().fontStyle = FontStyle.Bold;

            GameObject desc = CreateText();
            desc.name = "Description";
            rect = desc.GetComponent<RectTransform>();
            rect.SetParent(blank.GetComponent<RectTransform>());
            rect.SetAsLastSibling();
            rect.SetAnchor(AnchorPresets.TopLeft);
            rect.SetPivot(PivotPresets.TopLeft);
            rect.anchoredPosition = new Vector2(102,-25);
            rect.sizeDelta = new Vector2(300,75);
            desc.GetComponent<Text>().text = "Description";
            desc.GetComponent<Text>().fontSize = 18;

            GameObject icon = CreateImage();
            icon.name = "Icon";
            rect = icon.GetComponent<RectTransform>();
            rect.SetParent(blank.GetComponent<RectTransform>());
            rect.SetAsLastSibling();
            rect.anchoredPosition = new Vector2(0,0);
            return blank;
        }

        public static GameObject CreateTemplate(string sprite)
        {
            GameObject blank = CreatePanel(sprite);
            blank.name = "Achievement";
            blank.AddComponent<Achievement>();
            blank.transform.position = new Vector3(1000, 1000);

            GameObject title = CreateText();
            title.name = "Title";
            RectTransform rect = title.GetComponent<RectTransform>();
            rect.SetParent(blank.GetComponent<RectTransform>());
            rect.SetAnchor(AnchorPresets.TopLeft);
            rect.SetPivot(PivotPresets.TopLeft);
            rect.anchoredPosition = new Vector2(102, 0);
            rect.sizeDelta = new Vector2(300, 25);
            title.GetComponent<Text>().text = "Title";
            title.GetComponent<Text>().fontSize = 20;
            title.GetComponent<Text>().fontStyle = FontStyle.Bold;

            GameObject desc = CreateText();
            desc.name = "Description";
            rect = desc.GetComponent<RectTransform>();
            rect.SetParent(blank.GetComponent<RectTransform>());
            rect.SetAsLastSibling();
            rect.SetAnchor(AnchorPresets.TopLeft);
            rect.SetPivot(PivotPresets.TopLeft);
            rect.anchoredPosition = new Vector2(102, -25);
            rect.sizeDelta = new Vector2(300, 75);
            desc.GetComponent<Text>().text = "Description";
            desc.GetComponent<Text>().fontSize = 18;

            GameObject icon = CreateImage();
            icon.name = "Icon";
            rect = icon.GetComponent<RectTransform>();
            rect.SetParent(blank.GetComponent<RectTransform>());
            rect.SetAsLastSibling();
            rect.anchoredPosition = new Vector2(0, 0);
            return blank;
        }

        private static bool HasAchievement(string name)
        {
            if(obtainedAchievements.Contains(name))
            {
                return true;
            }
            else return false;
        }
        private static void AddAchievement(string name, string desc, string mod)
        {
            obtainedAchievements.Add($"{name}.{desc}.{mod}");
            File.WriteAllLines(path, obtainedAchievements);
        }
        private List<string> GetAchievements()
        {

            List<string> achievements = File.ReadAllLines(path).ToList<string>();
            int i = 0;
            string[][] achivement = new string[achievements.Count()][];
            foreach (var ach in achievements)
            {
                achivement[i] = ach.Split(',');
                i++;
            }
            achivementList = achivement;
            return achievements;
        }

        // Please ignore all the shit below this comment :D

        private static GameObject CreateOverlay()
        {
            GameObject blank = new GameObject();
            blank.name = "Achievement Overlay";
            blank.AddComponent<Canvas>();
            blank.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            blank.GetComponent<Canvas>().sortingOrder = 1000;
            blank.AddComponent<CanvasScaler>();
            blank.AddComponent<GraphicRaycaster>();
            blank.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            blank.GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;
            blank.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920,1080);
            DontDestroyOnLoad(blank);
            return blank;
        }
        private static GameObject CreatePanel()
        {
            GameObject blank = CreateImage();
            blank.name = "Panel";
            RectTransform rect = blank.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(350,100);
            rect.SetAnchor(AnchorPresets.BottomRight);
            rect.SetPivot(PivotPresets.BottomRight);
            blank.GetComponent<Image>().color = new Color(0.2745f,0.2745f,0.3529f,1f);
            return blank;
        }
        private static GameObject CreatePanel(string sprite)
        {
            GameObject blank = CreateImage();
            blank.name = "Panel";
            RectTransform rect = blank.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(350, 100);
            rect.SetAnchor(AnchorPresets.BottomRight);
            rect.SetPivot(PivotPresets.BottomRight);
            if (sprite != "") {
                blank.GetComponent<Image>().sprite = Core.LoadSprite(sprite, Vector4.zero,100);
                return blank;
            }
            blank.GetComponent<Image>().color = new Color(0.2745f, 0.2745f, 0.3529f, 1f);
            return blank;
        }
        private static GameObject CreateImage()
        {
            GameObject blank = new GameObject();
            blank.name = "Image";
            blank.AddComponent<RectTransform>();
            RectTransform rect = blank.GetComponent<RectTransform>();
            blank.AddComponent<CanvasRenderer>();
            rect.sizeDelta = new Vector2(100,100);
            rect.SetAnchor(AnchorPresets.TopLeft);
            rect.SetPivot(PivotPresets.TopLeft);
            blank.AddComponent<Image>();
            return blank;
        }
        private static GameObject CreateText()
        {
            GameObject blank = new GameObject();
            blank.name = "Text";
            blank.AddComponent<RectTransform>();
            blank.AddComponent<CanvasRenderer>();
            RectTransform rect = blank.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(500, 50);
            rect.SetAnchor(AnchorPresets.MiddleCenter);
            rect.SetPivot(PivotPresets.MiddleCenter);
            blank.AddComponent<Text>();
            blank.GetComponent<Text>().text = "Text";
            blank.GetComponent<Text>().font = Font.GetDefault();
            blank.GetComponent<Text>().fontSize = 30;
            blank.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
            blank.GetComponent<Text>().color = Color.white;
            return blank;
        }
        public static Sprite LoadSprite(string path, Vector4 border, float pixelsPerUnit)
        {
            Texture2D texture = new Texture2D(200, 200);
            texture.LoadImage(File.ReadAllBytes(path));
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelsPerUnit, 0, SpriteMeshType.Tight, border);
        }
    }

}
