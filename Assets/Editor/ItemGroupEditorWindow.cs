using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ItemGroupEditorWindow : EditorWindow
{
    private ItemGroup _itemGroup;
    private Item _selectedItem;

    [MenuItem("Tools/Item Group Editor")]
    public static void ShowWindow()
    {
        GetWindow<ItemGroupEditorWindow>("Item Group Editor");
    }

    private void OnGUI()
    {
        _itemGroup = (ItemGroup)EditorGUILayout.ObjectField("Item Group", _itemGroup, typeof(ItemGroup), false);

        if (_itemGroup == null)
        {
            EditorGUILayout.HelpBox("Hãy kéo một ItemGroup ScriptableObject vào đây.", MessageType.Warning);
            return;
        }

        // Bắt đầu một group để chứa nút và label
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Select an Item", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        if (_itemGroup.items.Count == 0)
        {
            EditorGUILayout.HelpBox("Danh sách items rỗng. Hãy tạo một item mới.", MessageType.Warning);
        }

        // --- Giới hạn hàng ngang ---
        int itemsPerRow = 5;
        int itemIndex = 0;

        foreach (var item in _itemGroup.items)
        {
            if (itemIndex % itemsPerRow == 0)
            {
                if (itemIndex > 0)
                {
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.BeginHorizontal();
            }

            if (item != null && item.itemIcon != null)
            {
                if (GUILayout.Button(item.itemIcon.texture, GUILayout.Width(64), GUILayout.Height(64)))
                {
                    _selectedItem = item;
                    GUI.FocusControl(null);
                }
            }
            itemIndex++;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(20);

        if (_selectedItem != null)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("Item Details", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            _selectedItem.itemIcon = (Sprite)EditorGUILayout.ObjectField(_selectedItem.itemIcon, typeof(Sprite), false, GUILayout.Width(100), GUILayout.Height(100));
            EditorGUILayout.BeginVertical();
            _selectedItem.itemName = EditorGUILayout.TextField("Tên:", _selectedItem.itemName);
            _selectedItem.itemMota = EditorGUILayout.TextField("Mô tả:", _selectedItem.itemMota);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Player Stats", EditorStyles.boldLabel);
            _selectedItem.bounusDamage = EditorGUILayout.FloatField("Tăng sát thương", _selectedItem.bounusDamage);
            _selectedItem.bonusHeal = EditorGUILayout.FloatField("Tăng máu:", _selectedItem.bonusHeal);
            _selectedItem.bonusAmor = EditorGUILayout.FloatField("Tăng giáp:", _selectedItem.bonusAmor);
            _selectedItem.bonusSpeed = EditorGUILayout.FloatField("Tăng tốc độ:", _selectedItem.bonusSpeed);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Weapon Stats", EditorStyles.boldLabel);
            _selectedItem.bonusCooldown = EditorGUILayout.FloatField("Tăng tốc độ bắn::", _selectedItem.bonusCooldown);
            _selectedItem.bonusMagazineSize = EditorGUILayout.IntField("Tăng băng đạn:", _selectedItem.bonusMagazineSize);
            _selectedItem.bonusTotalAmmo = EditorGUILayout.IntField("Tăng tổng đạn:", _selectedItem.bonusTotalAmmo);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(_selectedItem);
            }
        }
    }

}