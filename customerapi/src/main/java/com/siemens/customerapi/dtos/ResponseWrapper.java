package com.siemens.customerapi.dtos;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data

@NoArgsConstructor
public class ResponseWrapper<T> {
    private T object;
    private String message;

    public  ResponseWrapper(T object){
        this.object=object;
    }

    public ResponseWrapper(String message){
        this.message=message;
    }

}
