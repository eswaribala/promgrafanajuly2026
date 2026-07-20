package com.siemens.customerapi.controllers;
import com.siemens.customerapi.models.Customer;
import com.siemens.customerapi.services.CustomerService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PostMapping;

@Controller
public class CustomerFormController {
    @Autowired
    private CustomerService customerService;
    @GetMapping("/home")
    public String load(Model model){
        model.addAttribute("customer", new Customer());
        return "home.html";
    }
    @PostMapping("/save")
    public String save(@ModelAttribute("customer") Customer customer, Model model){
        System.out.println(customer.getName().getFirstName());
        Customer customerObj=null;
        if(customer.getName().getFirstName()!=null){
            customerObj=this.customerService.addCustomer(customer);
            model.addAttribute("customerResponse",customerObj);
            return "showcustomer.html";
        }else{
            return "redirect:/home";
        }


    }
    @GetMapping("/showall")
    public String showAllCustomers(Model model){
       model.addAttribute("customers", this.customerService.fetchAllCustomers());
       return "viewcustomers.html";

    }


}
