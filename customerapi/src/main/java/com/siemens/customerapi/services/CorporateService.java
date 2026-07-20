package com.siemens.customerapi.services;

import com.siemens.customerapi.dtos.CustomerUpdateDTO;
import com.siemens.customerapi.models.Corporate;

import java.util.List;

public interface CorporateService {
    Corporate addCorporate(Corporate corporate);
    List<Corporate> fetchAllCorporates();
    Corporate fetchCorporateByAccountNo(long accountNo);
    List<Corporate> fetchCorporateByContactNo(long contactNo);
    Corporate updateCorporate(CustomerUpdateDTO customerUpdateDTO);
    boolean deleteCorporateByAccountNo(long accountNo);
}
