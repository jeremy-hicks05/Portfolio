import jwt

data = {'payload': 'data', 'id': 123456789}

token = jwt.encode(data, 'secret-key')
algs = ['HS256', 'HS512']

data_out = jwt.decode(token, 'secret-key', algorithms=algs)
print(token)
print(data_out)
