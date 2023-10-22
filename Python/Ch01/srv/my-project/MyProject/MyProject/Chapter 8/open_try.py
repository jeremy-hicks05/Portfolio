# files/open_try.py
fh = open('fear.txt') # r: read, t: text

try:
    for line in fh:
        print(line.strip()) # remove whitespace and print
finally:
    fh.close()
    