package com.siemens.customerapi.dtos;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class AddressUpdateDTO {
    private String doorNo;
    private String streetName;
    private String city;
    private String state;
    private long addressId;

}
