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

## Supported Endpoints: PostController

### Create Post
`POST /post`

**Request Body:**
- `CreatePostCommand` (JSON or XML)

**Response Codes:**
| Status Code | Description                |
|-------------|----------------------------|
| 201         | Created - Post created     |
| 500         | Internal Server Error      |

---

### Get Post by Id
`GET /post/{id}`

**Query Parameters:**
- `includeAuthor` (bool, optional)

**Response Codes:**
| Status Code | Description                |
|-------------|----------------------------|
| 200         | OK - Post found            |
| 404         | Not Found                  |
