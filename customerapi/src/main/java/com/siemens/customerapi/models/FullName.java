package com.siemens.customerapi.models;

import jakarta.persistence.Column;
import jakarta.persistence.Embeddable;
import lombok.Data;

@Embeddable
@Data
public class FullName {
    @Column(name = "First_Name",length = 50,nullable = false)
    private String firstName;
    @Column(name = "Middle_Name",length = 50,nullable = true)
    private String middleName;
    @Column(name = "Last_Name",length = 50,nullable = false)
    private String lastName;

}
