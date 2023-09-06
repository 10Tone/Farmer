extends Node2D

var load_result = {}

func load_data():
	var file = File.new()
	file.open("res://JsonFiles/CraftingRecipesTable.json", file.READ)
	
	if file.file_exists("res://JsonFiles/CraftingRecipesTable.json"):
		var result = JSON.parse(file.get_as_text())
		file.close()
	
		if result.error:
			print("Failed to parse file " + result.error_string)
		else:
			load_result = result.result as Dictionary
		
func _ready() -> void:
	load_data()

func get_recipe_materials(name):
	for recipe in load_result:
		if recipe == name:
			return load_result[name].Materials
			
func get_recipes():
	return load_result
