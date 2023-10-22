from functools import cache, cached_property

class Client:
    def __init__(self):
        print("setting up the client...")
        
    def query(self, **kwargs):
        print(f"Performing a query: {kwargs}")
        
# class Manager:
#     @property
#     def client(self):
#         return Client()
    
#     def perform_query(self, **kwargs):
#         return self.client.query(**kwargs)

# manager = Manager()
# manager.perform_query()
# manager.perform_query()
# manager.perform_query()
# manager.perform_query()
# manager.perform_query()

# class ManualCacheManager:
#     @property
#     def client(self):
#         if not hasattr(self, '_client'):
#             self._client = Client()
#         return self._client

class CachedPropertyManager:
    @cached_property
    def client(self):
        return Client()
    
    def perform_query(self, **kwargs):
        return self.client.query(**kwargs)
    
manager = CachedPropertyManager()
manager.perform_query(object_id=42)
manager.perform_query(name_ilike='%python%')
del manager.client
manager.perform_query(age_gte=18)