package com.siemens.scheduledjobapp;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.scheduling.annotation.EnableScheduling;

@SpringBootApplication
@EnableScheduling  // Enable Spring Boot scheduling
public class ScheduledJobAppApplication {

    public static void main(String[] args) {
        SpringApplication.run(ScheduledJobAppApplication.class, args);
    }

}
