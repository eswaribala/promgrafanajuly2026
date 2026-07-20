package com.siemens.customerapi.models;

import jakarta.persistence.*;
import lombok.Data;

@Table(name="Address")
@Entity
@Data
public class Address {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    @Column(name="Address_Id")
    private long addressId;
    @Column(name = "Door_No",length = 4,nullable = false)
    private String doorNo;
    @Column(name = "Street_Name",length = 50,nullable = false)
    private String streetName;
    @Column(name = "City",length = 50,nullable = false)
    private String city;
    @Column(name = "State",length = 50,nullable = false)
    private String state;
    @ManyToOne(fetch = FetchType.LAZY,cascade = CascadeType.ALL)
    @JoinColumn(foreignKey = @ForeignKey(name = "Customer_Id"),
            name = "Customer_Id_FK")
    private Customer customer;
}
