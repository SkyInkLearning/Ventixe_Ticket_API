# Event Ticket Service API:

## Purpose and thinking:

This is the service backend where a ticket is created. The data comes in through a service bus from the ticket gateway and it has a API attached for the extraction of data. 


## Sequence diagram plantuml

<img src="https://github.com/user-attachments/assets/337fefe0-5c01-447a-a884-0e0db2e1e85c" width="400">

# Postman:

## Authentication:

All requests to this API require an API-Key to be passed in the header under "X-API-KEY". 

Invalid requests will be met with:

```json
{
    "success": false,
    "error": "Invalid api-key or api-key is missing."
}
```

## POST and PUT: 


```json


```


## GET:


```json
```

## DELETE


```json
```

### Created By:

https://github.com/SimonR-prog
