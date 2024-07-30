# The process is like this:
1. `kubectl apply -f .\mongo-secret.yaml` to store the secrets
1. `kubectl apply -f .\mongo.yaml` to apply service and deployment
1. `kubectl apply -f .\mongo-configmap.yaml` to store the mongodb connection string
1. `kubectl apply -f .\shoppingapi.yaml`
1. `kubectl apply -f .\shoppingapi-configmap.yaml`
1. `kubectl apply -f .\shoppingclient.yaml`
