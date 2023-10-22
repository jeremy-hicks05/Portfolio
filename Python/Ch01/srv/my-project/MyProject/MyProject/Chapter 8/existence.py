# files/existence.py
from pathlib import Path

p = Path('fear.txt')
path = p.parent.absolute()

print(p.is_file())
print(path)
print(path.is_dir())

q = Path('/Users/fab/srv/lpp3e/ch08/files')
print(q.is_dir())