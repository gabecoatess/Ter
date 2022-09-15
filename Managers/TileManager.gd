extends Node2D

# Imports
var blockTilemap = preload("res://Sprites/MainTileSpritesheet.png")

# Dev variables
var worldSize = Vector2(32,32)
var selectedBlock = 1

# World variables
var tileCoordinates = []

func _ready():
	createCoordSystem(worldSize)
##	initalizeWorld()

func createCoordSystem(Vector2WorldSize):
	var indexX = 0
	var indexY = 0
	while indexY < Vector2WorldSize[1]:
		while indexX < Vector2WorldSize[0]:
			tileCoordinates.append([indexX, indexY])
			indexX += 1
		indexX = 0
		indexY += 1
	indexY = 0

##func initalizeWorld():
##	var i = 1
##	for tile in tileCoordinates:
##		if tile[1] <= 4:
##			createTile(tile, 0)
##		elif tile[1] >= 4 && tile[1] <= 5:
##			createTile(tile, 1)
##		elif tile[1] >= 5 && tile[1] <= 7:
##			createTile(tile, 2)
##		elif tile[1] >= 7 && tile[1] <= 9:
##			createTile(tile, 3)
##		else:
##			createTile(tile, 4)
			
func createTile(coords, blockId):
	if blockId != 0:
		var tileObject = StaticBody2D.new()
		var tileSprite = Sprite.new()
		var tileCollider = CollisionShape2D.new()
		var rectangle = RectangleShape2D.new()
		rectangle.extents = Vector2(8,8)
		tileCollider.shape = rectangle
		tileCollider.set_name("collider")
		tileObject.set_name("area2d")
		tileSprite.set_name("sprite")
		add_child(tileObject)
		tileObject.add_child(tileCollider)
		tileObject.add_child(tileSprite)
		tileSprite.texture = blockTilemap
		tileSprite.vframes = blockTilemap.get_size().y / 16
		tileSprite.hframes = blockTilemap.get_size().x / 16
		tileSprite.frame = blockId
		tileObject.position = Vector2((coords[0] * 16) + 8, (coords[1] * 16) + 8)

func _process(delta):
	var tempX = floor(get_global_mouse_position().x / 16)
	var tempY = floor(get_global_mouse_position().y / 16)
##
	$CoordsUI.text = str(tempX) + ", " + str(tempY)
	
	##if Input.is_action_pressed("place"):
	##	createTile(Vector2(tempX, tempY), selectedBlock)
	##	
	##if Input.is_key_pressed(KEY_1):
	##	selectedBlock = 1
	##	$Label2.text = "Grass"
	##
	##if Input.is_key_pressed(KEY_2):
	##	selectedBlock = 2
	##	$Label2.text = "Dirt"
	##
	##if Input.is_key_pressed(KEY_3):
	##	selectedBlock = 3
	##	$Label2.text = "Soft Stone"
	##	
	##if Input.is_key_pressed(KEY_4):
	##	selectedBlock = 4
	##	$Label2.text = "Hard Stone"
