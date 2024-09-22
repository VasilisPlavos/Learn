## Prepare az env

1. az login --use-device-code
2. az acr create --resource-group 240710aks --name 240909shoppingacr --sku Basic  
3. az acr login --name 240909shoppingacr
4. az aks create --resource-group 240710aks --name 240909myAKSCluster --node-count 1 --generate-ssh-keys --attach-acr 240909shoppingacr
5. az aks install-cli
6. az aks get-credentials --resource-group 240710aks --name 240909myAKSCluster


kubectl create secret docker-registry acr-secret --docker-server=shoppingacr.azurecr.io --docker-username=shoppingacr --docker-password=<containerregistryaccesskeypasswordhere> --docker-email=user@email.com


========== DELETE THE ABOVE =============

# The process is like this:
1. `kubectl apply -f .\mongo-secret.yaml` to store the secrets
1. `kubectl apply -f .\mongo.yaml` to apply service and deployment
1. `kubectl apply -f .\mongo-configmap.yaml` to store the mongodb connection string
1. `kubectl apply -f .\shoppingapi.yaml`
1. `kubectl apply -f .\shoppingapi-configmap.yaml`
1. `kubectl apply -f .\shoppingclient.yaml`

# To delete all of the resources you can do `kubectl apply -f .\k8s\` and it will delete all the resources described in all yaml files in this directory