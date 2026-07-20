package com.siemens.customerapi.dtos;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class CustomerUpdateDTO {
    private long accountNo;
    private long contactNo;
    private String email;
}
