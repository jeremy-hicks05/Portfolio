# context/decimal.prec.py
from decimal import Context, Decimal, getcontext, setcontext, localcontext

one = Decimal("1")
three = Decimal("3")

with localcontext(Context(prec = 5)) as ctx:
    print(ctx)
    print(one / three)
print(one / three)