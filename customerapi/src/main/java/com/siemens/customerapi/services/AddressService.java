package com.siemens.customerapi.services;

import com.siemens.customerapi.dtos.AddressUpdateDTO;
import com.siemens.customerapi.dtos.CustomerUpdateDTO;
import com.siemens.customerapi.models.Address;

import java.util.List;

public interface AddressService {
    Address addAddress(Address address, long accountNo);
    List<Address> fetchAllAddresses();
    List<Address> fetchAddressByAccountNo(long accountNo);

    Address updateAddress(AddressUpdateDTO addressUpdateDTO);
    boolean deleteAddressByAccountNo(long addressId);
}
