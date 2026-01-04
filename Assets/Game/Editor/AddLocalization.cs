using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.Editor
{
    public static class AddLocalization
    {
        [MenuItem("GameObject/UI/Localization Text", false, 2000)]
        public static void CreateSpecialText(MenuCommand menuCommand)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Game/Editor/Localization Text.prefab");
            if (prefab == null)
            {
                Debug.LogError("MySpecialText prefab not found!");
                return;
            }
            
            GameObject parent = menuCommand.context as GameObject;
            Canvas canvas = parent != null ? parent.GetComponentInParent<Canvas>() : null;
            if (canvas == null)
            {
                GameObject canvasGO = new GameObject("Canvas");
                canvas = canvasGO.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasGO.AddComponent<CanvasScaler>();
                canvasGO.AddComponent<GraphicRaycaster>();
                Undo.RegisterCreatedObjectUndo(canvasGO, "Create Canvas");

                parent = canvasGO;
            }
            else
            {
                parent = canvas.gameObject;
            }

            // Ensure EventSystem exists
            if (Object.FindObjectOfType<EventSystem>() == null)
            {
                GameObject eventSystemGO = new GameObject("EventSystem");
                eventSystemGO.AddComponent<EventSystem>();
                eventSystemGO.AddComponent<StandaloneInputModule>();
                Undo.RegisterCreatedObjectUndo(eventSystemGO, "Create EventSystem");
            }

            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent.scene);
            Undo.RegisterCreatedObjectUndo(instance, "Create My Special Text");

            instance.transform.SetParent(parent.transform, false);
            instance.transform.localPosition = Vector3.zero;

            PrefabUtility.UnpackPrefabInstance(
                instance,
                PrefabUnpackMode.Completely,
                InteractionMode.AutomatedAction
            );
            
            Selection.activeGameObject = instance;
            EditorSceneManager.MarkSceneDirty(parent.scene);
        }
    }
}
