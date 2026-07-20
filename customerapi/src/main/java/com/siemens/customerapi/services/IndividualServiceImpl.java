package com.siemens.customerapi.services;

import com.siemens.customerapi.dtos.CustomerUpdateDTO;
import com.siemens.customerapi.models.Customer;
import com.siemens.customerapi.models.Individual;
import com.siemens.customerapi.repositories.IndividualRepository;
import jakarta.persistence.EntityManager;
import jakarta.persistence.TypedQuery;
import jakarta.persistence.criteria.CriteriaBuilder;
import jakarta.persistence.criteria.CriteriaQuery;
import jakarta.persistence.criteria.Root;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
@Service
public class IndividualServiceImpl implements IndividualService{
    @Autowired
    private IndividualRepository individualRepository;
    @Autowired
    private EntityManager entityManager;
    @Override
    public Individual addIndividual(Individual individual) {
        return this.individualRepository.save(individual);
    }

    @Override
    public List<Individual> fetchAllIndividuals() {
        return this.individualRepository.findAll();
    }

    @Override
    public Individual fetchIndividualByAccountNo(long accountNo) {
        return this.individualRepository.findById(accountNo).orElse(null);
    }

    @Override
    public List<Individual> fetchIndividualByContactNo(long contactNo) {
        CriteriaBuilder cb = entityManager.getCriteriaBuilder();
        CriteriaQuery<Individual> cq= cb.createQuery(Individual.class);
        Root<Individual> root=cq.from(Individual.class);
        cq.where(cb.equal(root.get("contactNo"), contactNo));
        CriteriaQuery<Individual> result=cq.select(root);
        TypedQuery<Individual> typedQuery= entityManager.createQuery(result);
        return typedQuery.getResultList();
    }

    @Override
    public Individual updateIndividual(CustomerUpdateDTO customerUpdateDTO) {
        Individual individual=fetchIndividualByAccountNo(customerUpdateDTO.getAccountNo());
        if(individual != null){
            individual.setContactNo(customerUpdateDTO.getContactNo());
            individual.setEmail(customerUpdateDTO.getEmail());
            return this.individualRepository.save(individual);
        }
        else
            return null;
    }

    @Override
    public boolean deleteIndividualByAccountNo(long accountNo) {
        boolean status=false;
        Individual individual =fetchIndividualByAccountNo(accountNo);
        if(individual != null){
            this.individualRepository.deleteById(accountNo);
            status=true;
        }
        return status;
    }
}
