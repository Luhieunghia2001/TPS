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

        // Nút để tạo Item mới
        if (GUILayout.Button("Create New Item", GUILayout.Width(150)))
        {
            CreateNewItem();
        }
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
            _selectedItem.bonusHeal = EditorGUILayout.FloatField("Tăng máu:", _selectedItem.bonusHeal);
            _selectedItem.bonusAmor = EditorGUILayout.FloatField("Tăng giáp:", _selectedItem.bonusAmor);
            _selectedItem.bonusSpeed = EditorGUILayout.FloatField("Tăng tốc độ:", _selectedItem.bonusSpeed);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Weapon Stats", EditorStyles.boldLabel);
            _selectedItem.bonusCooldown = EditorGUILayout.FloatField("Giảm hồi chiêu:", _selectedItem.bonusCooldown);
            _selectedItem.bonusMagazineSize = EditorGUILayout.IntField("Tăng băng đạn:", _selectedItem.bonusMagazineSize);
            _selectedItem.bonusTotalAmmo = EditorGUILayout.IntField("Tăng tổng đạn:", _selectedItem.bonusTotalAmmo);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(_selectedItem);
            }
        }
    }

    private void CreateNewItem()
    {
        // Tạo một instance mới của ScriptableObject Item
        Item newItem = ScriptableObject.CreateInstance<Item>();
        newItem.itemName = "New Item";

        // Lấy đường dẫn của ItemGroup
        string path = AssetDatabase.GetAssetPath(_itemGroup);
        if (string.IsNullOrEmpty(path))
        {
            path = "Assets";
        }

        // Tạo đường dẫn mới cho Item
        string newPath = AssetDatabase.GenerateUniqueAssetPath(path.Replace(".asset", "") + "/" + newItem.itemName + ".asset");

        // Lưu Item mới vào Project
        AssetDatabase.CreateAsset(newItem, newPath);

        // Thêm Item vào danh sách của ItemGroup
        _itemGroup.items.Add(newItem);

        // Đánh dấu ItemGroup và Item mới là "dirty" để lưu thay đổi
        EditorUtility.SetDirty(_itemGroup);
        EditorUtility.SetDirty(newItem);

        // Hiển thị và chọn Item mới trong Project view
        EditorGUIUtility.PingObject(newItem);

        // Cập nhật lại cửa sổ
        Repaint();
    }
}