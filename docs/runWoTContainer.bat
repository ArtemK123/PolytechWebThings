docker run ^
  -d ^
  -p 8080:8080 ^
  -p 4443:4443 ^
  -e TZ=America/Los_Angeles ^
  -v /path/to/shared/data:/home/node/.webthings ^
  --log-opt max-size=1m ^
  --log-opt max-file=10 ^
  --name webthings-gateway ^
  webthingsio/gateway:latest