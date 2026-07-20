package com.siemens.customerapi.services;

import com.siemens.customerapi.dtos.CustomerUpdateDTO;
import com.siemens.customerapi.models.Customer;

import java.util.List;

public interface CustomerService {
    Customer addCustomer(Customer customer);
    List<Customer> fetchAllCustomers();
    Customer fetchCustomerByAccountNo(long accountNo);
    List<Customer> fetchCustomerByContactNo(long contactNo);
    Customer updateCustomer(CustomerUpdateDTO customerUpdateDTO);
    boolean deleteCustomerByAccountNo(long accountNo);


}
