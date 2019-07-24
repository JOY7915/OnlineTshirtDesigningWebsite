﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAccount_Login : System.Web.UI.Page
{
    Customer cust;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ButtonLoginModal_Click(object sender, EventArgs e)
    {
        //If page controls aren't valid 
        //Dont proceed
        if (!Page.IsValid) return;


        cust = new Customer();

        cust.objSelection = SelectionChoice.Customer;

        
        if (cust.Login())
            Response.Redirect("Home.aspx");

        else Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.error('Enter right Creditional OR Click Sign Up', 'Error')", true);

    }

    protected void ButtonUserSignUp_Click(object sender, EventArgs e)
    {
        //To generate random numbers for HomeBaneerImgId column
        DateTime dTime = DateTime.Now;

        RequiredFieldValidator validatorEmail = (RequiredFieldValidator)(this.Page.FindControl("ValidatorEmailSignUp"));

        Label lblError = (Label)(this.Page.FindControl("LblError"));

       //Initialize the customer Obj
        cust = new Customer();

        //Set Unique id to customer
        cust.MyId = dTime.Millisecond;

        //Pass the validatorEmail and lblError Obj Reference to the class method SignUp()
        cust.SignUp(validatorEmail, lblError);
    }
}