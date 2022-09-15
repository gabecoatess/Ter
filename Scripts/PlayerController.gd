extends KinematicBody2D

export var move_speed := 100
export var gravity := 13000

var velocity = Vector2.ZERO

func _ready():
	velocity = Vector2(0,0)

func _physics_process(delta: float) -> void:
	velocity = Vector2(0,0)
	# reset horizontal velocity
	velocity.x = 0

	# set horizontal velocity
	if Input.is_action_pressed("right"):
		velocity.x += move_speed
	if Input.is_action_pressed("left"):
		velocity.x -= move_speed

	# apply gravity
	# player always has downward velocity
	velocity.y += gravity * delta

	# actually move the player
	velocity = move_and_slide(velocity, Vector2.UP)
