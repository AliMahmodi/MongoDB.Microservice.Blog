# MongoDB.Microservice.Blog
Asp Core 8.0 as API provider

MongoDB as DataBase provider -- Docker Desktop bitnami/mongodb:latest

First install docker Desktop

Then run this command:

docker run --name=MongoDB-Blogs  -p 27017:27017 -v /var/lib/docker/volumes/MongoBlogsVol/_data:/bitnami/mongodb/ --user 0  -d bitnami/mongodb:latest


