package com.siemens.customerapi.repositories;

import com.siemens.customerapi.models.Customer;
import org.springframework.data.jpa.repository.JpaRepository;

public interface CustomerRepository extends JpaRepository<Customer,Long> {
}
