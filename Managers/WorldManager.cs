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
        
        private UInt32 worldWidth = 10;//8400;
        private UInt32 worldHeight = 2;//2400;

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
            Int32 worldCoordX = ((Int32)Math.Floor(GetGlobalMousePosition().x / 16) * 16) + 8;
            Int32 worldCoordY = ((Int32)Math.Floor(GetGlobalMousePosition().y / 16) * 16) + 8;

            Int32 gameCoordX = (Int32)Math.Floor(GetGlobalMousePosition().x / 16);
            Int32 gameCoordY = (Int32)Math.Floor(GetGlobalMousePosition().y / 16);

            String tileGameCoords = $"{gameCoordX},{gameCoordY}";

            if (activeTiles.Contains($"{gameCoordX},{gameCoordY}")) {
                GD.Print("Tile already exists there!");
            } else {
                tileManager.createTile(this, new TileManagement.Tile(1, true), worldCoordX, worldCoordY, gameCoordX, gameCoordY);

                activeTiles.Add(tileGameCoords);
            }
        }

        private void deleteTileFromWorld() {
            Int32 worldCoordX = ((Int32)Math.Floor(GetGlobalMousePosition().x / 16) * 16) + 8;
            Int32 worldCoordY = ((Int32)Math.Floor(GetGlobalMousePosition().y / 16) * 16) + 8;

            Int32 gameCoordX = (Int32)Math.Floor(GetGlobalMousePosition().x / 16);
            Int32 gameCoordY = (Int32)Math.Floor(GetGlobalMousePosition().y / 16);
                
            String tileGameCoords = $"{gameCoordX},{gameCoordY}";
                
            if (activeTiles.Contains($"{gameCoordX},{gameCoordY}")) {
                tileManager.deleteTile(this, gameCoordX, gameCoordY);

                activeTiles.Remove(tileGameCoords);
            } else {
                GD.Print("No tiles to remove!");
            }
        }

        private void createTileInWorld(Int32 gameCoordX, Int32 gameCoordY) {
            Int32 worldCoordX = (Int32)Math.Floor(gameCoordX * 16.0) + 8;
            Int32 worldCoordY = (Int32)Math.Floor(gameCoordY * 16.0) + 8;

            String tileGameCoords = $"{gameCoordX},{gameCoordY}";

            tileManager.createTile(this, new TileManagement.Tile(1, true), worldCoordX, worldCoordY, gameCoordX, gameCoordY);

            activeTiles.Add(tileGameCoords);
        }

        private void removeTileFromWorld(Int32 worldCoordX, Int32 worldCoordY) {
            Int32 gameCoordX = (Int32)Math.Floor(worldCoordX / 16.0);
            Int32 gameCoordY = (Int32)Math.Floor(worldCoordY / 16.0);

            String tileGameCoords = $"{gameCoordX},{gameCoordY}";

            tileManager.deleteTile(this, gameCoordX, gameCoordY);

            activeTiles.Remove(tileGameCoords);
        }

        private void CreateWorld() {
            for (int gameCoordY = 0; gameCoordY < worldHeight; gameCoordY++) {
                for (int gameCoordX = 0; gameCoordX < worldWidth; gameCoordX++) {
                    createTileInWorld(gameCoordX, gameCoordY + 4);
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