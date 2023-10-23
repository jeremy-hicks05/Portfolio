# io_examples/req_post.py
import requests

# urls = {
#     "get": "https://httpbin.org/get?t=learn+python+programming",
#     "headers": "https://httpbin.org/headers",
#     "ip": "https://httpbin.org/ip",
#     "user-agent": "https://httpbin.org/user-agent",
#     "UUID": "https://httpbin.org/uuid",
#     "JSON": "https://httpbin.org/json"
#     }

# def get_content(title, url):
#     resp = requests.get(url)
#     print(f"Response for {title}")
#     print(resp.json())
    
# for title, url in urls.items():
#     get_content(title, url)
#     print("-" * 49)
    
url = 'https://httpbin.org/post'
data = dict(title='Learn Ptyhon Programming')

resp = requests.post(url, data=data)
print('Response for POST')
print(resp.json())