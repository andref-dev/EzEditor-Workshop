#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using EzEditor;

[CustomEditor(typeof(TerrainController))]
public class TerrainControllerEditor : Editor
{
    private TerrainController _target;
    
    private bool _tileOptionsFoldout;

    private TileOption _newTile = new TileOption();
    
    public override void OnInspectorGUI()
    {
        if(_target == null)
            _target = target as TerrainController;

        DrawDefaultInspector();

        DrawTileOptions();
        
        DrawButtons();
    }

    private void DrawTileOptions()
    {   
        _tileOptionsFoldout = gui.EzFoldout("Tile Options", _tileOptionsFoldout);

        if (!_tileOptionsFoldout)
            return;

        if (_target.TileOptions.Count == 0)
        {
            gui.EzHelpBox("Lista de options vazia. Nao sera possivel criar um terreno.", MessageType.Warning, true);
        }
        
        foreach (var option in _target.TileOptions)
        {
            using (gui.Horizontal())
            {
                option.Name = gui.EzTextField("", option.Name);
                option.Sprite = gui.EzObjectField("", option.Sprite, 0, GUILayout.Width(100));
                option.Weight = gui.EzIntField("Weight", option.Weight);
                if (gui.EzButton(gui.DeleteButton))
                {
                    _target.TileOptions.Remove(option);
                }
            }
        }

        using (gui.Horizontal())
        {
            _newTile.Sprite = gui.EzObjectField("New Tile", _newTile.Sprite);
            _newTile.Weight = gui.EzIntField("Weight", _newTile.Weight);
            if (_newTile.Sprite != null)
            {
                _target.TileOptions.Add(_newTile);
                _newTile = new TileOption();
            }
        }
    }

    private void DrawButtons()
    {
        
        gui.HorizontalBar(6);
        
        if (gui.EzGrayoutButton("Create Terrain", _target.TileOptions.Count == 0))
        {
            _target.CreateTerrain();
        }

        if (gui.EzButton("Create COins"))
        {
            _target.CreateCoins();
        }

        if (gui.EzButton("Clear Level"))
        {
            _target.ClearLevel();
        }
    }
}
#endif //UNITY_EDITOR