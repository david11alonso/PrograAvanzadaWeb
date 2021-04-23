export KUBECONFIG=PrograAvanzadaWeb-kubeconfig.yaml
docker build -t david11alonso/prograavanzadaweb:backend .
docker push david11alonso/prograavanzadaweb:backend

cd YAMLFILES/
