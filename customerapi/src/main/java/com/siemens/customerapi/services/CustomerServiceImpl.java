package com.siemens.customerapi.services;

import com.siemens.customerapi.dtos.CustomerUpdateDTO;
import com.siemens.customerapi.models.Customer;
import com.siemens.customerapi.repositories.CustomerRepository;
import jakarta.persistence.EntityManager;
import jakarta.persistence.criteria.CriteriaBuilder;
import jakarta.persistence.criteria.CriteriaQuery;
import jakarta.persistence.criteria.Root;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import jakarta.persistence.TypedQuery;
import java.util.List;

@Service
public class CustomerServiceImpl implements CustomerService{
    @Autowired
    private CustomerRepository customerRepository;

    @Autowired
    private EntityManager entityManager;

    @Override
    public Customer addCustomer(Customer customer) {
        return this.customerRepository.save(customer);
    }

    @Override
    public List<Customer> fetchAllCustomers() {
        return this.customerRepository.findAll();
    }

    @Override
    public Customer fetchCustomerByAccountNo(long accountNo) {
        return this.customerRepository.findById(accountNo).orElse(null);
    }

    @Override
    public List<Customer> fetchCustomerByContactNo(long contactNo) {
        CriteriaBuilder cb = entityManager.getCriteriaBuilder();
         CriteriaQuery<Customer> cq= cb.createQuery(Customer.class);
         Root<Customer> root=cq.from(Customer.class);
        cq.where(cb.equal(root.get("contactNo"), contactNo));
        CriteriaQuery<Customer> result=cq.select(root);
        TypedQuery<Customer> typedQuery= entityManager.createQuery(result);
        return typedQuery.getResultList();
    }

    @Override
    public Customer updateCustomer(CustomerUpdateDTO customerUpdateDTO) {

        Customer customer=fetchCustomerByAccountNo(customerUpdateDTO.getAccountNo());
        if(customer != null){
            customer.setContactNo(customerUpdateDTO.getContactNo());
            customer.setEmail(customerUpdateDTO.getEmail());
            return this.customerRepository.save(customer);
        }
        else
           return null;
    }

    @Override
    public boolean deleteCustomerByAccountNo(long accountNo) {
        boolean status=false;
        Customer customer=fetchCustomerByAccountNo(accountNo);
        if(customer != null){
            this.customerRepository.deleteById(accountNo);
            status=true;
        }
        return status;
    }
}
