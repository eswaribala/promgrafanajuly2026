package com.siemens.customerapi.repositories;

import com.siemens.customerapi.models.Corporate;
import org.springframework.data.jpa.repository.JpaRepository;

public interface CorporateRepository extends JpaRepository<Corporate,Long> {
}
