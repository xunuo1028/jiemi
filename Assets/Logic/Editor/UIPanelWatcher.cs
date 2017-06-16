using UnityEngine;
using UnityEditor;
using System.Collections;

public class UIPanelWatcherConfig {
    //当前正在加载的面板
    public static string loadingPanel = string.Empty;

    //当前正在等待加载的面板
    public static string nextWaitingPanel = string.Empty;

}

public class UIPanelWatcher : EditorWindow
{
    private static void Init()
    {
        UIPanelWatcher window = (UIPanelWatcher)EditorWindow.GetWindow(typeof(UIPanelWatcher));
        window.Show();
    }
}
