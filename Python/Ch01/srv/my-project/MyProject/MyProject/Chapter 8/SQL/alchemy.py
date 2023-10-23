from base64 import a85decode
from alchemy_models import Person, Address, engine
from sqlalchemy.orm import sessionmaker

Session = sessionmaker(bind=engine)
session = Session()

anakin = Person(name='Anakin Skywalker', age=32)
obi1 = Person(name='Obi-Wan Kenobi', age=40)

obi1.addresses=[
    Address(email='ob1@example.com'),
    Address(email='wanwan@example.com')
    ]

anakin.addresses.append(Address(email='ani@example.com'))
anakin.addresses.append(Address(email='evil.dart@example.com'))
anakin.addresses.append(Address(email='vader@example.com'))

session.add(anakin)
session.add(obi1)
session.commit()

obi1 = session.query(Person).filter(
    Person.name.like('Obi%')
    ).first()
print(obi1, obi1.addresses)

anakin = session.query(Person).filter(
    Person.name=='Anakin Skywalker'
    ).first()

print(anakin, anakin.addresses)

anakin_id = anakin.id
del anakin

def display_info():
    # get all addresses first
    addresses = session.query(Address).all()
    
    # display results
    for address in addresses:
        print(f'{address.person.name} <{address.email}>')
    
    # display how many objects we have in total
    print('people: {}, addresses: {}'.format(
        session.query(Person).count(),
        session.query(Address).count()))
    

display_info()

anakin = session.query(Person).get(anakin_id)
session.delete(anakin)
session.commit()

display_info()