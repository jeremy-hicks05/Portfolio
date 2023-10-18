# oop/super.explicit.py
class Book:
    def __init__(self, title, publisher, pages):
        self.title = title
        self.publisher = publisher
        self.pages = pages
        
class Ebook(Book):
    def __init__(self, title, publisher, pages, format_):
        super().__init__(self, title, publisher, pages)
        self.format_ = format_
        
ebook = Ebook(
    'Learn Python Programming', 'Packt Publishing', 500, 'PDF')
print(ebook.title)
print(ebook.publisher)
print(ebook.pages)
print(ebook.format_)