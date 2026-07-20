package com.siemens.customerapi.models;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.NoArgsConstructor;
import lombok.experimental.SuperBuilder;

@Entity
@Table(name="Corporate")
@AllArgsConstructor
@NoArgsConstructor
@Data
@EqualsAndHashCode(callSuper = false)
@SuperBuilder
public class Corporate extends  Customer{
    @Enumerated(EnumType.STRING)
    @Column(name = "Company_Type")
    private CompanyType companyType;

}
