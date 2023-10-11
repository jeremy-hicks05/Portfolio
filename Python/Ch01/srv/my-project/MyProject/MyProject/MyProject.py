class Person:
        def __init(self, age):
                self.age = age
                
fab = Person()
fab.age = 42
print(fab.age)

print(id(fab))

print(id(fab.age))

fab.age = 25

print(id(fab))
print(id(fab.age))