# oop/multiple.inheritance.py
class Shape:
    geometric_type = 'Generic Shape'
    def area(self): # this acts as a placeholder for the interface
        raise NotImplementedError
    def get_geometric_type(self):
        return self.geometric_type
    
class Plotter:
    def plot(self, ratio, topleft):
        # Imagine some nice plotting logic here...
        print('Plotting at {}, ratio {},'.format(
            topleft, ratio))
        

class Polygon(Shape, Plotter): # base class for polygons
    geometric_type = 'Polygon'
    
class RegularPolygon(Polygon): # Is-A Polygon
    geometric_type = 'Regular Polygon'
    def __init__(self, side):
        self.side = side

class RegularHexagon(RegularPolygon): # Is-A RegularPolygon
    geometric_type = 'RegularHexagon'
    def area(self):
        return 1.5 * (3 ** .5 * self.side ** 2)
    
class Square(RegularPolygon): # IsA RegularPolygon
    geometric_type = 'Square'
    def area(self):
        return self.side * self.side
    
hexagon = RegularHexagon(10)
print(hexagon.area())
print(hexagon.get_geometric_type())
hexagon.plot(0.8, (75, 77))

square = Square(12)
print(square.area())
print(square.get_geometric_type())
square.plot(0.93, (74, 75))

polygon = RegularPolygon(5)
print(polygon.area())