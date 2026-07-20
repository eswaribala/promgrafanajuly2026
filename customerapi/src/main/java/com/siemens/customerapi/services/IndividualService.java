package com.siemens.customerapi.services;

import com.siemens.customerapi.dtos.CustomerUpdateDTO;
import com.siemens.customerapi.models.Individual;

import java.util.List;

public interface IndividualService {
    Individual addIndividual(Individual individual);
    List<Individual> fetchAllIndividuals();
    Individual fetchIndividualByAccountNo(long accountNo);
    List<Individual> fetchIndividualByContactNo(long contactNo);
    Individual updateIndividual(CustomerUpdateDTO customerUpdateDTO);
    boolean deleteIndividualByAccountNo(long accountNo);
}
