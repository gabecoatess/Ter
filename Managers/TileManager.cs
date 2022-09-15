using Godot;
using System;

// Name: TileManager
// Description: TileManager.cs controls creating tiles, placing tiles, and deleting tiles.

namespace TileManagement {

	// ================
	// = Misc Classes =
	// ================
	public class Tile {
		public ushort type;
		public bool hasCollision;

		public Tile(ushort type, bool hasCollision) {
			this.type = type;
			this.hasCollision = hasCollision;
		}
	}

	public class TileManager : Node2D {

		// ==================
		// = Imported Files =
		// ==================
		public Texture MainSpritesheet = (Texture)GD.Load("res://Sprites/MainTileSpritesheet.png");


		// ==================
		// = Custom Methods =
		// ==================
		public void createTile(Node2D tileHolder, Tile tile, Int32 worldCoordX, Int32 worldCoordY, Int32 gameCoordX, Int32 gameCoordY) {

			StaticBody2D tileObject = new StaticBody2D();
			Sprite tileSprite = new Sprite();
			CollisionShape2D tileCollider = new CollisionShape2D();
			RectangleShape2D colliderBounds = new RectangleShape2D();

			tileObject.Name = $"{gameCoordX},{gameCoordY}";
			tileHolder.AddChild(tileObject);

			tileSprite.Name = "Tile_Sprite";
			tileObject.AddChild(tileSprite);

			if (tile.hasCollision) {
			    colliderBounds.Extents = new Vector2(8, 8);
			    tileCollider.Shape = colliderBounds;

			    tileCollider.Name = "Tile_Collider";
			    tileObject.AddChild(tileCollider);
			} else {
			    tileCollider.Disabled = true;
			}

			tileSprite.Texture = MainSpritesheet;
			tileSprite.Vframes = (int)MainSpritesheet.GetData().GetSize().y / 16;
			tileSprite.Hframes = (int)MainSpritesheet.GetData().GetSize().x / 16;

            if (tileHolder.GetNodeOrNull($"{gameCoordX},{gameCoordY - 1}") != null) {
                tileSprite.Frame = 2;
            } else {
            	tileSprite.Frame = 1;
				if (tileHolder.GetNodeOrNull($"{gameCoordX},{gameCoordY + 1}") != null) {
					tileHolder.GetNode($"{gameCoordX},{gameCoordY + 1}").GetNode<Sprite>("Tile_Sprite").Frame = 2;
				} else {
					GD.Print(tileHolder.GetNodeOrNull($"{gameCoordX},{gameCoordY + 1}") == null);
					GD.Print($"{gameCoordX},{gameCoordY}");
				}
            }
		
			tileObject.Position = new Vector2(worldCoordX, worldCoordY);
		}
	
        public void deleteTile(Node2D tileHolder, Int32 gameCoordX, Int32 gameCoordY) {

			if (tileHolder.GetNodeOrNull($"{gameCoordX},{gameCoordY + 1}") == null) {
				tileHolder.GetNode($"{gameCoordX},{gameCoordY}").QueueFree();
			} else {
				tileHolder.GetNode($"{gameCoordX},{gameCoordY + 1}").GetNode<Sprite>("Tile_Sprite").Frame = 1;
				tileHolder.GetNode($"{gameCoordX},{gameCoordY}").QueueFree();
			}
            
        }
    }
}
