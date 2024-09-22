# How to deploy

1. az acr create --resource-group 240922aks --name 240922shoppingacr --sku Basic
1. az acr login --name 240922shoppingacr
1. az aks create --resource-group 240922aks --name 240922myAKSCluster --node-count 1 --generate-ssh-keys --attach-acr 240922shoppingacr
1. az aks get-credentials --resource-group 240922aks --name 240922myAKSCluster
1. Try `kubectl get all`. It should return only ClusterIP
1. `kubectl apply -f .\manifests\`
1. Azure DevOps -> New Pipeline -> One for api and one for client

it is not that simple, it will bring a lot of errors! debuggins is mandatory