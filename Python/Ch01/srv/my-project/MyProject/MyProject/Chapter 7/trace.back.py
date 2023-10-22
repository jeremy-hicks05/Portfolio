# exception/trace.back.py
def squareroot(number):
    if number < 0:
        raise ValueError("No negative numbers please")
    
def quadratic(a, b, c):
    d = b ** 2 - 4 * a * c
    return ((-b - squareroot(d)) / (2 * a),
            (-b + squareroot(d)) / (2 * a))

quadratic(1, 0, 1)