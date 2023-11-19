using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Globalization;
using DataAccessLayer;

namespace WebApp.Forms
{
    public partial class EmployeeAddUpdate : System.Web.UI.Page
    {
        private String dateFormat = "MM/dd/yyyy";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDownLists();
                LoadData();
            }
        }

        private void BindDropDownLists()
        {
            ddlDesignation.DataSource = Designation.GetAll();
            ddlDesignation.DataValueField = "Id";
            ddlDesignation.DataTextField = "Name";
            ddlDesignation.DataBind();

            ddlCountry.DataSource = Country.GetAll();
            ddlCountry.DataValueField = "Id";
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataBind();

            ddlEmployeeType.DataSource = EmployeeType.GetAll();
            ddlEmployeeType.DataValueField = "Id";
            ddlEmployeeType.DataTextField = "Name";
            ddlEmployeeType.DataBind();
        }

        private void LoadData()
        {
            if (Request.QueryString["Id"] != null)
            {
                try
                {
                    Employee employee = Employee.Get(Convert.ToInt32(Request.QueryString["Id"]));
                    if (employee.Id > 0)
                    {
                        txtFirstName.Text = employee.FirstName;
                        txtMiddleName.Text = employee.MiddleName;
                        txtLastName.Text = employee.LastName;
                        txtEmailAddress.Text = employee.EmailAddress;
                        chkIsActive.Checked = employee.IsActive;
                        ddlEmployeeType.SelectedValue = employee.EmpTypeId.ToString();
                        txtAddress.Text = employee.Address;
                        txtPassportNo.Text = employee.PassportNo;

                        if (employee.DateOfBirth != null)
                        {
                            txtDateOfBirth.Text = Convert.ToDateTime(employee.DateOfBirth).ToString(dateFormat);
                        }

                        if (!String.IsNullOrWhiteSpace(employee.Gender))
                        {
                            ddlGender.SelectedValue = employee.Gender;
                        }

                        if (employee.DesignationId != null)
                        {
                            ddlDesignation.SelectedValue = employee.DesignationId.ToString();
                        }

                        if (employee.Country != null)
                        {
                            ddlCountry.SelectedValue = employee.Country.ToString();
                        }
                    }
                    else
                    {
                        lblMessage.CssClass = "errorMessage";
                        lblMessage.Text = "Record not found.";
                    }
                }
                catch
                {
                    lblMessage.CssClass = "errorMessage";
                    lblMessage.Text = "Unable to load data.";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValid)
                {
                    return;
                }

                Employee employee = GetEmployee();

                if (employee.Id > 0)
                {
                    employee.Update();
                }
                else
                {
                    employee.Add();
                    ViewState["Id"] = employee.Id;
                }

                lblMessage.CssClass = "successMessage";
                lblMessage.Text = "Record saved successfully.";
            }
            catch
            {
                lblMessage.CssClass = "errorMessage";
                lblMessage.Text = "Unable to save record.";
            }
        }

        private Employee GetEmployee()
        {
            Employee employee = new Employee();
            employee.FirstName = txtFirstName.Text;
            employee.MiddleName = String.IsNullOrWhiteSpace(txtMiddleName.Text) ? null : txtMiddleName.Text;
            employee.LastName = txtLastName.Text;
            employee.EmailAddress = String.IsNullOrWhiteSpace(txtEmailAddress.Text) ? null : txtEmailAddress.Text;
            employee.IsActive = chkIsActive.Checked;
            employee.EmpTypeId = Convert.ToInt32(ddlEmployeeType.SelectedValue);
            employee.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue) == -1 ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            employee.Country = Convert.ToInt32(ddlCountry.SelectedValue) == -1 ? (int?)null : Convert.ToInt32(ddlCountry.SelectedValue);
            employee.Address = String.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text;
            employee.DateOfBirth = String.IsNullOrWhiteSpace(txtDateOfBirth.Text) ? (DateTime?)null : DateTime.ParseExact(txtDateOfBirth.Text, dateFormat, CultureInfo.InvariantCulture);
            employee.Gender = ddlGender.SelectedValue == "-1" ? null : ddlGender.SelectedValue;
            employee.PassportNo = String.IsNullOrWhiteSpace(txtPassportNo.Text) ? null : txtPassportNo.Text;
            
            if (Request.QueryString["Id"] != null)
            {
                employee.Id = Convert.ToInt32(Request.QueryString["Id"]);
            }
            else if (ViewState["Id"] != null)
            {
                employee.Id = Convert.ToInt32(ViewState["Id"]);
            }

            return employee;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/EmployeesList.aspx");
        }

        protected void ValidateDate(object sender, ServerValidateEventArgs e)
        {
            try
            {
                DateTime.ParseExact(e.Value, dateFormat, CultureInfo.InvariantCulture);
                e.IsValid = true;
            }
            catch
            {
                e.IsValid = false;
            }
        }
    }
}