﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

public class CustomerController : ApiController
{
    online_tshirt_designingEntities designEntity;

    //GET api/<controller/id
    // [Route("api/customer/GetCustomerEntireData/{Id}")]
    [HttpGet]
    public IHttpActionResult GetCustomer(string id)
    {
        designEntity = new online_tshirt_designingEntities();
        
        //If A customer has got details along with Shipping details
        //Return this customerEntireData
        var customerEntireData = from cust in designEntity.customers
                                 join custAddr in designEntity.customer_address
                                 on cust.CustId equals custAddr.CustId
                                 where cust.CustId == id
                                 select new
                                 {
                                     cust.CustFirstName,
                                     cust.CustLastName,
                                     cust.CustMobNo,
                                     cust.CustEmailAddr,
                                     cust.CustImg,

                                     custAddr.CustAddrId,
                                     custAddr.CustShipAddr,
                                     custAddr.CustShipCity,
                                     custAddr.CustShipState,
                                     custAddr.CustShipPinCode,
                                     CustomerAddrrStatus = true
                                 };

        //If customer hasnt got any Shipping Details in 
        //Return customerData 
        if (customerEntireData.Count() == 0)
        {
            var customerData = from cust in designEntity.customers
                               where cust.CustId == id
                               select new
                               {
                                   cust.CustId,
                                   cust.CustFirstName,
                                   cust.CustLastName,
                                   cust.CustMobNo,
                                   cust.CustEmailAddr,
                                   cust.CustImg,
                                   CustomerAddrrStatus = false
                               };

            return Ok(customerData);
        }


        return Ok(customerEntireData);
    }

    [HttpPost]
    public IHttpActionResult Login([FromBody] Tuple<string, string> tupleCustomer)
    {
        //Instantiate the object
        designEntity = new online_tshirt_designingEntities();

        //var matchesLogin = from log  in designEntity.customers
        //                   where log.CustEmailAddr == tupleCustomer.Item1 && log.CustPwd == tupleCustomer.Item2
        //                   select new {

        //                       Id = log.CustId
        //                   };

        var matchesLogin = designEntity.customers.Where((log) => log.CustEmailAddr == tupleCustomer.Item1 && log.CustPwd == tupleCustomer.Item2).Select(log => log.CustId.ToString()).FirstOrDefault();

        //IF the Customer is not Registred, return the Message 
        if (matchesLogin == null) return Ok<string>("Login info is incorrect, Please click on Signup");

        //If the customer is registred, return Customer Id 
        return Ok(matchesLogin);
       
    }
    // POST api/<controller>
    [HttpPost]
    public IHttpActionResult SignUp([FromBody] customer theCustomer)
    {
        //Instantiate the obj 1st
        designEntity = new online_tshirt_designingEntities();

        //To generate random numbers for HomeBaneerImgId column
        DateTime dTime = DateTime.Now;


        //Set Unique id to CustId & CustAddrId column
        string id = Convert.ToString(dTime.Millisecond);

        int updatedRecord = 0; //Will update if SaveChanges() updates record successfully

        //Check if Customer has already an EmailID or not  
        var customerPresent = designEntity.customers.FirstOrDefault((c) => c.CustEmailAddr == theCustomer.CustEmailAddr);

        //FirstOrDefault returns null if matching record is not found
        if (customerPresent == null) return NotFound();

        //If not, Insert the details into Database i.e(Entity Database) 
        var newCustomer = new customer()
        {

            //Add properties to Class
            CustId = id,

            CustFirstName = theCustomer.CustFirstName,

            CustLastName = theCustomer.CustLastName,

            CustPwd = theCustomer.CustPwd,

            CustMobNo = Convert.ToSByte(theCustomer.CustMobNo),

            CustEmailAddr = theCustomer.CustEmailAddr,

            CustImg = theCustomer.CustImg
        };
        // customer x = customer.CreateProduct();


        try
        {
            //Finally commit the changes the changes and insert the record
            //In the database
            designEntity.customers.Add(newCustomer);

            updatedRecord = designEntity.SaveChanges();

        }
        catch (Exception error)
        {

            System.Diagnostics.Debug.WriteLine("Error in Linq", error);
        }

        //IF records get updated successfully
        if (updatedRecord > 0)
        {
            //Mail info to client
         
            //Instantiate the customer obj i.e CustomerExtension
            customer email = new customer();

            //Call the method from CustomerExtension.cs along with the parameters
            email.SendSignUpMail( new Tuple<string, string>(theCustomer.CustFirstName + " "+ theCustomer.CustLastName, theCustomer.CustEmailAddr) );
        }

        return Ok(newCustomer.CustId);
    }

