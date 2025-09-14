# Blogging System Web API


## Running with Docker Compose

To build and run the application using Docker Compose, use the following commands in your terminal:

### Start the application
```
docker compose up --build
```
This command will build the Docker images (if needed) and start all services defined in the `docker-compose.yml` file.

**Note:** As part of the Docker build process, all tests are executed automatically. If any test fails, the build will stop and the application will not be started.

### Stop the application
```
docker-compose down
```
This command will stop and remove all containers defined in the `docker-compose.yml` file.

---

Make sure you have [Docker](https://www.docker.com/products/docker-desktop/) installed and running on your machine before executing these commands.

---

## WebAPI Documentation

### Common Response Codes

| Status Code | Description                |
|-------------|----------------------------|
| 200         | OK - Successful request    |
| 201         | Created - Resource created |
| 204         | No Content                 |
| 400         | Bad Request                |
| 401         | Unauthorized               |
| 404         | Not Found                  |
| 500         | Internal Server Error      |

The API follows RESTful conventions. All endpoints return standard HTTP status codes to indicate the result of the operation.
