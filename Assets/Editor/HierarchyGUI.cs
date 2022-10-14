using UnityEngine;
using UnityEditor;

public class HierarchyGUI : MonoBehaviour
{
    private const int ICON_SIZE = 16;

    private const int WIDTH = 16;
    private const int OFFSET = 13;

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instanceID, Rect selectionRect)
    {
        // instanceID をオブジェクト参照に変換
        GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (go == null)
        {
            return;
        }

        // オブジェクトが所持しているコンポーネント一覧を取得
        Component[] components = go.GetComponents<Component>();
        if (components.Length == 0)
        {
            return;
        }

        int count = 0;

        foreach (Component component in components)
        {
            // コンポーネントのアイコン画像を取得
            Texture2D texture2D = AssetPreview.GetMiniThumbnail(component);

            if (!texture2D.name.Contains("d_cs Script Icon")) { continue; }
            count++;
        }

        selectionRect.x = selectionRect.xMax - ICON_SIZE * count;
        selectionRect.width = ICON_SIZE;

        foreach (Component component in components)
        {
            // コンポーネントのアイコン画像を取得
            Texture2D texture2D = AssetPreview.GetMiniThumbnail(component);

            if (!texture2D.name.Contains("d_cs Script Icon")) { continue; }

            GUI.DrawTexture(selectionRect, texture2D);
            selectionRect.x += ICON_SIZE;
        }

        Rect pos = selectionRect;
        pos.x = pos.xMax - OFFSET;
        pos.width = WIDTH;

        bool active = GUI.Toggle(pos, go.activeSelf, string.Empty);
        if (active == go.activeSelf)
        {
            return;
        }

        Undo.RecordObject(go, $"{(active ? "Activate" : "Deactivate")} GameObject '{go.name}'");
        go.SetActive(active);
        EditorUtility.SetDirty(go);
    }
}
