using UnityEngine;
using UnityEditor;
using System.Collections;

public class UIPanelWatcherConfig {
    //��ǰ���ڼ��ص����
    public static string loadingPanel = string.Empty;

    //��ǰ���ڵȴ����ص����
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
