package com.siemens.customerapi.controllers;

import com.siemens.customerapi.dtos.CustomerUpdateDTO;
import com.siemens.customerapi.dtos.ResponseWrapper;
import com.siemens.customerapi.models.Customer;
import com.siemens.customerapi.services.CustomerService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;

import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/customers")
public class CustomerController {
    @Autowired
    private CustomerService customerService;

    //create customer

    @PostMapping("/v1.0")
    public ResponseEntity<ResponseWrapper> createCustomer(@RequestBody Customer customer){
        Customer customerObj=this.customerService.addCustomer(customer);
        if(customerObj!=null){
            return ResponseEntity.status(201).body(new ResponseWrapper(customerObj));
        }else{
            return ResponseEntity.status(400).body(new ResponseWrapper("Customer could not be created"));
        }

    }

    @GetMapping(path = "/v1.0",produces ={MediaType.APPLICATION_JSON_VALUE,
    MediaType.APPLICATION_XML_VALUE
    })

    public List<Customer> getAllCustomers(){
        return this.customerService.fetchAllCustomers();
    }

    @GetMapping("/v1.0/{accountNo}")
    public ResponseEntity<ResponseWrapper> getCustomerByAccountNo(@PathVariable("accountNo") long accountNo){
        Customer customer= this.customerService.fetchCustomerByAccountNo(accountNo);
        if(customer!=null){
            return ResponseEntity.status(200).body(new ResponseWrapper(customer));
        }else{
            return ResponseEntity.status(500).body(new ResponseWrapper("Customer not available"));
        }

    }

    @GetMapping("/v1.0/search/{contactNo}")
    public ResponseEntity<ResponseWrapper> getCustomerByContactNo(@PathVariable("contactNo") long contactNo){
        List<Customer> customers= this.customerService.fetchCustomerByContactNo(contactNo);
        if(customers.size()>0){
            return ResponseEntity.status(200).body(new ResponseWrapper(customers));
        }else{
            return ResponseEntity.status(500).body(new ResponseWrapper("Customer not available"));
        }

    }

    @PutMapping("/v1.0")
    public ResponseEntity<ResponseWrapper> updateCustomer(@RequestBody CustomerUpdateDTO customerUpdateDTO){
        Customer customerObj=this.customerService.updateCustomer(customerUpdateDTO);
        if(customerObj!=null){
            return ResponseEntity.status(201).body(new ResponseWrapper(customerObj));
        }else{
            return ResponseEntity.status(400).body(new ResponseWrapper("Customer could not be created"));
        }

    }

    @DeleteMapping("/v1.0/{accountNo}")
    public ResponseEntity<ResponseWrapper> deleteCustomerByAccountNo(@PathVariable("accountNo") long accountNo){

        if(this.customerService.deleteCustomerByAccountNo(accountNo)){
            return ResponseEntity.status(200).body(new ResponseWrapper("Customer Deleted"));
        }else{
            return ResponseEntity.status(500).body(new ResponseWrapper("Customer could not be deleted"));
        }

    }



}
