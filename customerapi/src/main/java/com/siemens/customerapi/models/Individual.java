package com.siemens.customerapi.models;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.NoArgsConstructor;
import lombok.experimental.SuperBuilder;
import org.springframework.format.annotation.DateTimeFormat;

import java.time.LocalDate;

@Entity
@Table(name="Individual")
@AllArgsConstructor
@NoArgsConstructor
@Data
@EqualsAndHashCode(callSuper = false)
@SuperBuilder
public class Individual extends Customer{
   @Enumerated(EnumType.STRING)
   @Column(name="Gender")
   private Gender gender;
   @DateTimeFormat(iso = DateTimeFormat.ISO.DATE)
   @Column(name="DOB")
   private LocalDate dob;
}
