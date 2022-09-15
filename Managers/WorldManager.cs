using Godot;
using System;
using TileManagement;
using System.Collections.Generic;
using System.Collections;

// Name: WorldManager
// Description: WorldManager.cs keeps track of all the tiles in the world.

namespace WorldManagement {

    public class WorldManager : Node2D {
        
        // ===================
        // = World variables =
        // ===================
        public List<String> activeTiles = new List<String>();
        
        private UInt32 worldWidth = 32;//8400;
        private UInt32 worldHeight = 32;//2400;

        private UInt32 maxDistanceWidth = 0;
        private UInt32 maxDistanceHeight = 0;

        // ====================
        // = Player variables =
        // ====================
        private Node2D playerObject; 


        // ===========
        // = Imports =
        // ===========
        private TileManager tileManager = new TileManager();

        
        // ==================
        // = Custom Methods =
        // ==================
        private void placeTileInWorld() {
            Int32 tileX = ((Int32)Math.Floor(GetGlobalMousePosition().x / 16) * 16) + 8;
            Int32 tileY = ((Int32)Math.Floor(GetGlobalMousePosition().y / 16) * 16) + 8;

            Int32 rawTileX = (Int32)Math.Floor(GetGlobalMousePosition().x / 16);
            Int32 rawTileY = (Int32)Math.Floor(GetGlobalMousePosition().y / 16);

            String tileCoords = $"{rawTileX},{rawTileY}";

            if (activeTiles.Contains($"{rawTileX},{rawTileY}")) {
                GD.Print("Tile already exists there!");
            } else {
                tileManager.createTile(this, new TileManagement.Tile(1, true), tileX, tileY, rawTileX, rawTileY);

                activeTiles.Add(tileCoords);
            }
        }

        private void deleteTileFromWorld() {
            Int32 tileX = ((Int32)Math.Floor(GetGlobalMousePosition().x / 16) * 16) + 8;
            Int32 tileY = ((Int32)Math.Floor(GetGlobalMousePosition().y / 16) * 16) + 8;

            Int32 rawTileX = (Int32)Math.Floor(GetGlobalMousePosition().x / 16);
            Int32 rawTileY = (Int32)Math.Floor(GetGlobalMousePosition().y / 16);
                
            String tileCoords = $"{rawTileX},{rawTileY}";
                
            if (activeTiles.Contains($"{rawTileX},{rawTileY}")) {
                tileManager.deleteTile(this, tileX, tileY);

                activeTiles.Remove(tileCoords);
            } else {
                GD.Print("No tiles to remove!");
            }
        }

        private void createTileInWorld(Int32 x, Int32 y) {
            Int32 tileX = (x * 16) + 8;
            Int32 tileY = (y * 16) + 8;

            String tileCoords = $"{x},{y}";

            tileManager.createTile(this, new TileManagement.Tile(1, true), tileX, tileY, tileX, tileY);

            activeTiles.Add(tileCoords);
        }

        private void removeTileFromWorld(Int32 x, Int32 y) {
            Int32 tileX = (x * 16) + 8;
            Int32 tileY = (y * 16) + 8;

            String tileCoords = $"{x},{y}";

            tileManager.deleteTile(this, tileX, tileY);

            activeTiles.Remove(tileCoords);
        }

        private void CreateWorld() {
            for (int y = 0; y < worldHeight; y++) {
                for (int x = 0; x < worldWidth; x++) {
                    createTileInWorld(x, y + 4);
                }    
            }
        }

        //private void CreateWorld() {
        //    for (int y = 0; y < worldHeight; y++) {
        //        for (int x = 0; x < worldWidth; x++) {
        //            activeTiles.Add($"{x},{y}");
        //        }
        //    }
        //}

        // ====================
        // = Override Methods =
        // ====================
        public override void _Ready() {
            CreateWorld();
            playerObject = (Node2D)GetNode("../PlayerObject");
        }


        public override void _Process(float delta) {

            if (Input.IsActionJustPressed("place") && !Input.IsActionPressed("delete")) {
                
                placeTileInWorld();
            }

            if (Input.IsActionJustPressed("delete") && !Input.IsActionPressed("place")) {
                
                deleteTileFromWorld();
            }

            
        }
    }
}