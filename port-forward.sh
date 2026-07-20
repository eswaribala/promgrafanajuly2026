kubectl port-forward -n prometheus svc/prometheus-server 9090:80 &
kubectl port-forward -n prometheus svc/prometheus-prometheus-pushgateway 9091:9091 &
kubectl port-forward -n prometheus svc/prometheus-prometheus-node-exporter 9100:9100 &
kubectl port-forward -n prometheus svc/prometheus-alertmanager 9093:9093 &
