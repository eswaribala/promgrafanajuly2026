package com.siemens.scheduledjobapp.services;

import io.prometheus.client.CollectorRegistry;
import io.prometheus.client.Gauge;
import io.prometheus.client.exporter.PushGateway;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
import java.util.Random;

@Component
public class MetricPushService {

    // Create a registry to register your custom metrics
    private final CollectorRegistry registry = new CollectorRegistry();

    // Define a gauge metric to record the duration of a job
    private final Gauge jobDurationGauge = Gauge.build()
            .name("job_duration_seconds")
            .help("Duration of the job in seconds.")
            .register(registry);

    private final PushGateway pushGateway = new PushGateway("localhost:9091");  // Pointing to the Pushgateway

    // This method simulates a job that runs every 15 seconds
    @Scheduled(cron = "0 * * * * *")
    public void pushMetrics() {
        // Record the start time
        long startTime = System.currentTimeMillis();

        // Simulate a job (you can replace this with actual logic)
        performJob();

        // Record the end time and calculate the job duration
        long duration = (System.currentTimeMillis() - startTime) / 1000;
        jobDurationGauge.set(duration);  // Set the gauge with job duration

        // Labels for the job (you can add additional labels as needed)
        Map<String, String> groupingKey = new HashMap<>();
        groupingKey.put("job", "spring_boot_job"+new Random().nextInt(1,1000));

        try {
            // Push the metrics to the Prometheus Pushgateway
            pushGateway.pushAdd(registry, "spring_boot_job"+new Random().nextInt(1,1000), groupingKey);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void performJob() {
        // Simulate job execution (replace with actual logic)
        try {
            Thread.sleep(5000);  // Simulate a job that takes 5 seconds
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}
