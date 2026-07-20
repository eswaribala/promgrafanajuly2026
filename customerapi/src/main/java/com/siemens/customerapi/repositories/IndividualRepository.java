package com.siemens.customerapi.repositories;

import com.siemens.customerapi.models.Individual;
import org.springframework.data.jpa.repository.JpaRepository;

public interface IndividualRepository extends JpaRepository<Individual,Long> {
}
