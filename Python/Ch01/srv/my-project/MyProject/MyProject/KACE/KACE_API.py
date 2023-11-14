# KACE_API.py
import json
import requests

from flask import jsonify
from flask_cors import CORS, cross_origin
from fastapi.middleware.cors import CORSMiddleware

from fastapi import FastAPI

app = FastAPI()

# CORS(app)
# cors = CORS(app, resource={
#     r"/*":{
#         "origins": "*"}})

origins = ["*"]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"]
    )

@app.get("/fields")
def hello():

    # # Start future API

    username = 'admin'
    password = 'Flint48503'
    org_name = 'Default'

    session = requests.session()
    session.params = {'password': password,
                        'userName': username,
                        'organizationName': org_name}
    session.headers = {'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'x-dell-api-version': '13'}

    login_path = 'http://192.168.122.33/ams/shared/api/security/login'

    login = session.post(
        login_path, 
        headers=session.headers, 
        data=json.dumps(session.params))

    cookie = login.cookies

    # get_info = 'http://192.168.122.33/api/service_desk/queues/1/'
    get_info = 'http://192.168.122.33/api/service_desk/queues/1/fields'

    session3 = requests.session()
    session3.headers = {'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'x-dell-api-version': '13'}

    info = session3.get(
        get_info,
        headers=session3.headers, 
        cookies=cookie)
    print(info.content)
    return info.content

hello()

# username = 'admin'
# password = 'Flint48503'
# org_name = 'Default'

# session = requests.session()
# session.params = {'password': password,
#                     'userName': username,
#                     'organizationName': org_name}
# session.headers = {'Accept': 'application/json',
#                     'Content-Type': 'application/json',
#                     'x-dell-api-version': '13'}

# login_path = 'http://192.168.122.33/ams/shared/api/security/login'

# login = session.post(login_path, headers=session.headers, data=json.dumps(session.params))

# cookie = login.cookies

# print(cookie)

# # Start future API

# get_info = 'http://192.168.122.33/api/service_desk/queues/1/fields'

# session3 = requests.session()
# session3.headers = {'Accept': 'application/json',
#                     'Content-Type': 'application/json',
#                     'x-dell-api-version': '13'}

# info = session3.get(
#     get_info, 
#     headers=session3.headers, 
#     cookies=cookie)

# # return info

# # End future API

# print(info)
# print(info.json())

# get_tickets = 'http://192.168.122.33/api/service_desk/tickets'

# session3 = requests.session()
# session3.headers = {'Accept': 'application/json',
#                     'Content-Type': 'application/json',
#                     'x-dell-api-version': '13'}
# session3.params = {
#     'Tickets':
#     [
#         {
#             'title': 'API Test',
#             'owner': {'id': 79},
#             'hd_queue_id': 1,
#             'submitter': {'id': 79},
#             'summary': 'API Test',
#             'category': 1,
#             'status': 3
#         }
#     ]
# }

# # ticket = session3.post(
# #     get_tickets, 
# #     headers=session3.headers, 
# #     data=json.dumps(session3.params),
# #     cookies=cookie)

# # print(ticket)
# # print(ticket.json())
