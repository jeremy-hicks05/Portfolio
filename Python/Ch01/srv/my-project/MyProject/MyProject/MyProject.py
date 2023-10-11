# bike.py
# let's define the class Bike
class Bike:
    
    def __init__(self, color, frame_material):
        self.color=color
        self.frame_material = frame_material
        
    def brake(self):
        print("Braking!")
        
# Let's create a couple of instances
red_bike = Bike('Red', 'Carbon fiber')
blue_bike = Bike('Blue', 'Steel')

# Let's inspect the objects we have, instance of the Bike class.
print(red_bike.color)
print(red_bike.frame_material)
print(blue_bike.color)
print(blue_bike.frame_material)

# let's brake!
red_bike.brake()
