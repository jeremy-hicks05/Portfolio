# exceptions/multiple.py
values = (1, 2)

try:
    q, r = divmod(*values)
except(ZeroDivisionError, TypeError) as e:
    print(type(e), e)