﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAccount_UserAccount : System.Web.UI.Page
{
    protected Customer cust;

    protected ManipulateCustomerData custMpData;

    private Dictionary<string, string[]> ContStateCollection
    {
        get
        {
            //Retreive from ViewState
            object d = ViewState["ContryStateCollection"];

            //set the condition
            return (d == null ? null : (Dictionary<string, string[]>)d);
        }

        set
        {
            ViewState["ContryStateCollection"] = value;
        }
    }
    // protected Dictionary<string , string[]> stateSelection;

    private int id;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Display user info
        cust = new Customer();

        if (!this.IsPostBack)
        {
            //To set the Multiview property 
            LinkButtonUserProf_Click(LinkButtonUserProf, null);

            if (cust.CheckCookie())
            {

                ViewState["CustId"] = cust.MyId;

                cust.DisplayCustomerData();

                custMpData = cust.ShowCustManupulatedData();

                custMpData.ShowCustIdentity();

                ShowDisplayedData();

            }

            else Response.Redirect("~/UserAccount/Login.aspx");

            //This function will add the Country and state
            //In the dropdown List 
            AddCountryList();
        }

        if (this.IsPostBack)
        {
            //Retreive CustId 
            id = (int)ViewState["CustId"];

            //Retreive CustManupulated Obj 
            custMpData = (ManipulateCustomerData)ViewState["CustManupData"];
        }


    }

    private void AddCountryList()
    {
        //Add Country list

        if (ContStateCollection == null)
        {
            ContStateCollection = new Dictionary<string, string[]>();

            ContStateCollection.Add("India", new string[] { "Maharashtra", "Kerala", "Punjab" });

            ContStateCollection.Add("USA", new string[] { "California ", "Hawaii", "New York" });

        }

        //Set Dictionary key of country  into Dropdownlist
        foreach (string key in ContStateCollection.Keys)
        {
            DropDownListUserCountry.Items.Add(new ListItem(key));

            ListShipNewCountry.Items.Add(new ListItem(key));
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Persist Variable

        ContStateCollection = ContStateCollection;

        //Persist  CustManupulated Obj / Update the ViewState 
        ViewState["CustManupData"] = custMpData;

    }

    //For User profile Tab
    private void ShowDisplayedData()
    {

        LblUserProfEmail.Text = cust.EmailId;

        LblUserProfMob.Text = cust.CustMobNo.ToString();

        LblUserProfFullName.Text = cust.CustFullName;
    }

    protected void ButtonUserReturn_Click(object sender, EventArgs e)
    {
        //MultiViewUserOrdered.ActiveViewIndex = 1;

        //List<Table> returnedProductList = new List<Table>();
        //int productCatCount = 5;

        //Panel pannelUserOrderedProductInfo0; // which will create panel in td
        //Panel pannelUserOrderedProductInfo1;  // which will create panel in td
        //Panel pannelUserOrderedProductInfo2; // which will create panel in td
        //Image productImage;

        //Button userProductChoiceOrderdtl;
        //Button userProductChoiceOrderdInvoice;

        //Label labelUserOrderedProductName;

        //for (int i = 0; i < 2; i++)
        //{
        //    Table table = new Table();
        //    table.ID = "TableUserOrder" + i.ToString();
        //    table.CssClass = "table";

        //    TableHeaderRow tr = new TableHeaderRow();
        //    tr.CssClass = "relate";
        //    table.Controls.Add(tr);


        //    for (int thead = 0; thead < productCatCount; thead++)
        //    {
        //        /*<thead> <tr><th></th> </tr></thead>*/

        //        tr.TableSection = TableRowSection.TableHeader;
        //        TableHeaderCell th = new TableHeaderCell();
        //        th.Text = "Product (" + thead.ToString() + ")";
        //        th.CssClass = "align-top";
        //        tr.Cells.Add(th);
        //        table.Rows.Add(tr);
        //    }

        //    tr = new TableHeaderRow();
        //    table.Controls.Add(tr);

        //    tr.TableSection = TableRowSection.TableBody;
        //    TableCell td = new TableCell();

        //    pannelUserOrderedProductInfo0 = new Panel();
        //    pannelUserOrderedProductInfo0.ID = "PannelUserOrderedProductInfo";
        //    pannelUserOrderedProductInfo0.CssClass = "item-img display-cell align-top";
        //    productImage = new Image();
        //    productImage.ImageUrl = "../Images/Home/10.jpg";
        //    productImage.CssClass = "img-responsive";
        //    // 1st add image in td

        //    // add image to 1st pannel 
        //    pannelUserOrderedProductInfo0.Controls.Add(productImage);
        //    td.Controls.Add(pannelUserOrderedProductInfo0);

        //    TableCell td1 = new TableCell();
        //    pannelUserOrderedProductInfo1 = new Panel();
        //    pannelUserOrderedProductInfo1.CssClass = " item - desc display - cell align - top";

        //    labelUserOrderedProductName = new Label();
        //    labelUserOrderedProductName.Text = "Linen Full sleeve shirt" + "<br/>";

        //    Label labelUserOrderedProductCode = new Label();
        //    labelUserOrderedProductCode.CssClass = "item-code";
        //    labelUserOrderedProductCode.Text = "Product Code:" + 565666666.ToString() + "<br />";

        //    Label labelUserProductPrice = new Label();
        //    labelUserProductPrice.CssClass = "cost";
        //    labelUserProductPrice.Text = "&#8377; " + 4500.ToString();

        //    //add label to another pannel
        //    pannelUserOrderedProductInfo1.Controls.Add(labelUserOrderedProductName);
        //    pannelUserOrderedProductInfo1.Controls.Add(labelUserOrderedProductCode);
        //    pannelUserOrderedProductInfo1.Controls.Add(labelUserProductPrice);

        //    td1.Controls.Add(pannelUserOrderedProductInfo1);

        //    TableCell td2 = new TableCell();

        //    userProductChoiceOrderdtl = new Button();
        //    userProductChoiceOrderdtl.ID = "ButtonUserProductChoiceOrderDtl";
        //    userProductChoiceOrderdtl.CssClass = "btn order-action--btn text-uppercase";
        //    userProductChoiceOrderdtl.Text = "order detail";

        //    userProductChoiceOrderdInvoice = new Button();
        //    userProductChoiceOrderdInvoice.ID = "ButtonUserProductChoiceOrderInvoi";
        //    userProductChoiceOrderdInvoice.CssClass = "btn order-action--btn text-uppercase";
        //    userProductChoiceOrderdInvoice.Text = "Buy it again";
        //    userProductChoiceOrderdInvoice.OnClientClick = "UserProductByAgain_Click";

        //    pannelUserOrderedProductInfo2 = new Panel();
        //    pannelUserOrderedProductInfo2.Controls.Add(userProductChoiceOrderdtl);
        //    pannelUserOrderedProductInfo2.Controls.Add(userProductChoiceOrderdInvoice);
        //    td2.Controls.Add(pannelUserOrderedProductInfo2);

        //    //add all td to TableRow obj
        //    tr.Controls.Add(td);
        //    tr.Controls.Add(td1);
        //    tr.Controls.Add(td2);

        //    //for (int tbody = 0; tbody < 3; tbody++)
        //    //{
        //    //    /*<tbody><tr><td></tr>*/

        //    //    tr.TableSection = TableRowSection.TableBody;
        //    //    TableCell td = new TableCell();


        //    //    //tr.Cells.Add(td);
        //    //    // Put table cell in the TableRow
        //    //    tr.Controls.Add(td);

        //    //    // table.Rows.Add(tr);
        //    //}

        //    returnedProductList.Add(table);

        //}

        //foreach (Table item in returnedProductList)
        //{
        //    PanelUserOrderReturnTable.Controls.Add(item);
        //}

    }

    protected void ButtonUserCancelled_Click(object sender, EventArgs e)
    {

        MultiViewUserOrdered.ActiveViewIndex = 2;
        List<Table> cancelledProductList = new List<Table>();
        int productCatCount = 5;

        Panel pannelUserOrderedProductInfo0; // which will create panel in td
        Panel pannelUserOrderedProductInfo1;  // which will create panel in td
        Panel pannelUserOrderedProductInfo2; // which will create panel in td
        Image productImage;

        Button userProductChoiceOrderdInvoice;

        Label labelUserOrderedProductName;

        for (int i = 0; i < 2; i++)
        {
            Table table = new Table();
            table.ID = "TableUserOrder" + i.ToString();
            table.CssClass = "table";

            TableHeaderRow tr = new TableHeaderRow();
            tr.CssClass = "relate";
            table.Controls.Add(tr);


            for (int thead = 0; thead < productCatCount; thead++)
            {
                /*<thead> <tr><th></th> </tr></thead>*/

                tr.TableSection = TableRowSection.TableHeader;
                TableHeaderCell th = new TableHeaderCell();
                th.Text = "Product (" + thead.ToString() + ")";
                th.CssClass = "align-top";
                tr.Cells.Add(th);
                table.Rows.Add(tr);
            }

            tr = new TableHeaderRow();
            table.Controls.Add(tr);

            tr.TableSection = TableRowSection.TableBody;
            TableCell td = new TableCell();

            pannelUserOrderedProductInfo0 = new Panel();
            pannelUserOrderedProductInfo0.ID = "PannelUserOrderedProductInfo";
            pannelUserOrderedProductInfo0.CssClass = "item-img display-cell align-top";
            productImage = new Image();
            productImage.ImageUrl = "../Images/Home/10.jpg";
            productImage.CssClass = "img-responsive";
            // 1st add image in td

            // add image to 1st pannel 
            pannelUserOrderedProductInfo0.Controls.Add(productImage);
            td.Controls.Add(pannelUserOrderedProductInfo0);

            TableCell td1 = new TableCell();
            pannelUserOrderedProductInfo1 = new Panel();
            pannelUserOrderedProductInfo1.CssClass = " item - desc display - cell align - top";

            labelUserOrderedProductName = new Label();
            labelUserOrderedProductName.Text = "Linen Full sleeve shirt" + "<br/>";

            Label labelUserOrderedProductCode = new Label();
            labelUserOrderedProductCode.CssClass = "item-code";
            labelUserOrderedProductCode.Text = "Product Code:" + 565666666.ToString() + "<br />";

            Label labelUserProductPrice = new Label();
            labelUserProductPrice.CssClass = "cost";
            labelUserProductPrice.Text = "&#8377; " + 4500.ToString();

            //add label to another pannel
            pannelUserOrderedProductInfo1.Controls.Add(labelUserOrderedProductName);
            pannelUserOrderedProductInfo1.Controls.Add(labelUserOrderedProductCode);
            pannelUserOrderedProductInfo1.Controls.Add(labelUserProductPrice);

            td1.Controls.Add(pannelUserOrderedProductInfo1);

            TableCell td2 = new TableCell();

            userProductChoiceOrderdInvoice = new Button();
            userProductChoiceOrderdInvoice.ID = "ButtonUserProductChoiceOrderInvoi";
            userProductChoiceOrderdInvoice.CssClass = "btn order-action--btn text-uppercase";
            userProductChoiceOrderdInvoice.Text = "Buy it again";
            userProductChoiceOrderdInvoice.OnClientClick = "UserProductByAgain_Click";

            pannelUserOrderedProductInfo2 = new Panel();
            pannelUserOrderedProductInfo2.Controls.Add(userProductChoiceOrderdInvoice);
            td2.Controls.Add(pannelUserOrderedProductInfo2);

            //add all td to TableRow obj
            tr.Controls.Add(td);
            tr.Controls.Add(td1);
            tr.Controls.Add(td2);

            //for (int tbody = 0; tbody < 3; tbody++)
            //{
            //    /*<tbody><tr><td></tr>*/

            //    tr.TableSection = TableRowSection.TableBody;
            //    TableCell td = new TableCell();


            //    //tr.Cells.Add(td);
            //    // Put table cell in the TableRow
            //    tr.Controls.Add(td);

            //    // table.Rows.Add(tr);
            //}

            cancelledProductList.Add(table);

        }

        foreach (Table item in cancelledProductList)
        {
            PanelUserOrderCancelTable.Controls.Add(item);
        }
    }

    protected void ButtonUserOrders_Click(object sender, EventArgs e)
    {
        MultiViewUserOrdered.ActiveViewIndex = 0;
    }

    protected void LinkButtonUserProf_Click(object sender, EventArgs e)
    {
        MultiViewUserChoice.ActiveViewIndex = 0;

        MultiViewUserDtl.ActiveViewIndex = 0;

        //disable profile address tab & Orders tab 
        MultiViewUserAddr.ActiveViewIndex = -1;

        PanelUserOrder.Visible = false;
    }

    protected void LinkButtonUserAddr_Click(object sender, EventArgs e)
    {

        MultiViewUserAddr.ActiveViewIndex = 0;

        //Disable User profile 
        MultiViewUserChoice.ActiveViewIndex = -1;

        //To check customer has already a Shipping address or not 
        cust.MyId = id;

        cust.DisplayCustomerData();

        if (custMpData.ShowCustIdentity() == null)

            MultiViewUserAddr.ActiveViewIndex = 2;

        else
        {

            RptUserAddr.DataSource = custMpData.ShowCustIdentity();

            RptUserAddr.DataBind();
        }

    }

    protected void LinkButtonuserOrdered_Click(object sender, EventArgs e)
    {
        PanelUserOrder.Visible = true;

        //Disable Both tab profile & manage address tab 
        MultiViewUserChoice.ActiveViewIndex = -1;

        MultiViewUserAddr.ActiveViewIndex = -1;
    }

    //If Customer has already addresses
    protected void ButtonUserAddrSave_Click(object sender, EventArgs e)
    {
        //If all controls aren't valid 
        if (!Page.IsValid) return;

        //If all controls are valid, proceed the underneath code

        string fullAddr = String.Concat(TboxLine1.Text, " ", TboxLine2.Text).Trim();

        //Set the objects properties
        cust.CustShippAddr = fullAddr;

        cust.CustShipCountry = DropDownListUserCountry.SelectedItem.Text;

        cust.CustShipState = DropDownListUserState.SelectedItem.Text;

        cust.CustShipCity = TboxCity.Text;

        cust.CustShipPinCode = Convert.ToInt32(TboxPinCode.Text);

        cust.MyId = id;

        // save info into Database at the paricular AddressId
        //Of the customer 
        cust.SaveShipAddr(Convert.ToInt32(ButtonUserAddrSave.CommandArgument));

        //Fetch from database
        cust.DisplayCustomerData();

        //Update the ManipulateCustom obj
        custMpData = cust.ShowCustManupulatedData();

        //Sore updated Manupulated Obj in ViewState
        ViewState["CustManupData"] = custMpData;

        //Fect the Dicitionary Obj & Update the Repeater  
        RptUserAddr.DataSource = custMpData.ShowCustIdentity();

        RptUserAddr.DataBind();

        //Switch to 1st tab
        MultiViewUserAddr.ActiveViewIndex = 0;

        //Update UI 
        ShowDisplayedData();
    }

    protected void DropDownListUserCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Find the Page control through master page     

        ContentPlaceHolder mainContent = Page.Master.FindControl("MainContent") as ContentPlaceHolder;

        DropDownList dlUserState = (DropDownList)mainContent.FindControl("DropDownListUserState");

        //   // fill this state dropdown through Dictionary's Key 
        dlUserState.DataSource = ContStateCollection[DropDownListUserCountry.SelectedItem.Text];

        //   DropDownListUserState.DataTextField = "Value";

        //Activate Binding
        dlUserState.DataBind();
    }

    protected void BtnUserDtlSaved_Click(object sender, EventArgs e)
    {
        //If all controls aren't valid
        if (!Page.IsValid) return;

        //Id all controls are valid , proceed the underneath code

        //Set obj's properties 
        cust.CustFirstName = TBoxFirstName.Text.Trim();

        cust.CustLastName = TBoxLastName.Text.Trim();

        cust.CustMobNo = Convert.ToInt32(TBoxMobNo.Text.Trim());

        cust.EmailId = TBoxEmail.Text;

        cust.MyId = id;

        //Save the details
        cust.SaveCustmerInfo();

        //Fetch from database
        cust.DisplayCustomerData();

        //Update UI 
        ShowDisplayedData();

    }

    protected void BtnShipNewAddrSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        //If all controls are valid, proceed the underneath code

        string fullAddr = String.Concat(TBoxNewAddrLine1.Text, TBoxNewAddrLine2.Text).Trim();

        //Set the objects properties
        cust.CustShippAddr = fullAddr;

        cust.CustShipCountry = ListShipNewCountry.SelectedItem.Text;

        cust.CustShipState = ListShipNewState.SelectedItem.Text;

        cust.CustShipCity = TBoxNewAddrCity.Text;

        cust.CustShipPinCode = Convert.ToInt32(TBoxNewAddrPinCode.Text);

        cust.MyId = id;
        // save info into Database
        cust.SaveShipNewAddr();

        //Fetch from database
        cust.DisplayCustomerData();

        //Update the ManipulateCustom obj
        custMpData = cust.ShowCustManupulatedData();

        //Sore updated Manupulated Obj in ViewState
        ViewState["CustManupData"] = custMpData;

        //Fect the Dicitionary Obj & Update the Repeater  
        RptUserAddr.DataSource = custMpData.ShowCustIdentity();

        RptUserAddr.DataBind();

        //Switch to 1st tab
        MultiViewUserAddr.ActiveViewIndex = 0;

        //Update UI 
        ShowDisplayedData();
    }

    protected void LkBtnAddMoreShipAddr_Click(object sender, EventArgs e)
    {
        MultiViewUserAddr.ActiveViewIndex = 2;
    }

    protected void ButtonUserAddrEdit_Command(object sender, CommandEventArgs e)
    {
        //Open edit tab
        MultiViewUserAddr.ActiveViewIndex = 1;

        ButtonUserAddrSave.CommandArgument = e.CommandArgument.ToString();

    }

    protected void RptUserAddr_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblUserFirstName = e.Item.FindControl("LblUserFirstName") as Label;

        Label lblUserLasttName = e.Item.FindControl("LblUserLastName") as Label;

        Label LblUserAddrEmail = e.Item.FindControl("LblUserAddrEmail") as Label;

        Label LblUseShipAddr = e.Item.FindControl("LblUserShipAddr") as Label;

        Label LblUserShipCity = e.Item.FindControl("LblUserShipCity") as Label;

        Label LblUserShipPinCode = e.Item.FindControl("LblUserShipPinCode") as Label;

        Label LblUserShipCountry = e.Item.FindControl("LblUserShipContry") as Label;

        KeyValuePair<int, Customer> kp = (KeyValuePair<int, Customer>)e.Item.DataItem;

        //   Dictionary<int,MyClass> kp1 = kp.Value.;

        lblUserFirstName.Text = kp.Value.CustFirstName;

        lblUserLasttName.Text = kp.Value.CustLastName;

        LblUserAddrEmail.Text = kp.Value.EmailId;

        LblUseShipAddr.Text = kp.Value.CustShippAddr;

        LblUserShipCity.Text = kp.Value.CustShipCity;

        LblUserShipPinCode.Text = kp.Value.CustShipPinCode.ToString();

        LblUserShipCountry.Text = kp.Value.CustShipCountry;


    }

    protected void ListShipNewCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Find the Page control through master page     

        ContentPlaceHolder mainContent = Page.Master.FindControl("MainContent") as ContentPlaceHolder;

        DropDownList dlUserNewState = (DropDownList)mainContent.FindControl("ListShipNewState");

        dlUserNewState.DataSource = ContStateCollection[ListShipNewCountry.SelectedItem.Text];

        //Activate Binding
        dlUserNewState.DataBind();
    }
}