    [HttpPost]
    public IHttpActionResult SaveCustomerAddress([FromBody] customer_address customerAddrr)
    {
        //To generate random numbers for HomeBaneerImgId column
        DateTime dTime = DateTime.Now;


        //Set Unique id to CustId & CustAddrId column
        string id = Convert.ToString(dTime.Millisecond);

        int updatedRecord = 0;

        //Insert the details into database i.e(EntityDatabase)
        var newCustomerAddrr = new customer_address {

            //Add properties to the class
            CustAddrId = id,

            CustShipAddr = customerAddrr.CustShipAddr,

            CustShipCountry = customerAddrr.CustShipCountry,

            CustShipState = customerAddrr.CustShipState,

            CustShipCity = customerAddrr.CustShipCity,

            CustShipPinCode = customerAddrr.CustShipPinCode
        };

        try
        {
            //Finally commit the changes the changes and insert the record
            //In the database
            designEntity.customer_address.Add(newCustomerAddrr);

            updatedRecord = designEntity.SaveChanges();

        }
        catch (Exception error)
        {

            System.Diagnostics.Debug.WriteLine("Error in Linq", error);
        }

        //IF records get updated successfully
        //if (updatedRecord > 0)
        //{

        //    return Ok();
        //}

        return Ok();
    }
    
    [HttpPost]
    public IHttpActionResult UpdateCustomerInfo([FromBody] string id, customer theCustomer){
        //Instantiate the object
        designEntity = new online_tshirt_designingEntities();

        int updatedRecord = 0;

        var updateCustoemerAddrr = from cust in designEntity.customers
                                   where cust.CustId == id
                                   select cust;

       
        //Execute the Query and return the Object
        customer custAddrrToUpdate = updateCustoemerAddrr.Single();

        //Change the Entity object
        custAddrrToUpdate.CustFirstName = theCustomer.CustFirstName;

        custAddrrToUpdate.CustLastName = theCustomer.CustLastName;

        custAddrrToUpdate.CustMobNo = theCustomer.CustMobNo;

        custAddrrToUpdate.CustEmailAddr = theCustomer.CustEmailAddr;

        //Commit the changes back to the database

        try
        {
          updatedRecord =  designEntity.SaveChanges();

        }
        catch (Exception error)
        {

            System.Diagnostics.Debug.WriteLine(error);
        }

        return Ok<string>("User info has been successfully updated");

    }

    [HttpPost]
    public IHttpActionResult UpdateCustomerAddress([FromBody] string id, customer_address customerAddrr)
    {
        //Instantiate the object
        designEntity = new online_tshirt_designingEntities();

        int updatedRecord = 0;

        var updateCustoemerAddrr = from cust in designEntity.customer_address
                                   where cust.CustAddrId == id
                                   orderby cust.CustShipAddr
                                   select cust;

        //Execute the Query and return the Object
        customer_address custAddrrToUpdate = updateCustoemerAddrr.Single();

        //Change the Entity object
        custAddrrToUpdate.CustShipAddr = customerAddrr.CustShipAddr;

        custAddrrToUpdate.CustShipCountry = customerAddrr.CustShipCountry;

        custAddrrToUpdate.CustShipState = customerAddrr.CustShipState;

        custAddrrToUpdate.CustShipCity = customerAddrr.CustShipCity;

        custAddrrToUpdate.CustShipPinCode = customerAddrr.CustShipPinCode;

        //Commit the changes back to the database
        try
        {
           updatedRecord =  designEntity.SaveChanges();

        }
        catch (Exception)
        {

            System.Diagnostics.Debug.WriteLine("Entity Update Error");
        } 

        return Ok<string>("Address updated successfully");
    }
    
    // PUT api/<controller>/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    public void DeleteCustomerAddress(string id)
    {
        //Instantiate the obj
        designEntity = new online_tshirt_designingEntities();

        //Find an appropriate Customer Address
        var matches = from c in designEntity.customer_address
                      where c.CustAddrId == id
                      orderby c.CustAddrId
                      select c;

        //Excute the query and return the Object
        customer_address customerAddrr = matches.Single();

        //Delete the record from the Database
        designEntity.customer_address.Remove(customerAddrr);

        designEntity.SaveChanges();
                      
    }


}
