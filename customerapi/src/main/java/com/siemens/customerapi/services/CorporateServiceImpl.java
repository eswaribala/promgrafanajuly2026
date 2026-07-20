package com.siemens.customerapi.services;

import com.siemens.customerapi.dtos.CustomerUpdateDTO;
import com.siemens.customerapi.models.Corporate;
import com.siemens.customerapi.models.Corporate;
import com.siemens.customerapi.models.Corporate;
import com.siemens.customerapi.models.Corporate;
import com.siemens.customerapi.repositories.CorporateRepository;
import jakarta.persistence.EntityManager;
import jakarta.persistence.TypedQuery;
import jakarta.persistence.criteria.CriteriaBuilder;
import jakarta.persistence.criteria.CriteriaQuery;
import jakarta.persistence.criteria.Root;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class CorporateServiceImpl implements  CorporateService{
    @Autowired
    private CorporateRepository corporateRepository;
    @Autowired
    private EntityManager entityManager;
    @Override
    public Corporate addCorporate(Corporate corporate) {
        return this.corporateRepository.save(corporate);
    }

    @Override
    public List<Corporate> fetchAllCorporates() {
        return this.corporateRepository.findAll();
    }

    @Override
    public Corporate fetchCorporateByAccountNo(long accountNo) {
        return this.corporateRepository.findById(accountNo).orElse(null);
    }

    @Override
    public List<Corporate> fetchCorporateByContactNo(long contactNo) {
        CriteriaBuilder cb = entityManager.getCriteriaBuilder();
        CriteriaQuery<Corporate> cq= cb.createQuery(Corporate.class);
        Root<Corporate> root=cq.from(Corporate.class);
        cq.where(cb.equal(root.get("contactNo"), contactNo));
        CriteriaQuery<Corporate> result=cq.select(root);
        TypedQuery<Corporate> typedQuery= entityManager.createQuery(result);
        return typedQuery.getResultList();
    }

    @Override
    public Corporate updateCorporate(CustomerUpdateDTO customerUpdateDTO) {
        Corporate corporate=fetchCorporateByAccountNo(customerUpdateDTO.getAccountNo());
        if(corporate != null){
            corporate.setContactNo(customerUpdateDTO.getContactNo());
            corporate.setEmail(customerUpdateDTO.getEmail());
            return this.corporateRepository.save(corporate);
        }
        else
            return null;
    }

    @Override
    public boolean deleteCorporateByAccountNo(long accountNo) {
        boolean status=false;
        Corporate corporate =fetchCorporateByAccountNo(accountNo);
        if(corporate != null){
            this.corporateRepository.deleteById(accountNo);
            status=true;
        }
        return status;
    }
}
