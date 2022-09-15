# Ter - A Terraria Clone

This Godot project was created to not only learn about but also experiment with the features of the video game called Terraria. This project also includes inspiration from Starbound.


## Notes
### Coordinate Systems
An issue that I ran into while making the world manager and tile manager is that I had two types of coordinate systems that I used for different purposes. It was code that I had wrote that I cannot read back to myself and understand how it works. I will one day go back to it and see what I can do to make it more concise but the best I could do was to write any bit of information here.

#### World Coordinates - GoDot provided (ex. [600, 300])
The World Coordinates are the X and Y that Godot provides in their own scaling. This is also known as global position in the engine. This is used for figuring out how the grid system anchors each tile and object that exists.

#### Game Coordinates - Programmatically provided (ex. [1, 5])
The Game Coordinates are the X and Y that I have made myself. This is the coordinate system that each tile follows in order to be placed correctly. There is 16 pixels in between each whole number, so from world coordinates to game coordinates it looks like this.

Game Coordinates: [1, 5]
World Coordinates: [16, 80]

Game Coordinates: [21, 27]
World Coordinates: [336, 432]

In the Tile Manager (TileManager.cs), I have a createTile() method that takes a few parameters. You can see the details below:

createTile( type:Godot.Node2D tileHolder, type:TileManager.Tile tile, type:Int32 x, type:Int32 y, type:Int32 coordX, type:Int32 coordY ) returns: void

Godot.Node2D tileHolder: A Node2D object that exists within the game world to hold the tiles.
TileManager.Tile tile: A class that is custom created to hold simple tile data.
Int32 x: Horizontal World Coordinate
Int32 y: Vertical World Coordinate
Int32 coordX: Horizontal Game Coordinate
Int32 coordY: Vertical Game Coordinate

The distinction between Int32 x, Int32 y, Int32 coordX, and Int32 coordY is very hard to determine. Where these values are stored around the Tile Manager also has tough distinction. So they have been renamed to:

x (world) = worldCoordX
y (world) = worldCoordY
coordX (game) = gameCoordX
coordY (game) = gameCoordY

Ideally, I would like to create two custom methods in the Game Manager that can easily translate between the two, but that is for the future.

The World Coordinates clamping to the grid looks like this:
Int32 worldCoordX = ((Int32)Math.Floor(GetGlobalMousePosition().x / 16) * 16) + 8;

The math looks like this:

1. Get the global mouse position on either X or Y axis = x:132.63889
2. Divide the value by 16 = x:8.289930625
3. Floor the value to get rid of the decimals = x:8
4. Multiply the floored value by 16 = x:128
5. Offset it by +8 because sprite is rendered at the center = x:136

The World Coordinates from Game Coordinates looks like this:
Int32 worldCoordX = (Int32)Math.Floor(gameCoordX * 16.0) + 8;

1. Get game coordinate = x:5
2. Using the game coordinate, multiply it by 16 pixels since 1 game coord is equal to 16 world coord = x:80
3. Floor the number in case it is a decimal for whatever reason = x:80
4. Offset by +8 pixels because sprite is rendered at the center = x:88