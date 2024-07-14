# run
- `kubectl apply -f .\nginx-depl.yaml`
- `kubectl apply -f .\nginx-service.yaml`
- `kubectl describe service nginx-service`

You will see at the Endpoinds that service assigned 2 endpoinds at the same port (eg. 10.1.0.117:8080, 10.1.0.118:8080 )
It happens because we are running 2 pods