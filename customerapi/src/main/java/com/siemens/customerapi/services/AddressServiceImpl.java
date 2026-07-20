package com.siemens.customerapi.services;

import com.siemens.customerapi.dtos.AddressUpdateDTO;
import com.siemens.customerapi.models.*;
import com.siemens.customerapi.models.Address;
import com.siemens.customerapi.repositories.AddressRepository;
import com.siemens.customerapi.repositories.CustomerRepository;
import jakarta.persistence.EntityManager;
import jakarta.persistence.TypedQuery;
import jakarta.persistence.criteria.CriteriaBuilder;
import jakarta.persistence.criteria.CriteriaQuery;
import jakarta.persistence.criteria.Root;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
@Service
public class AddressServiceImpl implements AddressService{
    @Autowired
    private CustomerRepository customerRepository;
    @Autowired
    private AddressRepository addressRepository;
    @Autowired
    private EntityManager entityManager;
    @Override
    public Address addAddress(Address address, long accountNo) {

        Customer customer= this.customerRepository.findById(accountNo).orElse(null);
        if(customer!=null){

            address.setCustomer(customer);
            return this.addressRepository.save(address);
        }else
        return null;
    }

    @Override
    public List<Address> fetchAllAddresses() {
        return this.addressRepository.findAll();
    }

    @Override
    public List<Address> fetchAddressByAccountNo(long accountNo) {
        Customer customer= this.customerRepository.findById(accountNo).orElse(null);
        if(customer!=null) {
            CriteriaBuilder cb = entityManager.getCriteriaBuilder();
            CriteriaQuery<Address> cq = cb.createQuery(Address.class);
            Root<Address> root = cq.from(Address.class);
            cq.where(cb.equal(root.get("customer"), customer));
            CriteriaQuery<Address> result = cq.select(root);
            TypedQuery<Address> typedQuery = entityManager.createQuery(result);
            return typedQuery.getResultList();
        }else
            return null;
    }



    @Override
    public Address updateAddress(AddressUpdateDTO addressUpdateDTO) {


        Address address=this.addressRepository.findById(addressUpdateDTO.getAddressId()).orElse(null);
        if(address != null){
            address.setCity(addressUpdateDTO.getCity());
            address.setState(addressUpdateDTO.getState());
            address.setStreetName(addressUpdateDTO.getStreetName());
            address.setDoorNo(addressUpdateDTO.getDoorNo());
            return this.addressRepository.save(address);
        }
        else
            return null;
    }

    @Override
    public boolean deleteAddressByAccountNo(long addressId) {
        boolean status=false;
        Address address =this.addressRepository.findById(addressId).orElse(null);
        if(address != null){
            this.addressRepository.deleteById(addressId);
            status=true;
        }
        return status;
    }
}
